using ASPNETCoreFundamentals.Middlewares;
using Microsoft.AspNetCore.Http;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ASPNETCoreFundamentals.Test.Middlewares
{
    public class HealthCheckMiddlewareTests
    {
        [Fact]
        public async Task ForNonMatchingRequest_CallsNextDelegate()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Path = "/somethingelse";

            var wasExecuted = false;
            RequestDelegate next = (ctx) =>
            {
                wasExecuted = true;
                return Task.CompletedTask;
            };

            var middleware = new HealthCheckMiddleware(next: next);

            // Act
            await middleware.Invoke(context);

            // Assert
            wasExecuted.ShouldBe(true);
        }

        [Fact]
        public async Task ReturnsPongBodyContent()
        {
            // Arrange
            var bodyStream = new MemoryStream();
            var context = new DefaultHttpContext();
            context.Response.Body = bodyStream;
            context.Request.Path = "/ping";

            RequestDelegate next = (ctx) => Task.CompletedTask;
            var middleware = new HealthCheckMiddleware(next: next);

            // Act
            await middleware.Invoke(context);

            string response;
            bodyStream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(bodyStream))
            {
                response = await reader.ReadToEndAsync();
            }

            // Assert
            response.ShouldBe("pong");
            context.Response.ContentType.ShouldBe("text/plain");
            context.Response.StatusCode.ShouldBe(200);
        }
    }
}
