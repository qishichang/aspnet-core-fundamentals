using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace ASPNETCoreFundamentals.Test
{
    public class TestFixture : IDisposable
    {
        public TestFixture()
        {
            var projectRootPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "..", "..", "..", "..", "ASPNETCoreFundamentals");

            var builder = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseContentRoot(projectRootPath);

            Server = new TestServer(builder);

            Client = Server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:5000");
        }

        public TestServer Server { get; }
        public HttpClient Client { get; }

        public void Dispose()
        {
            Client?.Dispose();
            Server?.Dispose();
        }
    }
}
