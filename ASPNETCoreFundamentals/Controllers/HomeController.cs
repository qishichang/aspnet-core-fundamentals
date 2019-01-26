using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCoreFundamentals.Models;
using ASPNETCoreFundamentals.Core;

namespace ASPNETCoreFundamentals.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMyDependency _myDependency;

        public HomeController(IMyDependency myDependency)
        {
            _myDependency = myDependency;
        }

        public async Task<IActionResult> Index()
        {
            await _myDependency.WriteMessage("HomeController.Index created this message.");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            ViewData["country"] = "United States";
            ViewData["state"] = "Washington";
            ViewData["county"] = "King";
            ViewData["city"] = "Redmond";

            ViewData["columnNames"] = new string[] { "ID", "Name", "Price" };
            ViewData["content"] = new string[,]{
                {"101", "Apple", "1.01"},
                {"202", "Back", "2.02"},
                {"303", "Cup", "3.03"},
                {"404", "Donut", "3.03"}
            };

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

        public IActionResult Page1() => View();

        public IActionResult Page2() => View();
    }
}
