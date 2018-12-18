using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.Controllers
{
    public class CountriesController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public CountriesController(IHostingEnvironment env)
        {
            _environment = env;
        }

        public IActionResult Index()
        {
            return View(DataSource.Countries);
        }

        [HttpGet]
        public IActionResult UpdateNationalFlag(string code)
        {
            var country = DataSource.Countries.SingleOrDefault(c => c.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase));
            return View(country);
        }

        [HttpPost]
        public IActionResult UpdateNationalFlag(string code, IFormFile nationalFlagFile)
        {
            if(nationalFlagFile == null || nationalFlagFile.Length == 1)
            {
                return RedirectToAction(nameof(Index));
            }

            var targetFileName = $"{code}{Path.GetExtension(nationalFlagFile.FileName)}";
            var relativeFilePath = Path.Combine("images", targetFileName);
            var absoluteFilePath = Path.Combine(_environment.WebRootPath, relativeFilePath);
            var country = DataSource.Countries.SingleOrDefault(c => c.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase));
            country.NationalFlagPath = relativeFilePath;
            using (var stream = new FileStream(absoluteFilePath, FileMode.Create))
            {
                nationalFlagFile.CopyTo(stream);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}