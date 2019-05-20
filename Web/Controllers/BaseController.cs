using Business.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(ExchangeContext context)
        {
            Context = context;
        }

        protected ExchangeContext Context { get; set; }

        protected override void Dispose(bool disposing)
        {
            Context.Dispose();
            base.Dispose(disposing);
        }
    }
}
