using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.Controllers
{
    public class CurrenciesController : Controller
    {
        public IActionResult Index()
        {
            var url = Url.Action("View", "Currencies", new { code = "USD" });
            var url2 = Url.RouteUrl("currency_by_code", new { code = "USD" });
            return Content($"The url is {url}, another url is {url2}");
        }

        public string View(string code)
        {
            return code;
        }

        public string Convert(string currency, string others)
        {
            return $"{currency} to {others}";
        }

        public IActionResult RedirectingToAnActionMethod()
        {
            return RedirectToAction("View", "Currencies", new { code = "USD" });
        }

        public IActionResult RedirectingToARoute()
        {
            return RedirectToRoute("currency_by_code", new { code = "USD" });
        }

        public IActionResult RedirectingToAnActionInTheSameController()
        {
            return RedirectToAction("View");
        }
    }
}