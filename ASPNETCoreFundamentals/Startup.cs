using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Core;
using ASPNETCoreFundamentals.Data;
using ASPNETCoreFundamentals.Helpers;
using ASPNETCoreFundamentals.Middlewares;
using ASPNETCoreFundamentals.Modules;
using ASPNETCoreFundamentals.Options;
using ASPNETCoreFundamentals.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASPNETCoreFundamentals
{
    public class StartupStaging
    {
        private readonly IHostingEnvironment _env;
        private readonly ILoggerFactory _loggerFactory;

        public StartupStaging(IConfiguration configuration, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _env = env;
            _loggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var logger = _loggerFactory.CreateLogger<Startup>();

            if (_env.IsDevelopment())
            {
                // Development service configuration
                logger.LogInformation("Development environment");
            }
            else
            {
                // Non-development service configuration
                logger.LogInformation($"Environment: {_env.EnvironmentName}");
            }

            services.AddDbContext<TodoContext>(options =>
                options.UseInMemoryDatabase("db"));


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.Configure<MyOptions>(Configuration);
            services.Configure<MyOptionsWithDelegateConfig>(myOptions =>
            {
                myOptions.Option1 = "value1_configured_by_delegate";
                myOptions.Option2 = 500;
            });
            services.Configure<MySubOptions>(Configuration.GetSection("subsection"));
            services.Configure<MyOptions>("named_options_1", Configuration);
            services.Configure<MyOptions>("named_options_2", myOptions =>
            {
                myOptions.Option1 = "named_options_2_value_from_action";
            });


            services.AddOptions<MyOptions>().Configure(o => o.Option1 = "default");
            services.AddOptions<MyOptions>("named_options_1").Configure<OperationService>((o, s) =>
            {
                o.Option1 = "named_" + s.TransientOperation.OperationId;

            });

            services.PostConfigure<MyOptions>(myOptions =>
            {
                myOptions.Option1 = "post_configured_option1_value";
            });
            services.PostConfigure<MyOptions>("named_options_1", myOptions =>
            {
                myOptions.Option1 = "post_configured_option1_value";
            });
            services.PostConfigureAll<MyOptions>(myOptions =>
            {
                myOptions.Option1 = "post_configured_all_option1_value";
            });

            //services.ConfigureAll<MyOptions>(myOptions =>
            //{
            //    myOptions.Option1 = "ConfigureAll replacement value";
            //});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddScoped<IMyDependency, MyDependency>();
            //services.AddTransient<IOperationTransient, Operation>();
            //services.AddScoped<IOperationScoped, Operation>();
            //services.AddSingleton<IOperationSingleton, Operation>();
            //services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));

            services.AddTransient<OperationService, OperationService>();

            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaultModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration config, TodoContext todoContext, IOptionsMonitor<MyOptions> optionsAccessor)
        {
            var option1 = optionsAccessor.CurrentValue.Option1;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            todoContext.Database.EnsureCreated();

            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "text/plain";

                await context.HttpContext.Response.WriteAsync($"Status code page, status code: {context.HttpContext.Response.StatusCode}");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMiddleware<CustomExceptionMiddleware>();

            var value = config["quote1"];

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _env = env;
            _loggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return StartupConfigureServices(services);
        }

        public IServiceProvider ConfigureStagingServices(IServiceCollection services)
        {
            return StartupConfigureServices(services);
        }

        public IServiceProvider StartupConfigureServices(IServiceCollection services)
        {
            var logger = _loggerFactory.CreateLogger<Startup>();

            if (_env.IsDevelopment())
            {
                // Development service configuration
                logger.LogInformation("Development environment");
            }
            else
            {
                // Non-development service configuration
                logger.LogInformation($"Environment: {_env.EnvironmentName}");
            }

            services.AddDbContext<TodoContext>(options =>
                options.UseInMemoryDatabase("db"));


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.Configure<MyOptions>(Configuration);
            services.Configure<MyOptionsWithDelegateConfig>(myOptions =>
            {
                myOptions.Option1 = "value1_configured_by_delegate";
                myOptions.Option2 = 500;
            });
            services.Configure<MySubOptions>(Configuration.GetSection("subsection"));
            services.Configure<MyOptions>("named_options_1", Configuration);
            services.Configure<MyOptions>("named_options_2", myOptions =>
            {
                myOptions.Option1 = "named_options_2_value_from_action";
            });


            services.AddOptions<MyOptions>().Configure(o => o.Option1 = "default");
            services.AddOptions<MyOptions>("named_options_1").Configure<OperationService>((o, s) =>
            {
                o.Option1 = "named_" + s.TransientOperation.OperationId;

            });

            services.PostConfigure<MyOptions>(myOptions =>
            {
                myOptions.Option1 = "post_configured_option1_value";
            });
            services.PostConfigure<MyOptions>("named_options_1", myOptions =>
            {
                myOptions.Option1 = "post_configured_option1_value";
            });
            services.PostConfigureAll<MyOptions>(myOptions =>
            {
                myOptions.Option1 = "post_configured_all_option1_value";
            });

            //services.ConfigureAll<MyOptions>(myOptions =>
            //{
            //    myOptions.Option1 = "ConfigureAll replacement value";
            //});

            services.AddMvc(options => options.RespectBrowserAcceptHeader = true).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddXmlSerializerFormatters();

            //services.AddScoped<IMyDependency, MyDependency>();
            //services.AddTransient<IOperationTransient, Operation>();
            //services.AddScoped<IOperationScoped, Operation>();
            //services.AddSingleton<IOperationSingleton, Operation>();
            //services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));

            services.AddTransient<OperationService, OperationService>();

            services.AddEmailSender();

            services.AddScoped<IMessageSender, EmailSender>();
            services.AddScoped<IMessageSender, SmsSender>();
            services.TryAddScoped<IMessageSender, FacebookSender>();

            services.AddScoped<SingleMessageSender>();

            services.AddSingleton<HtmlGenerator>();

            services.AddScoped<DatabaseContext>();
            services.AddSingleton<Repository>();

            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaultModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration config, TodoContext todoContext, IOptionsMonitor<MyOptions> optionsAccessor)
        {
            var option1 = optionsAccessor.CurrentValue.Option1;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            todoContext.Database.EnsureCreated();

            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "text/plain";

                await context.HttpContext.Response.WriteAsync($"Status code page, status code: {context.HttpContext.Response.StatusCode}");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMiddleware<CustomExceptionMiddleware>();

            var value = config["quote1"];

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "currency_by_code",
                    template: "/currency/{code}",
                    defaults: new { controller = "currencies", action = "view" });
                routes.MapRoute(
                    name: "convert_currencies",
                    template: "{currency}/convert/{*others}",
                    defaults: new { controller = "currencies", action = "convert" });
                routes.MapRoute(
                    name: "rate_by_code",
                    template: "{controller}/{currency}/{action}",
                    defaults: new { currency = "USD", action = "view" },
                    constraints: new { currency = new LengthRouteConstraint(3) });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureStaging(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsStaging())
            {
                throw new Exception("Not staging.");
            }

            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
