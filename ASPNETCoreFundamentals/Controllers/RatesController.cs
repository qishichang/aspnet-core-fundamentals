using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.Controllers
{
    public class RatesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string View(string currency)
        {
            return currency;
        }
    }
}