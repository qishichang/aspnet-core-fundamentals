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
    public class HealthCheckMiddlewareIntegrationTests : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public HealthCheckMiddlewareIntegrationTests(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task StatusMiddlewareReturnsPong()
        {
            var response = await _client.GetAsync("/ping");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            content.ShouldBe("pong");
        }
    }
}
