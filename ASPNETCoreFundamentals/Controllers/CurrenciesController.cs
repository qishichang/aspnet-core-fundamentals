using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASPNETCoreFundamentals.Controllers
{
    public class CurrenciesController : Controller
    {
        private readonly CurrencyOptions _currencies;

        public CurrenciesController(IOptions<CurrencyOptions> currencies)
        {
            _currencies = currencies.Value;
        }
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

        public IActionResult List()
        {
            return Content($"Currencies: { string.Join(',', _currencies.Currencies.ToArray())}");
        }
    }
}