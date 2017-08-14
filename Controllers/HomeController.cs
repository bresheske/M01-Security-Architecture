using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace securityarchitecture.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            ViewData["name"] = "LOGGED IN AS : " + this.User.Claims.First(x => x.Type == "name").Value;
            return View();
        }

        [Authorize("PolicyName")]
        public IActionResult Policy()
        {
            ViewData["Message"] = "This is a secure page with a policy required.";
            ViewData["name"] = "LOGGED IN AS : " + this.User.Claims.First(x => x.Type == "name").Value;
            return View("About");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
