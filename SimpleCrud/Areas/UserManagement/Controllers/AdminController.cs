using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleCrud.Areas.UserManagement.Controllers
{
    [Area("usermanagement")]
    [Route("{usermanagement}/admin")]
    public class AdminController : Controller
    {
        [Route("")]
        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("logging")]
        public async Task<IActionResult> Logging(string username, string password, string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            if (username == "admin" && password == "admin")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("username", username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                var claimsIditity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrinciple = new ClaimsPrincipal(claimsIditity);
                await HttpContext.SignInAsync(claimsPrinciple);
                return Redirect(returnUrl);
            }
            TempData["Error"] = "Error, Username or Password is invalid.";
            return View("login");
        }
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/index");
        }
    }
}
