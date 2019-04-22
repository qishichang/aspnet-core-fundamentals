using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.Controllers
{
    public class UserController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IEnumerable<IMessageSender> _messageSenders;

        public UserController(IEmailSender emailSender, IEnumerable<IMessageSender> messageSenders)
        {
            _emailSender = emailSender;
            _messageSenders = messageSenders;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterUser(string username)
        {
            _emailSender.SendEmail(username);

            foreach (var messageSender in _messageSenders)
            {
                messageSender.SendMessage(username);
            }

            return Content($"Hi {username}, thanks for signing up!");
        }
    }
}