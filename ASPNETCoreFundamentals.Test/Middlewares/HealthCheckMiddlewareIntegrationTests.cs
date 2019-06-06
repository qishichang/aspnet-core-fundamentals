using ASPNETCoreFundamentals.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ASPNETCoreFundamentals.Test.Middlewares
{
    public class HealthCheckMiddlewareIntegrationTests
    {
        [Fact]
        public async Task StatusMiddlewareReturnsPong()
        {
            var hostBuilder = new WebHostBuilder()
                .UseStartup<Startup>();

            using(var server = new TestServer(hostBuilder))
            {
                HttpClient client = server.CreateClient();
                var response = await client.GetAsync("/ping");

                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                // Assert
                content.ShouldBe("pong");
            }
        }
    }
}
