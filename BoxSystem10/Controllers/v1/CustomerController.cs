using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BoxSystem10.Model;
using BoxSystem10.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemData;
using SystemData.Models;

namespace BoxSystem10.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        protected SystemContext _context;


        public CustomerController(SystemContext context) { _context = context; }

        [Authorize]
        [HttpGet("search")]

        public IActionResult GetCustomers(int Page,string Name = "", string BoxNumber = "" ,string PhoneNumber ="")
        {

            var OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;
            int officeNum = 0;

            int.TryParse(OfficeId, out officeNum);
            int records_at_page = 10;
            int start = (Page - 1) * records_at_page;
            int end = records_at_page;


            long boxnum = 0;
            long.TryParse(BoxNumber, out boxnum);
          //  var dd = (from bbbb in _context.Boxes where bbbb.OfficeId == officeNum select bbbb).ToList();

            var r = new List<VManageCustomer>();
            try
            {


                r = (from c in _context.Customer
                     join b in _context.Bills on c.Id equals b.CustomerId
                     /*   into gj
                     from subpet in gj.DefaultIfEmpty()*/
                     join bo in _context.Boxes on b.BoxId equals bo.Id
              /*       into boo
                     from subbo in boo.DefaultIfEmpty()*/
                     join bt in _context.TypeBox on bo.TypeBoxId equals bt.Id
               /*          into btt
                     from subbt in btt.DefaultIfEmpty()*/
                     where 
                     bo.OfficeId == officeNum &&
                     (Name == null || c.Name.Contains(Name))

                                       && (PhoneNumber == null || c.PhonNumber1.Contains(PhoneNumber))
                                       && (b == null || b.IsBooked == false)
                                      



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
                         BillsId =  b.Id,
                         DateBills = b.DateBills.ToString("yyyy-MM-dd"),
                         ExpiretDate =  b.ExpiretDate.ToString("yyyy-MM-dd"),
                         NumberBox = (!b.IsActive) ? 0 : bo.NumberBox,
                         TypeBox = (!b.IsActive) ? "" : bt.Name,
                     }).Skip(start).Take(end).ToList();
            }
            catch (Exception e)
            {
                var ee = e;
            }
            if (r.Count == 0)
                return BadRequest(new { message = "انتهت نتائج البحث" });


            return Ok(r);
        }
       
        [HttpPost]
        [Route("addCustomer")]
        public IActionResult AddCustomer([FromBody] Customer data)
        {



            if (!ModelState.IsValid)
                return BadRequest(new { message = MessageServices.Error });

            Customer customer = new Customer
            {
                Id = data.Id,
                Name = data.Name,
                Address = data.Address,
                EmailAddress = data.EmailAddress,
                PhonNumber1 = data.PhonNumber1,
                PhonNumber2 = data.PhonNumber2,
                CustomerIdentityId = data.CustomerIdentityId,
                CustomerJobsId = data.CustomerJobsId,
                CustomerTypeId = data.CustomerTypeId,
                IdentityNumber = data.IdentityNumber
            };



            _context.Customer.Add(data);
            _context.SaveChanges();



            return Ok(new { message = MessageServices.Success });
        }

        [HttpPost]
        [Route("editCustomer")]
        public IActionResult EditCustomer([FromBody] Customer data)
        {
       

            var result = _context.Customer.SingleOrDefault(b => b.Id == data.Id);
            if (result != null)
            {
                result.Name = data.Name;
                result.Address = data.Address;
                result.IdentityNumber = data.IdentityNumber;

                result.PhonNumber1 = data.PhonNumber1;
                result.CustomerTypeId = data.CustomerTypeId;
                result.CustomerIdentityId = data.CustomerIdentityId;
                result.CustomerJobsId = data.CustomerJobsId;

                _context.SaveChanges();
                return Ok(new { message = MessageServices.Success });

            }
            return BadRequest(new { message = MessageServices.Error });






        }
        [HttpGet("customertype")]
        public IEnumerable GetCustomerType()
        {
            var i = (from o in _context.CustomerType select o).ToList();
            return i;
        }

        [HttpGet("customeridentity")]
        public IEnumerable GetCustomerIdentity()
        {
            var i = (from o in _context.CustomerIdentity select o).ToList();
            return i;
        }

        [HttpGet("customerjobs")]
        public IEnumerable GetCustomerJobs()
        {
            var i = (from o in _context.CustomerJobs select o).ToList();
            return i;
        }
        [HttpGet]
        [Route("paytype")]
        public async Task<IActionResult> paytype()
        {

            var list = _context.PayType.ToList();
            if (list.Count >= 1)
                return Ok(list);
            return NoContent();
        }








        /****************************************************************************************************************************/
        /***************************************************  CustomerController ****************************************************/
        /****************************************************************************************************************************/



        [HttpGet]
        [Authorize]
        [Route("mybox")]
        public async Task<IActionResult> customerbox()
        {
            var UserId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value;

            var rrr = Convert.ToInt32(UserId);
           
            



            var list =  (from b in _context.Bills join c in _context.Customer on b.CustomerId equals c.Id where c.UserId == rrr select c  ).ToList();
            if (list.Count >= 1)
                return Ok(list);
            return NoContent();
        }

    }
}