using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Middlewares
{
    public static class HealthCheckExtensions
    {
        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HealthCheckMiddleware>();
        }
    }
}
