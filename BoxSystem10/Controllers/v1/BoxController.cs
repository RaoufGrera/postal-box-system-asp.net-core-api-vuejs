using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemData;
using BoxSystem10.Model;
using BoxSystem10.Services;

using SystemData.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ServiceReference1;
using Microsoft.AspNetCore.Identity;

namespace BoxSystem10.Controllers.v1
{

    [Route("v1/[controller]")]
    [ApiController]
    public class BoxController : ControllerBase
    {
        protected SystemContext _context;
        protected UserManager<AppUser> mUserManager;

        public BoxController(SystemContext context, UserManager<AppUser> userManager) { _context = context; mUserManager = userManager;
        }

        [HttpGet]
        [Route("typebox")]
        public async Task<IActionResult> typebox()
        {

            var list = _context.TypeBox.ToList();
            if (list.Count >= 1)
                return Ok(list);
            return NoContent();
        }
        [HttpGet]
        [Route("allboxoffice")]
        public async Task<IActionResult> allboxoffice()
        {
            var OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;
            int boxnum = 0;
            int.TryParse(OfficeId, out boxnum);
            var list = (from b in _context.Boxes join t in _context.TypeBox on b.TypeBoxId equals t.Id where b.OfficeId == boxnum
                        select new {
                NumberBox = b.NumberBox,
                TypeName =t.Name,
                Id=b.Id,
            }
                        ).OrderBy(o=>o.NumberBox).ToList();
            if (list.Count >= 1)
                return Ok(list);
            return NoContent();
        }

        [HttpGet]
        [Route("region")]
        public async Task<IActionResult> region()
        {

            var list = _context.Region.ToList();
            if (list.Count >= 1)
                return Ok(list);
            return NoContent();
        }

        [HttpGet]
        [Route("office")]
        public async Task<IActionResult> offices(int type =0)
        {

            var list = _context.Office.Where(p=>p.RegionId == type).ToList();
            if (list.Count >= 1)
                return Ok(list);
            return NoContent();
        }


