using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemData;

namespace BoxSystem10.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SystemContext _context;
        public UserController(SystemContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
 
            var users =(from m in _context.Users join o in _context.Office on m.OfficeId equals o.Id where !m.IsAdmin
                        select new {
                            m.Id,
                            m.Name,
                            OfficeName =o.Name,
                        }).ToList();

            return Ok(users);
        }

       
    }
}