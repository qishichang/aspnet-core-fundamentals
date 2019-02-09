using ASPNETCoreFundamentals.Data;
using ASPNETCoreFundamentals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.ViewComponents
{
    public class PriorityListViewComponent : ViewComponent
    {
        private readonly TodoContext _context;

        public PriorityListViewComponent(TodoContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var items = await GetItemsAsync(maxPriority, isDone);
            return View(items);
        }

        private Task<List<ToDoItem>> GetItemsAsync(int maxPriority, bool isDone) =>
            _context.Todo.Where(x => x.IsDone == isDone && x.Priority <= maxPriority).ToListAsync();
    }
}
