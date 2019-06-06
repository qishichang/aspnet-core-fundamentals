using ASPNETCoreFundamentals.Middlewares;
using Microsoft.AspNetCore.Http;
using Shouldly;
using System;
using System.Collections.Generic;
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
    }
}
