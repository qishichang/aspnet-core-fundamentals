using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Data
{
    public class DatabaseContext
    {
        static readonly Random _rand = new Random();
        public DatabaseContext()
        {
            RowCount = _rand.Next(1, 1_000_000_000);
        }

        public int RowCount { get; }
    }
}
