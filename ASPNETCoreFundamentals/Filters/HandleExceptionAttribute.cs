using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Filters
{
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = GetErrorResponse(context.Exception);
            context.ExceptionHandled = true;
        }

        private ObjectResult GetErrorResponse(Exception ex)
        {
            var error = new
            {
                Success = false,
                Errors = new[]
                {
                   ex.Message
               }
            };

            return new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
