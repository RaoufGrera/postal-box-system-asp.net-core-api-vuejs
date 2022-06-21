using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BoxSystem10.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SystemData;
using SystemData.Models;

namespace BoxSystem10.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        protected SystemContext _context;
        protected UserManager<AppUser> mUserManager;

        public MainController(SystemContext context, UserManager<AppUser> userManager)
        {
            _context = context; mUserManager = userManager;
        }
    
        [Authorize]
        [HttpGet]
        [Route("boxs")]
        public async Task<IActionResult> getBoxOffices() {
             var OfficeId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "OfficeId").Value;
            int boxnum = 0;
             int.TryParse(OfficeId, out boxnum);
            

            var obj = (from b in _context.Boxes
                       join bt in _context.TypeBox on b.TypeBoxId equals bt.Id
                       where b.OfficeId == boxnum
                       group new {b,bt} by new
                       {
                           b.TypeBoxId,
                           bt.Name,
                           
                       } into g 
                       select new VInfo  { Id= g.Key.TypeBoxId,  Name = g.Key.Name, TypeCount = g.Count() }

                       ).ToList();

            var objEjar = (from b in _context.Boxes
                           join bt in _context.TypeBox on b.TypeBoxId equals bt.Id
                           join bi in _context.Bills on b.Id equals bi.BoxId
                           where !bi.IsActive
                           group new { b, bt } by new
                           {
                               b.TypeBoxId,
                               bt.Name,

                           } into g
                           select new VInfo  { Id = g.Key.TypeBoxId, EjarCount = g.Count() }
                            ).ToList();
            var r = (from i in obj
                     join e in objEjar on i.Id equals e.Id
                     into j from sub in j.DefaultIfEmpty()
                     select new VInfo { Id = i.Id, EjarCount = (sub!= null)? sub.EjarCount:0, Name = i.Name, TypeCount = i.TypeCount }

                     ).ToList();



            r.Add(new VInfo { Id = 9, EjarCount = r.Sum(p => p.EjarCount), Name = "كل الصناديق",   TypeCount = r.Sum(p => p.TypeCount) });


            r.OrderByDescending(p=>p.Id);

            return Ok(r);
        }

    }
}