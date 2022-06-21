using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoxSystem10.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
      public IActionResult Index()
        {
            return new RedirectResult("~/swagger/");
        }

       /* [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello", "World" };
        }*/
    }
}