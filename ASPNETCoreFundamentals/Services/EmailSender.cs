using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly MessageFactory _factory;
        private readonly NetworkClient _client;

        public EmailSender(MessageFactory factory, NetworkClient client)
        {
            _factory = factory;
            _client = client;
        }
        public void SendEmail(string username)
        {
            var email = _factory.Create(username);
            _client.SendEmail(email);
            Console.WriteLine($"Email sent to {username}!");
        }
    }
}
