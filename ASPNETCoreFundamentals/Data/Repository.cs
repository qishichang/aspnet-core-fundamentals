using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Data
{
    public class Repository
    {
        private readonly DatabaseContext _dataContext;

        public Repository(DatabaseContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int RowCount => _dataContext.RowCount;
    }
}
