using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Data;
using Business.Entities;
using Business.Managers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;

namespace CookieDemo.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(ExchangeContext context) : base(context)
        {
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login([FromBody]LoginRequest login)
        {
            ClaimsIdentity identity = null;
            bool isAuthenticated = false;

            UserManager manager = new UserManager(Context);
            if (await manager.IsUserValidAsync(login.userName, login.password)){
                
                //Create the identity for the user
                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, login.userName),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                isAuthenticated = true;
            }

            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }

            return Json(new { Authenticated = isAuthenticated, urlToRedirect = isAuthenticated ? @"/Exchange/Index" : null });
        }
        
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}


