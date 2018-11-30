using ASPNETCoreFundamentals.Exceptions;
using ASPNETCoreFundamentals.Middlewares;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ASPNETCoreFundamentals.Test.Middlewares
{
    public class CustomExceptionMiddlewareTests
    {
        [Fact]
        public async Task WhenACustomExceptionIsRaised_CustomExceptionMiddlewareShouldBeHandleItToCustomErrorResponseAndCorrectHttpStatus()
        {
            // Arrange
            var middleware = new CustomExceptionMiddleware((innerHttpContext) =>
            {
                throw new NotFoundCustomException("Test", "Test");
            });

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Act
            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<CustomErrorResponse>(streamText);

            // Assert
            objResponse.Message.ShouldBe("Test");
            objResponse.Description.ShouldBe("Test");

            context.Response.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
        }
       
        [Fact]
        public async Task WhenAnUnExceptionExceptionIsRaised_CustomErrorResponseAndInternalServerErrorHttpStatus()
        {
            // Arrange
            var middleware = new CustomExceptionMiddleware(next: (innerHttpContext) =>
            {
                throw new Exception("Test");
            });

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Act
            await middleware.Invoke(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<CustomErrorResponse>(streamText);

            // Assert
            objResponse.Message.ShouldBe("Unexpected error");
            objResponse.Description.ShouldBe("Unexpected error");

            context.Response.StatusCode.ShouldBe((int)HttpStatusCode.InternalServerError);
        }
    }
}
