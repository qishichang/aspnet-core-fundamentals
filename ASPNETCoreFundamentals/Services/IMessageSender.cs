using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Services
{
    public interface IMessageSender
    {
        void SendMessage(string message);
    }
}
