using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CMS.Repo.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleCrud.AppDbContext;
using SimpleCrud.Models;
using SimpleCrud.Repository.UserRepository;
using SimpleCrud.UnitOfWork;
using SimpleCrud.ViewModels;

namespace SimpleCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyWorkRepository myWorkRepository;

        public HomeController(MyWorkRepository myWorkRepository)
        {
            this.myWorkRepository = myWorkRepository;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            HomeViewModel vm = new HomeViewModel();
            vm.myWorks = myWorkRepository.Queryable().Where(a => !a.IsDeleted).ToList();
            return View(vm);
        }

        //[HttpGet]
        //[Route("login")]
        //public IActionResult Login(string returnUrl)
        //{
        //    ViewData["returnUrl"] = returnUrl;
        //    return View();
        //}
        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Logging(string username,string password, string returnUrl)
        //{
        //    ViewData["returnUrl"] = returnUrl;
        //    if (username=="hello" && password=="world")
        //    {
        //        var claims = new List<Claim>();
        //        claims.Add(new Claim("username", username));
        //        claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
        //        var claimsIditity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
        //        var claimsPrinciple = new ClaimsPrincipal(claimsIditity);
        //        await HttpContext.SignInAsync(claimsPrinciple);
        //        return Redirect(returnUrl);
        //    }
        //    TempData["Error"] = "Error, Username or Password is invalid.";
        //    return View("login");
        //}

        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync();
        //    return Redirect("/");
        //}
    }
}
