using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Core
{
    public interface IMyDependency
    {
        Task WriteMessage(string message);
    }
}
