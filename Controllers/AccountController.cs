using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace securityarchitecture.Controllers
{
    public class AccountController : Controller
    {
        // Optional dependency injection for the authorization service.
        private readonly IAuthorizationService _authservice;
        public AccountController(IAuthorizationService authService)
        {
            _authservice = authService;
        }


        // GET: /<controller>/
        public IActionResult Login(string returnUrl)
        {
            return RedirectToAction("Authorize", new { returnUrl = returnUrl });
        }

        public async Task<IActionResult> Authorize(string returnUrl)
        {
            // Creates a fake login and sets the cookies and stuff.
            // You know, this is a nice pretend database.
            var claims = new Claim[]
            {
                new Claim("id", "1"),
                new Claim("name", "brandon"),
                new Claim("email", "bresheske@emaildomain.com"),
                new Claim("role", "hasPolicy"),
                new Claim("level", "seniorLevel")
            };
            var identity = new ClaimsIdentity(claims, "auth", "name", "role");
            var user = new ClaimsPrincipal(identity);
            await HttpContext.Authentication.SignInAsync("CookieMonster", user);
            return Redirect(returnUrl);
        }

        public IActionResult Denied()
        {
            return Content("ACCESS DENIED");
        }
    }
}
