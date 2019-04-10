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
using Microsoft.Extensions.Configuration;
using ASPNETCoreFundamentals.Filters;

namespace ASPNETCoreFundamentals.Controllers
{
    [AddHeaderWithFactory]
    public class HomeController : Controller
    {
        private readonly IMyDependency _myDependency;
        private readonly MyOptions _options;
        private readonly MyOptionsWithDelegateConfig _optionsWithDelegateConfig;
        private readonly MySubOptions _subOptions;
        private readonly MyOptions _snapshotOptions;
        private readonly MyOptions _named_options_1;
        private readonly MyOptions _named_options_2;
        private readonly IConfiguration _config;

        public HomeController(
            IConfiguration config,
            IOptionsMonitor<MyOptions> optionAccessor,
            IOptionsMonitor<MyOptionsWithDelegateConfig> optionsAccessorWithDelegateConfig,
            IOptionsMonitor<MySubOptions> subOptionsAccessor,
            IOptionsSnapshot<MyOptions> snapshotOptionsAccessor,
            IOptionsSnapshot<MyOptions> namedOptionsAccessor,
            IMyDependency myDependency,
            OperationService operationService,
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,
            IOperationSingletonInstance instanceOperation)
        {
            _config = config;

            _options = optionAccessor.CurrentValue;
            _optionsWithDelegateConfig = optionsAccessorWithDelegateConfig.CurrentValue;
            _subOptions = subOptionsAccessor.CurrentValue;
            _snapshotOptions = snapshotOptionsAccessor.Value;
            _named_options_1 = namedOptionsAccessor.Get("named_options_1");
            _named_options_2 = namedOptionsAccessor.Get("named_options_2");

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
            ViewBag.SnapshotOptions = $"snapshot option1 = {_snapshotOptions.Option1}, " +
                $"snapshot option2 = {_snapshotOptions.Option2}";
            ViewBag.NamedOptions = $"named_options_1: option1 = {_named_options_1.Option1}, option2 = {_named_options_1.Option2}," +
                $"named_options_2: option1 = {_named_options_2.Option1}, option2 = {_named_options_2.Option2}";
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

        public IActionResult Config()
        {
            ViewBag.CommandLineConfig = $"CommandLineValue1: {_config.GetValue<string>("CommandLineKey1")}, "
                   + $"CommandLineValue2: {_config.GetValue<string>("CommandLineKey2")}, "
                   + $"CommandLineValue3: {_config.GetValue<string>("CommandLineKey3")}";

            ViewBag.ConnectionString = $"Connection: {_config.GetConnectionString("MvcMovieContext")}";
            var providerName = _config.GetValue<string>("ConnectionStrings:MvcMovieContext_ProviderName");
            if (!string.IsNullOrEmpty(providerName))
            {
                ViewBag.ConnectionString += $", Provider: {providerName}";
            }

            ViewBag.INIConfig = $"section0:key0 = {_config.GetValue<string>("section0:key0")}, " +
                            $"section0:key1 = { _config.GetValue<string>("section0:key1")}, " +
                            $"section1:subsection:key = { _config.GetValue<string>("section1:subsection:key")}, " +
                            $"section2:subsection0:key = { _config.GetValue<string>("section2:subsection0:key")}, " +
                            $"section2:subsection1:key = { _config.GetValue<string>("section2:subsection1:key")}";

            ViewBag.JsonConfig = $"jsonoption1 = {_config.GetValue<string>("jsonoption1")}, " +
                                 $"jsonoption2:suboption1 = {_config.GetValue<string>("jsonoption2:suboption1")}, " +
                                 $"jsonoption2:suboption2 = {_config.GetValue<string>("jsonoption2:suboption2")}";

            ViewBag.XmlConfig = $"section0:key0 = {_config.GetValue<string>("section0:key0")}, " +
                                $"section0:key1 = {_config.GetValue<string>("section0:key1")}, " +
                                $"section1:key0 = {_config.GetValue<string>("section1:key0")}, " +
                                $"section1:key1 = {_config.GetValue<string>("section1:key1")}, " +
                                $"section:section0:name = {_config.GetValue<string>("section:section0:name")}, " +
                                $"section:section0:key:key0:name = {_config.GetValue<string>("section:section0:key:key0:name")}, " +
                                $"section:section0:key:key0 = {_config.GetValue<string>("section:section0:key:key0")}, " +
                                $"section:section0:key:key1:name = {_config.GetValue<string>("section:section0:key:key1:name")}, " +
                                $"section:section0:key:key1 = {_config.GetValue<string>("section:section0:key:key1")}";

            ViewBag.KeyPerFileConfig = $"Logging:LogLevel:System = {_config.GetValue<string>("Logging:LogLevel:System")}";

            ViewBag.InMemoryConfig = $"MemoryCollectionKey1 = {_config.GetValue<string>("MemoryCollectionKey1")}, " +
                                     $"MemoryCollectionKey2 = {_config.GetValue<string>("MemoryCollectionKey2")}";


            var starship = new Starship();
            _config.GetSection("starship").Bind(starship);
            ViewBag.StarshipConfig = $"Starship name: {starship.Name}, registry: {starship.Registry}, class: {starship.Class}, length: {starship.Length}, commisioned: {starship.Commissioned}, trademark: {_config.GetValue<string>("trademark")}";

            var tvShow = _config.GetSection("tvshow").Get<TvShow>();
            ViewBag.TvShowConfig = $"TvShow metadata: {tvShow.Metadata.Series}-{tvShow.Metadata.Title}-{tvShow.Metadata.AirDate}-{tvShow.Metadata.Episode}, actors: {tvShow.Actors.Names}, legal: {tvShow.Legal}";

            var arrayExample = _config.GetSection("array").Get<ArrayExample>();
            ViewBag.ArrayConfig = $"Entries: {string.Join(',', arrayExample.Entries)}";

            var jsonArrayExample = _config.GetSection("json_array").Get<JsonArrayExample>();
            ViewBag.JsonArrayConfig = $"Json Array: Key-{jsonArrayExample.Key}, Subsection: {string.Join(',', jsonArrayExample.Subsection)}";

            ViewBag.EFConfiguration = $"quote1: {_config.GetValue<string>("quote1")}, quote2: {_config.GetValue<string>("quote2")}, quote3: {_config.GetValue<string>("quote3")}";
            return View();
        }

        public IActionResult TwoColumnLayout() => View();

        public IActionResult SelectLists()
        {
            SelectListsViewModel vm = new SelectListsViewModel();
            vm.SelectedValue1 = "cpp";
            vm.SelectedValue2 = "js";
            vm.MultiValues = new List<string> { "csharp", "cpp" };
            vm.SelectedValues = new List<string> { "csharp", "js" };

            return View(vm);
        }
    }
}
