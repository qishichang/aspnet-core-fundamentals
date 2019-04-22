﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Services
{
    public interface IEmailSender : IMessageSender
    {
        void SendEmail(string username);
    }
}
