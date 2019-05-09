﻿using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Filters
{
    public class LogResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Executing!");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Executed");
        }
    }
}
