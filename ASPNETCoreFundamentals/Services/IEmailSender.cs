using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Services
{
    public interface IEmailSender
    {
        void SendEmail(string username);
    }
}
