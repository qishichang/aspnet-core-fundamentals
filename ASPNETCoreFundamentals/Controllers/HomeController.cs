using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCoreFundamentals.Models;
using ASPNETCoreFundamentals.Core;
using ASPNETCoreFundamentals.Options;
using Microsoft.Extensions.Options;

namespace ASPNETCoreFundamentals.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMyDependency _myDependency;
        private readonly MyOptions _options;
        private readonly MyOptionsWithDelegateConfig _optionsWithDelegateConfig;
        private readonly MySubOptions _subOptions;

        public HomeController(
            IOptionsMonitor<MyOptions> optionAccessor,
            IOptionsMonitor<MyOptionsWithDelegateConfig> optionsAccessorWithDelegateConfig,
            IOptionsMonitor<MySubOptions> subOptionsAccessor,
            IMyDependency myDependency,
            OperationService operationService,
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,
            IOperationSingletonInstance instanceOperation)
        {
            _options = optionAccessor.CurrentValue;
            _optionsWithDelegateConfig = optionsAccessorWithDelegateConfig.CurrentValue;
            _subOptions = subOptionsAccessor.CurrentValue;

            _myDependency = myDependency;
            OperationService = operationService;
            TransientOperation = transientOperation;
            ScopedOperation = scopedOperation;
            SingletonOperation = singletonOperation;
            InstanceOperation = instanceOperation;
        }

        public OperationService OperationService { get; }
        public IOperationTransient TransientOperation { get; }
        public IOperationScoped ScopedOperation { get; }
        public IOperationSingleton SingletonOperation { get; }
        public IOperationSingletonInstance InstanceOperation { get; }

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

        public IActionResult Page3()
        {
            ViewBag.SampleOptions = $"option1 = {_options.Option1}, option2 = {_options.Option2}";
            ViewBag.SimpleOptionsWithDelegateConfig = $"delegate_option1 = {_optionsWithDelegateConfig.Option1}, " +
                $"delegate_option2 = {_optionsWithDelegateConfig.Option2}";
            ViewBag.SubOptions = $"subOption1 = {_subOptions.SubOption1}, subOption2 = {_subOptions.SubOption2}";
            return View();
        }

        public IActionResult Services()
        {
            var message = "<h4>Controller operations:</h4>";
            message += $"Transient: {TransientOperation.OperationId}<br/>";
            message += $"Scoped: {ScopedOperation.OperationId}<br/>";
            message += $"Singleton: {SingletonOperation.OperationId}<br/>";
            message += $"Instance: {InstanceOperation.OperationId}<br/><br/>";
            message += "<h4>OperationService operations:</h4>";
            message += $"Transient: {OperationService.TransientOperation.OperationId}<br/>";
            message += $"Scoped: {OperationService.ScopedOperation.OperationId}<br/>";
            message += $"Singleton: {OperationService.SingletonOperation.OperationId}<br/>";
            message += $"Instance: {OperationService.InstanceOperation.OperationId}<br/>";
            ViewBag.Message = message;
            return View();
        }
    }
}
