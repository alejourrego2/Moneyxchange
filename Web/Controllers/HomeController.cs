using Business.Data;
using CookieDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Controllers;

namespace CookieDemo.Controllers
{

    public class HomeController : BaseController
    {
        public HomeController(ExchangeContext context) : base(context)
        {
        }

        [Authorize(Roles ="Admin, User")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Setting()
        {
            return View();

        }

        public IActionResult About()
        {
            ViewData["Message"] = "This page was develop as Interview Test";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
