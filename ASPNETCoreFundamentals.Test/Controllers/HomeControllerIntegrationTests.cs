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
    public class HomeControllerIntegrationTests : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public HomeControllerIntegrationTests(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task IndexReturnsHtml()
        {
            var response = await _client.GetAsync("/");
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldContain("<h1>Home page</h1>");
        }
    }
}
