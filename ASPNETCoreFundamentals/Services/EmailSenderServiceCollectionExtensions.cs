using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Services
{
    public static class EmailSenderServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailSender(this IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddSingleton<NetworkClient>();
            services.AddScoped<MessageFactory>();
            services.AddSingleton(provider => new EmailServerSettings(host: "smtp.server.com", port: 25));
            return services;
        }
    }
}
