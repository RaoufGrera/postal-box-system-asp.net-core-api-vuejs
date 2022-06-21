using BoxSystem10.Enums;
using BoxSystem10.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SystemData;
using SystemData.Models;
  

namespace BoxSystem10.Services
{
    public interface IAuthService
    {
        Task<VUser> LoginFunc(string username, string password);
        Task<VUser> LoginFuncUser(string phone, string password);

        Task<VMessage> RegisterFunc(VUser user);

        Task<VUser> FacebookAuthFunc(string AccessToken);
        Task<AppUser> GetUserAsync(int userId);
        Task<string> PhoneAuthFunc(VUser vUser);
        Task<bool> ChangePassword(VUser user);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
        //User Create(User user, string password);
        //void Update(User user, string password = null);
        //void Delete(int id);
    }

    public class AuthService : IAuthService
    {
        private SystemContext _context;

         private readonly JwtIssuerOptions _jwtOptions;
        private readonly FacebookAuthSettings _fbAuthSettings;
        private static readonly HttpClient Client = new HttpClient();

        private readonly AppSettings _appSettings;
        private readonly UserManager<AppUser> _mUserManager;
        private readonly SignInManager<AppUser> _mSignInManager;
        public AuthService(IOptions<AppSettings> appSettings , SystemContext context, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IOptions<JwtIssuerOptions> jwtOptions, IOptions<FacebookAuthSettings> fbAuthSettingsAccessor)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _mUserManager = userManager;
            _mSignInManager = signInManager;
            _jwtOptions = jwtOptions.Value;
            _fbAuthSettings = fbAuthSettingsAccessor.Value;
        

        }
        public Task<AppUser> GetUserAsync(int userId)
        {
            return _context.Users.Include(x => x.Office).FirstOrDefaultAsync(x => x.Id == userId);
        }
        public async Task<VUser> LoginFunc(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == username && x.IsValid);
            if (user == null)
                return null;
            var signInAsync = await _mSignInManager.PasswordSignInAsync(user, password, true, false)
               .ConfigureAwait(false);
            
            if (!signInAsync.Succeeded)
                return null;



            var objResult = await GenerateJwt(user);

