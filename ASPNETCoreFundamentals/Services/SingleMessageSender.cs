using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Services
{
    public class SingleMessageSender
    {
        private readonly IMessageSender _messageSender;

        public SingleMessageSender(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public void SendMessage(string message)
        {
            _messageSender.SendMessage(message);
        }
    }
}
