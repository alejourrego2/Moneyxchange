using Business.Data;
using Business.Entities;
using Business.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Web.Controllers;

namespace Test.Controllers
{
    public class ExchangeController : BaseController
    {
        public ExchangeController(ExchangeContext context) : base(context)
        {
        }

        [Authorize(Roles = "Admin, User")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<JsonResult> ExchangeCurrency([FromBody]ExchangeRequest request)
        {
            ExchangeManager manager;
            ExchangeInfo info;
            string strInfo = HttpContext.Session.GetString("currencyInfo");

            string url = @"http://data.fixer.io/api/latest?access_key=d0def2e2f997f931537d524b82434b2d&symbols=EUR,USD";

            if (!string.IsNullOrEmpty(strInfo))
            {
                info = JsonConvert.DeserializeObject<ExchangeInfo>(strInfo);

                if (DateTime.UtcNow.Date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToDouble(info.Timestamp))).TotalMinutes < 10)
                {
                    manager = new ExchangeManager(url, info);
                }
                else
                {
                    manager = new ExchangeManager(url);
                }
            }
            else
            {
                manager = new ExchangeManager(url);
            }

            decimal result = await manager.ExchangeCurrencies(request.value, request.fromCurrency, request.targetCurrency);

            if (manager.exchangeInfo != null)
            {
                HttpContext.Session.SetString("currencyInfo", JsonConvert.SerializeObject(manager.exchangeInfo));
            }

            return Json(result);
        }
    }
}