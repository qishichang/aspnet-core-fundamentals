﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

        public static void Main(string[] args)
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

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddInMemoryCollection(_dict);
                    config.AddIniFile("config.ini", optional: true, reloadOnChange: true);
                    config.AddJsonFile("config.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile("starship.json", optional: true, reloadOnChange: true);
                    config.AddXmlFile("config.xml", optional: true, reloadOnChange: true);
                    config.AddXmlFile("tvshow.xml", optional: true, reloadOnChange: true);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "path/to/files");
                    config.AddKeyPerFile(directoryPath: path, optional: true);
                    config.AddCommandLine(args, _switchMappings);
                })
                .UseStartup<Startup>();
    }
}
