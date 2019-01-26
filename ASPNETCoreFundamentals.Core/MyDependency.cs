using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Core
{
    public class MyDependency : IMyDependency
    {
        private readonly ILogger<MyDependency> _logger;

        public MyDependency(ILogger<MyDependency> logger)
        {
            _logger = logger;
        }

        public Task WriteMessage(string message)
        {
            _logger.LogInformation("MyDependency.WriteMessage called. Message: {MESSAGE}", message);
            return Task.FromResult(0);
        }
    }
}
