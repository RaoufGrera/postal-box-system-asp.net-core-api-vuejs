using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using BoxSystem10.Enums;
using BoxSystem10.Model;
using BoxSystem10.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SystemData;
using SystemData.Models;

namespace BoxSystem10.Controllers.v1
{
    [Route("v1/[controller]")]
    public class AuthController : ControllerBase
    {
        public IAuthService _authService;
        protected SystemContext _context;
         protected UserManager<AppUser> mUserManager;


        public AuthController(SystemContext context,
            UserManager<AppUser> userManager,

            IAuthService authService
 
           )
        {
            _authService = authService;
            _context = context;
            mUserManager = userManager;



        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]AppUser userParam)
        {
            var user = _authService.LoginFunc(userParam.UserName, userParam.Password);

            if (user.Result == null)
                return BadRequest(new { message = "أسم المستخدم او كلمة السر غير صحيحية " });

            return Ok(user.Result);
        }

        [Authorize]
      //  [Authorize(Policy = "AdminGuard")]
        [HttpGet("Test")]
        public IActionResult Test()
        {
            var userName = User.Identity.Name;
            var OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;

            return Ok($"Super secret content, I hope you've got clearance for this {userName}..OfficeId: .{OfficeId}");
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] VUser user)
        {

            var responseVm = await _authService.RegisterFunc(user).ConfigureAwait(false);


            if(responseVm == null)
                return BadRequest(new { message = "أسم السمتخدم او كلمة السر غير صحيحية " });

            return Ok(new { message = "تم تسجيل الدخول بنجاح" });

        }

        [AllowAnonymous]
        [HttpPost("facebookAuth")]
        public async Task<IActionResult> facebookAuth([FromBody] VUser userr)
        {
            var user = _authService.FacebookAuthFunc(userr.UserName);

            if (user.Result == null)
                return BadRequest(new { message = "خطاء اثناء تسجيل الدخول" });

            return Ok(user.Result);

        }

        [AllowAnonymous]
        [HttpPost("phoneLogin")]
        public IActionResult phoneLogin([FromBody]AppUser userParam)
        {
            var user = _authService.LoginFunc(userParam.UserName, userParam.Password);

            if (user.Result == null)
                return BadRequest(new { message = "رقم الهاتف غير صحيح او غير مفعل او كلمة السر غير صحيحة " });

            return Ok(user.Result);
        }


        [AllowAnonymous]
        [HttpPost("phoneAuth")]
        public async Task<IActionResult> phoneAuth([FromBody] VUser vUser)
        {
            var user = _authService.PhoneAuthFunc(vUser);

            if (user.Result == null)
                return BadRequest(new { message = "خطاء اثناء التسجيل" });
            if (user.Result == "4")
                return BadRequest(new { message = "رقم الهاتف غير صحيح" });


            if (user.Result == "1")
            return Ok(new { message = "تم إرسال رمز جديد" });
            else
                return BadRequest(new { message = "رقم الهاتف مستخدم والحساب مفعل الرجاء تسجيل الدخول" });


        }

        [AllowAnonymous]
        [HttpPost("phoneCheck")]
        public async Task<IActionResult> phoneCheck([FromBody] string code)
        {
            var r = await _context.Users.Where(p => p.PhoneAuth == code).FirstOrDefaultAsync();
            if (r != null)
            {

                r.PhoneAuth = null;
                r.IsValid = true;
                await mUserManager.UpdateAsync(r);
                await _context.SaveChangesAsync();

            }
            else
            {
                return BadRequest(new { message = "الرمز غير صحيح" });

            }


            return Ok(new { message = "تم تفعيل الحساب الرجاء تسجيل الدخول" });

        }


        [AllowAnonymous]
        [HttpPost("loginUser")]
        public IActionResult loginUser([FromBody]AppUser userParam)
        {
            var user = _authService.LoginFuncUser(userParam.PhoneNumber, userParam.Password);

            if (user.Result == null)
                return BadRequest(new { message = "رقم الهاتف او كلمة السر غير صحيحية " });

            return Ok(user.Result);
        }

        [HttpGet("users")]
        public IEnumerable GetUsers()
        {
           // var r = _context.Users;

            var i = (from u in _context.Users join o in _context.Office on u.OfficeId equals o.Id where !u.IsDelete select new VUser{

                UserId = u.Id,
                OfficeId = u.OfficeId,
                OfficeName = o.Name,
                Name = u.Name,
                UserName = u.UserName,
                Password = u.Password
            });

            return i;
        }

        [HttpPost]
        [Route("addUser")]
        public async Task<IActionResult> AddUser([FromBody] VUser userData)
        {



            if (!ModelState.IsValid)
                return BadRequest(new { message = MessageServices.Error });


            var result = await _authService.RegisterFunc(userData).ConfigureAwait(false);

            if(result.Type == (int)EnumMessage.Error)
                return BadRequest(new { message = result.Name });

            return Ok(new { message = result.Name });
        }

        [HttpPost]
        [Route("editUser")]
        public async Task<IActionResult> EditUser([FromBody] VUser user)
        {

            if (!ModelState.IsValid)
                return BadRequest(new { message = MessageServices.Error });

            var userFromDb = await _authService.ChangePassword(user).ConfigureAwait(false);

            if (userFromDb)
            {
                return Ok(new { message = MessageServices.Success });
            }
            return Ok(new { message = MessageServices.Error });
        
        }
        [Authorize]
        [HttpDelete]
        [Route("deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {

            var itemToRemove = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);

            if (itemToRemove == null) return BadRequest();

            itemToRemove.IsDelete = true;

       
            await _context.SaveChangesAsync();

            return Ok(true);
        }

      


        [HttpGet("offices")]
        public IEnumerable GetOffices()
        {
            // var r = _context.Users;

            var i = (from o in _context.Office select o).ToList();
                
            return i;
        }
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> Delete2User([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.IsDelete = true;
            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}