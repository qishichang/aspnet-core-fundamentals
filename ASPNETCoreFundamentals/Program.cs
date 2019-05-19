using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Core;
using ASPNETCoreFundamentals.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ASPNETCoreFundamentals
{
    public class Program
    {
        public static readonly Dictionary<string, string> _switchMappings =
            new Dictionary<string, string>
            {
                { "-CLKey1", "CommandLineKey1" },
                { "-CLKey2", "CommandLineKey2" }
            };

        public static readonly Dictionary<string, string> _dict =
            new Dictionary<string, string>
            {
                { "MemoryCollectionKey1", "value1" },
                { "MemoryCollectionKey2", "value2" }
            };

        public static readonly Dictionary<string, string> _arrayDict =
            new Dictionary<string, string>
            {
                { "array:entries:0", "value0" },
                { "array:entries:1", "value1" },
                { "array:entries:2", "value2" },
                //{ "array:entries:3", "value3" },
                { "array:entries:4", "value4" },
                { "array:entries:5", "value5" }
            };

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .CreateLogger();
            try
            {
                var host = CreateWebHostBuilder(args).Build();

                using (var serviceScope = host.Services.CreateScope())
                {
                    var services = serviceScope.ServiceProvider;
                    try
                    {
                        var serviceContext = services.GetRequiredService<IMyDependency>();
                        serviceContext.WriteMessage("Program.Main created this message.").Wait();
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred.");
                    }
                }

                host.Run();

            }
            catch (Exception exx)
            {
                Log.Fatal(exx, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {

            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.FullName;
            return WebHost.CreateDefaultBuilder()
                   .ConfigureAppConfiguration((hostingContext, config) =>
                   {
                       config.SetBasePath(Directory.GetCurrentDirectory());
                       config.AddInMemoryCollection(_dict);
                       config.AddInMemoryCollection(_arrayDict);
                       config.AddIniFile("config.ini", optional: true, reloadOnChange: true);
                       config.AddJsonFile("config.json", optional: true, reloadOnChange: true);
                       config.AddJsonFile("starship.json", optional: true, reloadOnChange: true);
                       config.AddJsonFile("missing_value.json", optional: false, reloadOnChange: false);
                       config.AddJsonFile("json_array.json", optional: true, reloadOnChange: true);
                       config.AddXmlFile("config.xml", optional: true, reloadOnChange: true);
                       config.AddXmlFile("tvshow.xml", optional: true, reloadOnChange: true);
                       var path = Path.Combine(Directory.GetCurrentDirectory(), "path/to/files");
                       config.AddKeyPerFile(directoryPath: path, optional: true);
                       config.AddEFConfiguration(options => options.UseInMemoryDatabase("InMemoryDb"));
                       config.AddCommandLine(args, _switchMappings);
                   })
                   //.UseSerilog()
                   .UseStartup(assemblyName)
                   .UseDefaultServiceProvider(options =>
                   {
                       options.ValidateScopes = true;
                   });
        }
    }
}