            return objResult;
        }
        public async Task<VUser> LoginFuncUser(string phone, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.PhoneNumber == phone && x.IsValid);
            if (user == null)
                return null;
            var signInAsync = await _mSignInManager.PasswordSignInAsync(user, password, true, false)
               .ConfigureAwait(false);

            if (!signInAsync.Succeeded)
                return null;



            var objResult = await GenerateJwt(user);

            return objResult;
        }
        

        public async Task<VMessage> RegisterFunc(VUser user)
        {

            VMessage _VMessage = new VMessage() ;

            // validation
            if (string.IsNullOrWhiteSpace(user.Password))
                return null;
            if (_context.Users.Any(x => x.UserName == user.UserName))
            {
                _VMessage.Name = "اسم المستخدم موجود من قبل";
                _VMessage.Type = (int)EnumMessage.Error;
                return _VMessage;
            }
            var us = new AppUser()
            {
                UserName = user.UserName,
                Name = user.Name,
                Email = "cc@ccc.cc;",
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                IsAdmin = (user.OfficeId != null ) ? true:false,
                OfficeId = user.OfficeId

            };

            var result = await _mUserManager.CreateAsync(us, user.Password).ConfigureAwait(false);


            _VMessage.Name = MessageServices.Success;
            _VMessage.Type = (int)EnumMessage.Success;



            //_context.Users.Add(user);
            //_context.SaveChanges();

            return _VMessage;
        }

        public async Task<bool> ChangePassword(VUser user)
        {
            var userFromDb = await  GetUserAsync(user.UserId).ConfigureAwait(false);

            if (userFromDb != null)
            {

          
                await _mUserManager.ChangePasswordAsync(userFromDb, userFromDb.Password, user.Password);
                userFromDb.OfficeId = user.OfficeId;
                userFromDb.Name = user.Name;

                userFromDb.UserName = user.UserName;
                userFromDb.PhoneNumber = user.PhoneNumber;
                userFromDb.Password = user.Password;
                await _mUserManager.UpdateAsync(userFromDb);
                await _context.SaveChangesAsync();
 

                return true;
            }
                return false;

        }

        public async Task<VUser> FacebookAuthFunc(string AccessToken)
        {
        
                // 1.generate an app access token
                var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_fbAuthSettings.AppId}&client_secret={_fbAuthSettings.AppSecret}&grant_type=client_credentials");
                var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
                // 2. validate the user access token
                var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={AccessToken}&access_token={appAccessToken.AccessToken}");
                var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

                if (!userAccessTokenValidation.Data.IsValid)
                {
                    return null;
                }
          
            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await _mUserManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new AppUser
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    FacebookId = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    PictureUrl = userInfo.Picture.Data.Url
                };

                var result = await _mUserManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

                if (!result.Succeeded) return null;

                //    await _context.User.AddAsync(new Customer { IdentityId = appUser.Id, Location = "", Locale = userInfo.Locale, Gender = userInfo.Gender });
                //    await _context.SaveChangesAsync();
                //
            }

            // generate the jwt for the local user...

            var Objuser = await _mUserManager.FindByNameAsync(userInfo.Email);

            var objResult = await GenerateJwt(Objuser);

            return objResult;
        }

        public async Task<string> PhoneAuthFunc(VUser vUser)
        {

             //if (!vUser.PhoneNumber.StartsWith("09") && vUser.PhoneNumber.Length != 10)
             //   return "4";

            var checkExist = _context.Users.Where(o => o.PhoneNumber == vUser.PhoneNumber).FirstOrDefault();

            
            Random rnd = new Random();
            int month = 1;// rnd.Next(10000, 99999);
            if (checkExist == null)
            {
                var appUser = new AppUser
                {
                     FirstName = "مستخدم جديد",
                      PhoneNumber = vUser.PhoneNumber,
                    Password = vUser.Password,
                    UserName = vUser.PhoneNumber,
                    IsCustomer = true,
                    IsValid = false,
                    PhoneAuth = month.ToString()

                };
                var result = await _mUserManager.CreateAsync(appUser, vUser.Password).ConfigureAwait(false);



                var checkCustomer = _context.Customer.Where(p => p.PhonNumber1 == vUser.PhoneNumber).FirstOrDefault();

                if (checkCustomer != null) {

                    checkCustomer.UserId = appUser.Id;
                    _context.SaveChanges();

                }
                else
                {

                    var custtype = _context.CustomerType.FirstOrDefault();
                    var custIdent = _context.CustomerIdentity.FirstOrDefault();
                    var custjob = _context.CustomerJobs.FirstOrDefault();


                    Customer customer = new Customer
                    {
                        UserId = appUser.Id,
                        Name = appUser.FirstName,
                        PhonNumber1 = appUser.PhoneNumber,
                        EmailAddress = appUser.Email,

                        CustomerTypeId = custtype.Id,
                        CustomerIdentityId = custIdent.Id,
                        CustomerJobsId = custjob.Id

                    };

                    _context.Customer.Add(customer);
                    _context.SaveChanges();
                }
                if (!result.Succeeded) return null;

            }
            else
            {
                if (checkExist.IsValid == true) return "3";
                Random rndd = new Random();
                int monthh = 1;// rndd.Next(10000, 99999);
                checkExist.PhoneAuth = month.ToString();

                
                _context.SaveChanges();
            }


          


            var Objuser = await _mUserManager.FindByNameAsync(vUser.PhoneNumber);

           //  var objResult = await GenerateJwt(Objuser);

            
            Service1Client c = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);


            return "1";

            var str = vUser.PhoneNumber;
                if (vUser.PhoneNumber.StartsWith("0"))
                    str = vUser.PhoneNumber.Remove(0, 1);

                string number = "+218" + str;
            string msg = "رمز تفعيل الحساب لخدمة الصناديق البريدية" + month;
                await c.SendSMSAsync(msg, number);
 
           


        }

        public async Task<VUser> GenerateJwt(AppUser objUser)
        {
            if (objUser == null)
                return null;


            var claims = new[]
                          {
                    new Claim(ClaimTypes.Name,objUser.UserName),
                     new Claim("OfficeId",objUser.OfficeId.ToString()),
                     new Claim("UserId",objUser.Id.ToString()),

                    new Claim("Admin",(objUser.IsAdmin) ? EnumUserType.Admin.ToString() :""),
                    new Claim("Office",(!objUser.IsAdmin && !objUser.IsCustomer) ? EnumUserType.Office.ToString() :""),
                  new Claim("User",(objUser.IsCustomer) ? EnumUserType.User.ToString() :""),
                    new Claim("Phone",objUser.PhoneNumber.ToString() ),
                };

            var tokenHandler = new JwtSecurityTokenHandler();
            //  var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
 
             var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));


            var token = new JwtSecurityToken(
              issuer: _jwtOptions.Issuer,
              audience: _jwtOptions.Audience,
              claims: claims,
              
                //expires: _jwtOptions.Expiration,
                expires: DateTime.Now.AddHours(10),

                           signingCredentials : new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256)
);


            string OName = null;

            objUser.Token = new JwtSecurityTokenHandler().WriteToken(token);
            _context.Entry(objUser).Property("Token").IsModified = true;
            _context.Users.Update(objUser);

            if (!objUser.IsAdmin && !objUser.IsCustomer)
            {
                OName = _context.Office.Where(o => o.Id == objUser.OfficeId).Select(p => p.Name).FirstOrDefault();
            }
            return new VUser
            {
                Message = "تم تسجيل الدخول بنجاح",
                UserId = objUser.Id,
                Name = objUser.Name,
                UserName = objUser.UserName,
                Token = objUser.Token,
                OfficeName = OName,
                OfficeId = objUser.OfficeId,
                IsCustomer = objUser.IsCustomer,
                
                
            };
        }

    }
}
