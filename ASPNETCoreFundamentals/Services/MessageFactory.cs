using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Services
{
    public class MessageFactory
    {
        public Email Create(string emailAddress)
        {
            return new Email
            {
                Address = emailAddress,
                Message = "Thanks for signing up!"
            };
        }
    }
}