        [HttpGet]
        [Route("officer")]
        public   string getBillsID(string OfficeId)
        {
          //  var OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;

            int boxnum = 0;

            var billsId = (from m in _context.Bills where OfficeId == OfficeId.ToString() orderby m.BillsNumber descending select m.BillsNumber).FirstOrDefault();

            string lastNum = "0";

            lastNum = OfficeId.ToString() + "0000000";

            var d = Convert.ToInt32(lastNum);

            if (billsId != null)
            {
                lastNum = OfficeId.ToString() + "0000000";
             if(billsId >= d) { 
                billsId = billsId + 1;
                lastNum = billsId.ToString();
                }
                else
                {
                    lastNum = OfficeId.ToString() + "0000000";

                }
            }

            return  lastNum ;
        }
        [HttpDelete("{id}")]
        public IActionResult   Delete2User([FromRoute] int id)
        {
      

            var user =  _context.Bills.FirstOrDefault(p=>p.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.IsActive = false;
            
                            user.IsBooked = false;

            _context.SaveChanges();

            return Ok(new {Message = MessageServices.Success });
        }

        [Authorize]
        [HttpPost("{id}")]
        public IActionResult OkBills([FromRoute] int id)
        {

            string OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;

            var BillsNumber = getBillsID(OfficeId);

            var user = _context.Bills.FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.IsActive = true;
            user.IsBooked = false;

            user.BillsNumber = Convert.ToInt32(BillsNumber);

            _context.SaveChanges();

            return Ok(new { Message = MessageServices.Success });
        }

        [Authorize]
        [HttpGet]
        [Route("officebox")]
        public async Task<IActionResult> getBoxOffices(int type = 0)
        {
            var userName = User.Identity.Name;

            var OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;

            int boxnum = 0;
             // var billsId =   (from m in _context.Bills where OfficeId == OfficeId.ToString() select m.BillsNumber ).FirstOrDefault();

            // string lastNum = "0";
            //if(billsId == null) { 
            //     lastNum = OfficeId.ToString() + "000000001";
            // }
            // else {
            //     billsId = billsId + 1;
            //     lastNum = billsId.ToString();
            // }

            int.TryParse(OfficeId, out boxnum);
            var list = (from m in _context.Boxes
                  

                        where !m.IsUsed && m.TypeBoxId == type && m.OfficeId == boxnum select m).ToList(); //_context.Boxes.Where(i=> !i.IsUsed && i.TypeBoxId == type && i.OfficeId == boxnum).ToList();
            if (list.Count >= 1)
                return Ok(list);
            return NoContent();
        }
        [HttpGet]
        [Route("officeboxbyid")]
        public async Task<IActionResult> officeboxbyid(int type = 0,int office = 0)
        {

       
            Random _rand = new Random();

           // var usedBox = (from k in _context.Bills join b in _context.Boxes on k.BoxId equals b.Id where )
            var results = (from m in _context.Boxes where m.OfficeId == office && m.TypeBoxId == type && !m.IsUsed select m.NumberBox).OrderBy(c => Guid.NewGuid()).FirstOrDefault();


            return Ok(results);
    
            return NoContent();
        }
        [HttpPost]
        [Route("checkcost")]
        public IActionResult GetRentCost([FromBody] VRent RevicedData)
        {
            var lastReuslt = checkCost(RevicedData);
            return Ok(new { Result = lastReuslt });


        }
        [HttpPost]
        [Route("checkcostf")]
        public  decimal checkCost(VRent RevicedData)
        {
            var dateRent = RevicedData.DateStartRent;

            //    var daysOfYear = DateTime.IsLeapYear(dateRent.Year) ? 366 : 365;
            //  var days = daysOfYear - dateRent.DayOfYear;
            var days = dateRent.DayOfYear;
            var costOfBox = (from tb in _context.TypeBox
                             join dr in _context.DetailsRent on tb.Id equals dr.TypeBox.Id
                             join dlc in _context.DetailsCost on dr.DetailsCost.Id equals dlc.Id
                             where tb.Id == RevicedData.TypeBoxId &&
         dlc.DayFrom <= days &&
         dlc.DayTo >= days
                             select dr.Cost.ToString(CultureInfo.InvariantCulture)).FirstOrDefault();

            int x = DateTime.Now.Year;
            int y = RevicedData.RentYears - x;

            var totalCost = decimal.Parse(costOfBox);
           var t  = totalCost + RevicedData.PriceKey;
            if (RevicedData.RentYears == 1) return t;


            var costBoxInYearByTypeBoxId = (from d in _context.DetailsRent where d.TypeBox.Id == RevicedData.TypeBoxId select d.Cost).FirstOrDefault();



            RevicedData.RentYears = RevicedData.RentYears - 1;


            var cc = costBoxInYearByTypeBoxId * RevicedData.RentYears;
            var lastReuslt = totalCost + cc + +RevicedData.PriceKey;
            return lastReuslt ;
        }

        [Authorize]
        [HttpPost]
        [Route("save")]
        public IActionResult GetsaveBox([FromBody] VManageCustomer Data)
        {
            string OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;

            var BillsNumber = getBillsID(OfficeId);

           
            var customer = _context.Customer.Where(ss => ss.PhonNumber1 == Data.PhonNumber1).FirstOrDefault();
            if (customer == null)
            {
                Customer customerr = new Customer();

                customerr.Name = Data.Name;
                customerr.PhonNumber1 = Data.PhonNumber1;
                customerr.Address = Data.Address;
                customerr.CustomerIdentityId = (int)Data.CustomerIdentityId;
                customerr.CustomerJobsId = (int)Data.CustomerJobsId;
                customerr.IdentityNumber = Data.IdentityNumber;
                customerr.CustomerTypeId = (int)Data.CustomerTypeId;
                customerr.EmailAddress = Data.EmailAddress;
                customerr.EmailAddress = Data.EmailAddress;
                _context.Customer.Add(customerr);
                _context.SaveChanges();

                customer = customerr;
            }


            Boxes box = _context.Boxes.Where(o => o.NumberBox == Data.BoxId).FirstOrDefault();
            if(box.IsUsed)
                return BadRequest(new { Message = "الصندوق محجوز مسبقاً" });


            box.State = true;
            box.IsUsed = true;


            _context.Entry(box).State = EntityState.Modified;

            _context.SaveChanges();

            var y = DateTime.Now.AddYears(Data.RentYears);
            Bills bills = new Bills();
            VRent a = new VRent
            {
                DateStartRent = DateTime.Now,
                TypeBoxId = Data.TypeBoxId,
                PriceKey = (int)Data.PriceKey,
                RentYears = Data.RentYears
            };
            var totalprocefun = checkCost(a);
            bills.TotalBills = totalprocefun;
            bills.InsertDate = DateTime.Now;
            bills.ExpiretDate = new DateTime(y.Year, 1, 1);
            bills.IsActive = true;
            bills.IsManual = true;
            bills.PayTypeId = (int)Data.PayTypeId;
            bills.DateBills = Data.DateStartRent;

            string UserId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            bills.AppUserId = int.Parse(UserId);
            bills.BoxId = box.Id;
            bills.CustomerId = customer.Id;
            bills.PriceKey = Data.PriceKey;
            // bills.BillsNumber = Data.BillsNumber;
            bills.BillsNumber = Convert.ToInt32(BillsNumber);
            bills.IsActive = true;
            _context.Bills.Add(bills);
            _context.SaveChanges();



            return Ok(new { Message = MessageServices.Success });

        }
        [Authorize]
        [HttpPost]
        [Route("saveboxcustomer")]
        public IActionResult GetsaveBoxCustomer([FromBody] VManageCustomer Data)
        {
            var userName = User.Identity.Name;


            var customer = _context.Customer.Where(ss => ss.PhonNumber1 == userName).FirstOrDefault();
            if (customer == null)
            {
                return BadRequest(new { Message = "يرجي التأكد من صحة بيانات الملف الشخصي" });

                /*  Customer customerr = new Customer();

                  customerr.Name = Data.Name;
                  customerr.PhonNumber1 = Data.PhonNumber1;
                  customerr.Address = Data.Address;
                  customerr.CustomerIdentityId = Data.CustomerIdentityId;
                  customerr.CustomerJobsId = Data.CustomerJobsId;
                  customerr.IdentityNumber = Data.IdentityNumber;
                  customerr.CustomerTypeId = Data.CustomerTypeId;
                  customerr.EmailAddress = Data.EmailAddress;
                  customerr.EmailAddress = Data.EmailAddress;
                  _context.Customer.Add(customerr);
                  _context.SaveChanges();

                  customer = customerr;*/
            }
            Random _rand = new Random();

            // var usedBox = (from k in _context.Bills join b in _context.Boxes on k.BoxId equals b.Id where )
            Boxes box = (from m in _context.Boxes where m.OfficeId == Data.OfficeId && m.TypeBoxId == Data.TypeBoxId && !m.IsUsed select m).OrderBy(c => Guid.NewGuid()).FirstOrDefault();


 

             if (box.IsUsed)
                return BadRequest(new { Message = "الصندوق محجوز مسبقاً" });


            box.State = true;
            box.IsUsed = true;


            _context.Entry(box).State = EntityState.Modified;

            _context.SaveChanges();

            var y = DateTime.Now.AddYears(Data.RentYears);
            Bills bills = new Bills();

            VRent a = new VRent
            {
                DateStartRent = DateTime.Now,
                TypeBoxId = Data.TypeBoxId,
                PriceKey = (int)Data.PriceKey,
                RentYears = Data.RentYears
            };
            var totalprocefun = checkCost(a);

            bills.TotalBills = totalprocefun;
            bills.InsertDate = DateTime.Now;
            bills.ExpiretDate = new DateTime(y.Year, 1, 1);
            bills.IsActive = false;
            bills.IsManual = false;
            bills.IsBooked = true;
            bills.PayTypeId = (int)Data.PayTypeId;
            bills.DateBills = Data.DateStartRent;

        //    string UserId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value;
        //    bills.AppUserId = int.Parse(UserId);
            bills.BoxId = box.Id;
            bills.CustomerId = customer.Id;
            bills.PriceKey = Data.PriceKey;
            bills.BillsNumber = Data.BillsNumber;
            //bills.IsActive = true;
            _context.Bills.Add(bills);
            _context.SaveChanges();



            return Ok(new { Message = MessageServices.Success });

        }

        /*     [Route("resave/{id}/{number}")]
        public IActionResult refreshSave(int id, int number)***/

        [HttpGet]
        [Route("resave/{id}")]
        public async Task<IActionResult> getCustomerDataResave(int id)
        {

            var r = (from bi in _context.Bills
                     join c in _context.Customer on bi.CustomerId equals c.Id
                     join b in _context.Boxes on bi.BoxId equals b.Id
                     where bi.Id == id
                     select new VManageCustomer
                     {
                         CustomerId = c.Id,
                         Name = c.Name,
                         PhonNumber1 = c.PhonNumber1,
                         Address = c.Address,
                         CustomerJobsId = c.CustomerJobsId,
                         CustomerIdentityId = c.CustomerIdentityId,
                         CustomerTypeId = c.CustomerTypeId,
                         IdentityNumber = c.IdentityNumber,
                         TypeBoxId = b.TypeBoxId,
                         DateStartRent = bi.ExpiretDate,
                         NumberBox = b.NumberBox,
                         EmailAddress = c.EmailAddress,
                         TypeKey = b.TypeBoxId,
                         BillsId = c.Id,
        }).FirstOrDefault();

            return Ok(r);
            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [Route("resave/{id}")]
        public IActionResult refreshSave(int id,[FromBody] VManageCustomer Data)
        {

            string OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;

            var UserId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var BillsNumber = getBillsID(OfficeId);
            var customer = _context.Customer.Where(p => p.Id == Data.CustomerId).FirstOrDefault();
            var b = _context.Bills.Where(p => p.Id == id).FirstOrDefault();
            if(customer != null)
            {
                Boxes box = _context.Boxes.Where(p => p.NumberBox == Data.NumberBox ).FirstOrDefault();
                box.State = true;
                box.IsUsed = true;


                _context.Entry(box).State = EntityState.Modified;

                _context.SaveChanges();

                VRent a = new VRent {
                    DateStartRent = DateTime.Now ,
                    TypeBoxId = Data.TypeBoxId,PriceKey =(int)Data.PriceKey,
                    RentYears = Data.RentYears
                };
                var totalprocefun = checkCost(a);
                var y = Data.RentYears + DateTime.Now.Year;
                Bills bills = new Bills();

                bills.TotalBills = totalprocefun;
                bills.InsertDate = DateTime.Now;
                bills.ExpiretDate = b.ExpiretDate.AddYears(Data.RentYears);
                bills.IsActive = true;
                bills.IsManual = true;
                bills.PayTypeId = (int)Data.PayTypeId;
                bills.DateBills = Data.DateStartRent;
                bills.AppUserId = int.Parse(UserId);
                bills.BoxId = box.Id;
                bills.CustomerId = customer.Id;
                bills.PriceKey = Data.PriceKey;
                bills.IsActive = true;


                bills.BillsNumber = Convert.ToInt32(BillsNumber);
                _context.Bills.Add(bills);
                _context.SaveChanges();



                return Ok(new { Message = MessageServices.Success });

            }

            return Ok(new { Message = MessageServices.Error });


        }
        [Authorize]
        [HttpGet]
        [Route("randombox")]
        public async Task<IActionResult> getRandomNumberBox()
        {
            var OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;

            int officeNum = 0;

            int.TryParse(OfficeId, out officeNum);
            Random _rand = new Random();
            var results = (from m in _context.Boxes where  m.OfficeId == officeNum  select m.NumberBox ).OrderBy(c => Guid.NewGuid()).FirstOrDefault();


                return Ok(results);
        }


        [Authorize]
        [HttpGet]
        [Route("boxrent")]
        public async Task<IActionResult> getAllBoxRent()
        {
            var userName = User.Identity.Name;

            var OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;

            int officeNum = 0;

            int.TryParse(OfficeId, out officeNum);

            var r = new List<VManageCustomer>();
            r = (from c in _context.Customer
                 join b in _context.Bills on c.Id equals b.CustomerId

                 join bo in _context.Boxes on b.BoxId equals bo.Id
                 where bo.OfficeId == officeNum && b.IsActive



                 select new VManageCustomer
                 {

                     CustomerId = c.Id,
                     BillsId = b.Id,
                     PhonNumber1 = c.PhonNumber1,


                     Name = c.Name,
                     NumberBox = bo.NumberBox,

                 }
                    ).ToList();

            if (r.Count >= 1)
                return Ok(r);
            return NoContent();
        }
        [HttpGet]
        [Route("note")]
        public async Task<IActionResult> getAllnote()
        {


            var list = _context.NoteType.ToList();
            if (list.Count >= 1)
                return Ok(list);
            return NoContent();
        }
        [Authorize]
        [HttpGet]
        [Route("notehistory")]
        public async Task<IActionResult> getAllnoteHistory()
        {
            var OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;
            int officeNum = 0;

            int.TryParse(OfficeId, out officeNum);

            var list = (from c in _context.Customer join bi in _context.Bills on c.Id equals bi.CustomerId
                        join bb in _context.Boxes on bi.BoxId equals bb.Id

                        join bc in _context.NoteTypeBills on bi.Id equals bc.BillsId
                        join nt in _context.NoteType on bc.NoteTypeId equals nt.Id
                        where bb.OfficeId == officeNum
                        orderby  bc.Id descending
                        select new
                        {
                            Name = c.Name,
                            NoteTypeName = nt.Name,
                            CreateDate= bc.CreateDate.ToString("yyyy-MM-dd"),
                            NumberBox = bb.NumberBox,
                        }
                        
                        )
                        .Take(100)
                        .ToList() ;
            if (list.Count >= 1)
                return Ok(list);
            return NoContent();
        }


        [Authorize]
        [HttpGet]
        [Route("notehistorycustomer")]
        public async Task<IActionResult> getAllnoteHistoryCustomer()
        {
            //var OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;
            var userName = User.Identity.Name;
            

            var list = (from c in _context.Customer
                        join bi in _context.Bills on c.Id equals bi.CustomerId
                        join bb in _context.Boxes on bi.BoxId equals bb.Id

                        join bc in _context.NoteTypeBills on bi.Id equals bc.BillsId
                        join nt in _context.NoteType on bc.NoteTypeId equals nt.Id
                        where c.PhonNumber1 == userName
                        orderby bc.Id descending
                        select new
                        {
                            Name = c.Name,
                            NoteTypeName = nt.Name,
                            CreateDate = bc.CreateDate.ToString("yyyy-MM-dd"),
                            NumberBox = bb.NumberBox,
                        }

                        )
                        .Take(100)
                        .ToList();
            if (list.Count >= 1)
                return Ok(list);
            return NoContent();
        }


        [HttpPost]
        [Route("send")]
    public IActionResult GetsaveBoxz([FromBody] VData Data)

        {

            Service1Client c = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

            var mes = _context.NoteType.FirstOrDefault(l => l.Id == Data.TypeId).Message;
            var k = Data.data;

            var phones = (from cc in _context.Customer join b in _context.Bills on cc.Id equals b.CustomerId where k.Contains(b.Id) && cc.PhonNumber1 != "" select cc.PhonNumber1).ToList();
            string str="";
            foreach (var ph in phones)
            {
                
                if(ph.StartsWith("0"))
                   str = ph.Remove(0, 1);

                 string number = "+218"+ str;

                c.SendSMSAsync(mes, number);

            }
            foreach ( var item in k)
            {
                _context.NoteTypeBills.Add(new NoteTypeBills { BillsId = item, NoteTypeId = Data.TypeId,CreateDate= DateTime.Now  });
                _context.SaveChanges();

            }

            return Ok(new { Message = MessageServices.Success });

        }

        [HttpGet("booked")]
        public IActionResult GetCustomersBooked()
        {

            // var r = _context.Users;

            var OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;
            int officeNum = 0;

            int.TryParse(OfficeId, out officeNum);

            var r = new List<VManageCustomer>();
            try
            {


                r = (from c in _context.Customer
                     join b in _context.Bills on c.Id equals b.CustomerId
                      
                     join bo in _context.Boxes on b.BoxId equals bo.Id
                  
                     join bt in _context.TypeBox on bo.TypeBoxId equals bt.Id
                     
                     where bo.OfficeId == officeNum && b.IsBooked == true  && b.IsManual==false



                     select new VManageCustomer
                     {
                         CustomerId = c.Id,
                         PhonNumber1 = c.PhonNumber1,
                         IdentityNumber = c.IdentityNumber,
                         CustomerJobsId = c.CustomerJobsId,
                         CustomerIdentityId = c.CustomerIdentityId,
                         CustomerTypeId = c.CustomerTypeId,
                         IsActive = b.IsActive,
                         Name = c.Name,
                         Address = c.Address,
                         BillsNumber = b.BillsNumber,
                         BillsId = b.Id,
                     //    BookedExpret = (subpet == null || subpet.ExpiretDate == null) ? "" : subpet.ExpiretDate.ToString("yyyy-MM-dd"),
                         DateBills = b.DateBills.ToString("yyyy-MM-dd"),
                         ExpiretDate = b.ExpiretDate.ToString("yyyy-MM-dd"),
                         NumberBox = bo.NumberBox,
                         TypeBox = bt.Name,

                     }
                        ).ToList();
            }
            catch (Exception e)
            {
                var ee = e;
            }
            if (r.Count == 0)
                return BadRequest(new { message = "انتهت نتائج البحث" });


            return Ok(r);
        }



        [HttpGet]
        [Route("sendtest")]
        public IActionResult GetsaveBfoxz()

        {
            Service1Client c = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

            var txtMsg = "السلام عليكم";
            string number = "+218927223001";

            c.SendSMSAsync(txtMsg, number);


            return Ok(new { Message = MessageServices.Success });

        }


        [Authorize]
        [HttpGet("mybox")]
        public async Task<IActionResult> mybox()
        {
           // var userPhone = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Phone").Value;
            var userId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userName = User.Identity.Name;

            var r = await (from c in _context.Customer join bi in _context.Bills on c.Id equals bi.CustomerId join b in _context.Boxes on bi.BoxId equals b.Id
                           join of in _context.Office on b.OfficeId equals of.Id
                           join ot in _context.TypeBox on b.TypeBoxId equals ot.Id
                           where userId == c.UserId.ToString()
                           select new VManageCustomer
                           {
                               CustomerId = c.Id,
                               Name = c.Name,
                               PhonNumber1 = c.PhonNumber1,
                               Address = c.Address,
                               CustomerJobsId = c.CustomerJobsId,
                               CustomerIdentityId = c.CustomerIdentityId,
                               CustomerTypeId = c.CustomerTypeId,
                               IdentityNumber = c.IdentityNumber,
                               TypeBoxId = b.TypeBoxId,
                               NumberBox = b.NumberBox,
                               DateBills = bi.DateBills.ToString("yyyy-MM-dd"),
                               BookedExpret = (bi.BookedExpret != null) ? ((DateTime)(bi.BookedExpret)).ToString("yyyy-MM-dd") : "", 
                               ExpiretDate = bi.ExpiretDate.ToString("yyyy-MM-dd"),
                               IsActive = bi.IsActive,
                               IsBooked = bi.IsBooked,
                               //State = (bi.IsBooked && !bi.IsActive)? "محجوز":"",
                               EmailAddress = c.EmailAddress,
                               OfficeName = of.Name,
                               TypeBox = ot.Name,
                               BillsId = bi.Id,
                           }).ToListAsync();
                          
      


            return Ok(r);

        }
        [Authorize]
        [HttpPost]
        [Route("createboxs")]
        public IActionResult createboxs([FromBody] VData Data)
        {
            string OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;
            int officeNum = 0;

            int.TryParse(OfficeId, out officeNum);
            var k = Data.number;

            _context.Boxes.RemoveRange(_context.Boxes.Where(x => k.Contains( x.NumberBox)));
            _context.SaveChanges();
            foreach(var box in k)
            {
                _context.Boxes.Add(new Boxes {
                    NumberBox = box,
                    TypeBoxId = Data.TypeId,
                    OfficeId = officeNum,
                    IsUsed = false,

                });

            }
            _context.SaveChanges();

            return Ok(new { Message = MessageServices.Success });

        }
        [Authorize]
        [HttpPost]
        [Route("updateboxs")]
        public IActionResult updateboxs([FromBody] VData Data)
        {
            string OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;
            int officeNum = 0;

            int.TryParse(OfficeId, out officeNum);
            var k = Data.number;
            var friendsaa = _context.Boxes.Where(f => k.Contains(f.NumberBox)).ToList();

            var friends = _context.Boxes.Where(f => k.Contains(f.NumberBox) && f.OfficeId == officeNum).ToList();
            friends.ForEach(a => a.TypeBoxId = Data.TypeId);
            _context.SaveChanges();
          
 
            return Ok(new { Message = MessageServices.Success });

        }
        [Authorize]
        [HttpPost]
        [Route("deleteboxs")]
        public IActionResult deleteboxs([FromBody] VData Data)
        {
            string OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;
            int officeNum = 0;

         
            int.TryParse(OfficeId, out officeNum);
            var k = Data.number;

            _context.Boxes.RemoveRange(_context.Boxes.Where(x => k.Contains(x.NumberBox)));
            _context.SaveChanges();


            return Ok(new { Message = MessageServices.Success });

        }
        [Authorize]
        [HttpPost]
        [Route("savebox")]
        public IActionResult saveBox([FromBody] VManageCustomer Data)
        {
            string OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;
            Customer customer = new Customer();

            customer.Name = Data.Name;
            customer.PhonNumber1 = Data.PhonNumber1;
            customer.Address = Data.Address;
            customer.CustomerIdentityId = (int)Data.CustomerIdentityId;
            customer.CustomerJobsId = (int)Data.CustomerJobsId;
            customer.IdentityNumber = Data.IdentityNumber;
            customer.CustomerTypeId = (int)Data.CustomerTypeId;
            customer.EmailAddress = Data.EmailAddress;
            customer.EmailAddress = Data.EmailAddress;
            _context.Customer.Add(customer);
            _context.SaveChanges();



            Boxes box = _context.Boxes.Where(o => o.NumberBox == Data.BoxId).FirstOrDefault();
            if(box.IsUsed)
                return BadRequest(new { Message = "الصندوق محجوز مسبقاً" });


            box.State = true;
            box.IsUsed = true;


            _context.Entry(box).State = EntityState.Modified;

            _context.SaveChanges();

            var y = Data.RentYears + DateTime.Now.Year;
            Bills bills = new Bills();

            bills.TotalBills = decimal.Parse(Data.TotalPrice);
            bills.InsertDate = DateTime.Now;
            bills.ExpiretDate = DateTime.Now.AddYears(Data.RentYears);
            bills.IsActive = true;
            bills.IsManual = true;
            bills.PayTypeId = (int)Data.PayTypeId;
            bills.DateBills = Data.DateStartRent;

            string UserId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            bills.AppUserId = int.Parse(UserId);
            bills.BoxId = box.Id;
            bills.CustomerId = customer.Id;
            bills.PriceKey = Data.PriceKey;
            bills.BillsNumber = Data.BillsNumber;
            bills.IsActive = true;
            _context.Bills.Add(bills);
            _context.SaveChanges();



            return Ok(new { Message = MessageServices.Success });

        }

      
        /*     [Route("resave/{id}/{number}")]
        public IActionResult refreshSave(int id, int number)***/

        [HttpGet]
        [Route("resavebox/{id}")]
        public async Task<IActionResult> getCustomerDataResav(int id)
        {
 
             var r = (from bi in _context.Bills
                     join c in _context.Customer on bi.CustomerId equals c.Id
                     join b in _context.Boxes on bi.BoxId equals b.Id
                     where bi.Id == id 
                     select new VManageCustomer
                     {
                         CustomerId = c.Id,
                         Name = c.Name,
                         PhonNumber1 = c.PhonNumber1,
                         Address = c.Address,
                         CustomerJobsId = c.CustomerJobsId,
                         CustomerIdentityId = c.CustomerIdentityId,
                         CustomerTypeId = c.CustomerTypeId,
                         IdentityNumber = c.IdentityNumber,
                         TypeBoxId = b.TypeBoxId,
                         NumberBox = b.NumberBox,
                         EmailAddress = c.EmailAddress,
                         TypeKey = b.TypeBoxId,
                         BillsId = c.Id,
        }).FirstOrDefault();

            return Ok(r);
            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [Route("resavebox/{id}")]
        public IActionResult reSave(int id,[FromBody] VManageCustomer Data)
        {

            var UserId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value;

            var customer = _context.Customer.Where(p => p.Id == Data.CustomerId).FirstOrDefault();
            var b = _context.Bills.Where(p => p.Id == id).FirstOrDefault();
            if(customer != null)
            {
                Boxes box = _context.Boxes.Where(p => p.NumberBox == Data.NumberBox ).FirstOrDefault();
                box.State = true;
                box.IsUsed = true;


                _context.Entry(box).State = EntityState.Modified;

                _context.SaveChanges();

                var y = Data.RentYears + DateTime.Now.Year;
                Bills bills = new Bills();

                bills.TotalBills = decimal.Parse(Data.TotalPrice);
                bills.InsertDate = DateTime.Now;
                bills.ExpiretDate = b.ExpiretDate.AddYears(Data.RentYears);
                bills.IsActive = true;
                bills.IsManual = true;
                bills.PayTypeId = (int)Data.PayTypeId;
                bills.DateBills = Data.DateStartRent;
                bills.AppUserId = int.Parse(UserId);
                bills.BoxId = box.Id;
                bills.CustomerId = customer.Id;
                bills.PriceKey = Data.PriceKey;
                bills.IsActive = true;


                bills.BillsNumber = Data.BillsNumber;
                _context.Bills.Add(bills);
                _context.SaveChanges();



                return Ok(new { Message = MessageServices.Success });

            }

            return Ok(new { Message = MessageServices.Error });


        }

        [Authorize]
        [HttpGet]
        [Route("data")]
        public IActionResult getCustomerData()
        {
            var r = User.Identity.Name;
            var userData = _context.Customer.Where(p => p.PhonNumber1 == r).FirstOrDefault();
            return Ok(userData);

        }

        [Authorize]
        [HttpPost]
        [Route("berforeeditbycustomer")]
        public async Task<IActionResult> BeforeEditCustomerAsync([FromBody] Customer data)
        {

            var n = User.Identity.Name;
            

            if (!data.PhonNumber1.StartsWith("09") && data.PhonNumber1.Length != 10)
                return BadRequest(new { message = "رقم الهاتف غير صحيح" });

            if (n != data.PhonNumber1) { 
            var v = _context.Users.Where(p => p.UserName == data.PhonNumber1  ).FirstOrDefault();

            if (v!= null)
                return BadRequest(new { message = "رقم الهاتف مستخدم من قبل" });
                var vv = _context.Customer.Where(p => p.PhonNumber1 == data.PhonNumber1).FirstOrDefault();

                if (vv != null)
                    return BadRequest(new { message = "رقم الهاتف مستخدم من قبل" });


            }


            Random rnd = new Random();
            int month = rnd.Next(10000, 99999);
            //if (n != data.PhonNumber1) {

              

                var r =  _context.Customer.Where(p => p.PhonNumber1 == n).FirstOrDefault();
           // if (r != null)
         //   {

                    r.PhonNumber2 = month.ToString();

                    _context.SaveChanges();

             


             //   }
              

        //    }

            Service1Client c = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);




            var str = data.PhonNumber1;
            if (data.PhonNumber1.StartsWith("0"))
                str = data.PhonNumber1.Remove(0, 1);

            string number = "+218" + str;
            string msg = "رمز تأكيد تعديل بيانات المستخدم" + month;
            await c.SendSMSAsync(msg, number);
            return Ok(month);


           /* var result = _context.Customer.Where(p => p.PhonNumber1 == n).FirstOrDefault();

            if (result != null)
            {
                result.Name = data.Name;
                result.Address = data.Address;
                // result.IdentityNumber = data.IdentityNumber;

//                result.PhonNumber1 = data.PhonNumber1;
                result.CustomerTypeId = data.CustomerTypeId;
                result.CustomerIdentityId = data.CustomerIdentityId;
                result.CustomerJobsId = data.CustomerJobsId;



                _context.SaveChanges();
                return Ok(new { message = MessageServices.Success });

            }
            return BadRequest(new { message = MessageServices.Error });

            */

        }

        [Authorize]
        [HttpPost]
        [Route("editbycustomer")]
        public IActionResult EditCustomerByCustomer([FromBody] Customer data)
        {
            var n = User.Identity.Name;
            if (!data.PhonNumber1.StartsWith("09") && data.PhonNumber1.Length != 10)
                return BadRequest(new { message = "رقم الهاتف غير صحيح" });

            var r = _context.Customer.Where(p => p.PhonNumber1 == n).FirstOrDefault();

            if (data.PhonNumber2 != r.PhonNumber2)
            {
                return BadRequest(new { message = "رمز التأكيد غير صحيح" });
            }


            var result = _context.Customer.Where(p => p.PhonNumber1 == n).FirstOrDefault();

            if (result != null)
            {
                result.Name = data.Name;
                result.Address = data.Address;
                // result.IdentityNumber = data.IdentityNumber;

                 result.PhonNumber1 = data.PhonNumber1;
                result.CustomerTypeId = data.CustomerTypeId;
                result.CustomerIdentityId = data.CustomerIdentityId;
                result.CustomerJobsId = data.CustomerJobsId;



                
                var v =   _context.Users.Where(p => p.UserName == n).FirstOrDefault();
                if (v != null)
                {
                    _context.SaveChanges();
                    v.UserName = data.PhonNumber1;
                   
                     mUserManager.UpdateAsync(v);
                    _context.SaveChangesAsync();

                }
                 

                return Ok(new { message = MessageServices.Success });

            }
            return BadRequest(new { message = MessageServices.Error });
             






        }



    }
}