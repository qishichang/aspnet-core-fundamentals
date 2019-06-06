using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ASPNETCoreFundamentals.Test.Controllers
{
    public class HomeControllerIntegrationTests
    {
        [Fact]
        public async Task IndexReturnsHtml()
        {
            var projectRootPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "..", "..", "..", "..", "ASPNETCoreFundamentals");

            var builder = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseContentRoot(projectRootPath);

            using (var server = new TestServer(builder))
            {
                HttpClient client = server.CreateClient();

                var response = await client.GetAsync("/");

                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                content.ShouldContain("<h1>Home page</h1>");
            }
        }
    }
}
