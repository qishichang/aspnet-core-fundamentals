using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Exceptions
{
    public class BaseCustomException : Exception
    {
        private int _code;
        private string _description;

        public int Code
        {
            get => _code;
        }

        public string Descrition
        {
            get => _description;
        }

        public BaseCustomException(string message, string description, int code) : base(message)
        {
            _code = code;
            _description = description;
        }
    }
}
