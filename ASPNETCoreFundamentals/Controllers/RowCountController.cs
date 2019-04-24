using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Data;
using ASPNETCoreFundamentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.Controllers
{
    public class RowCountController : Controller
    {
        private readonly Repository _repository;
        private readonly DatabaseContext _dataContext;

        public RowCountController(Repository repository, DatabaseContext dataContext)
        {
            _repository = repository;
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var viewModel = new RowCountViewModel
            {
                RepositoryCount = _repository.RowCount,
                DataContextCount = _dataContext.RowCount
            };
            return View(viewModel);
        }
    }
}