﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Exceptions
{
    public class NotFoundCustomException : BaseCustomException
    {
        public NotFoundCustomException(string message, string description):base(message, description, (int)HttpStatusCode.NotFound)
        {
        }
    }
}
