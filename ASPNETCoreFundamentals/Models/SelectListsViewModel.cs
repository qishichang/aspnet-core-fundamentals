using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Models
{
    public class SelectListsViewModel
    {
        public string SelectedValue1 { get; set; }
        public string SelectedValue2 { get; set; }

        public IEnumerable<string> MultiValues { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; } = new List<SelectListItem>
        {
            new SelectListItem{Value= "csharp", Text="C#"},
            new SelectListItem{Value= "python", Text= "Python"},
            new SelectListItem{Value= "cpp", Text="C++"},
            new SelectListItem{Value= "java", Text="Java"},
            new SelectListItem{Value= "js", Text="JavaScript"},
            new SelectListItem{Value= "ruby", Text="Ruby"},
        };

        public IEnumerable<string> SelectedValues { get; set; }
        public IEnumerable<SelectListItem> ItemsWithGroup { get; set; }

        public SelectListsViewModel()
        {
            var dynamic = new SelectListGroup { Name = "Dynamic" };
            var stat = new SelectListGroup { Name = "Static" };
            ItemsWithGroup = new List<SelectListItem>
            {
                new SelectListItem{Value= "csharp", Text="C#", Group = stat},
                new SelectListItem{Value= "python", Text= "Python", Group = dynamic},
                new SelectListItem{Value= "cpp", Text="C++", Group = stat},
                new SelectListItem{Value= "java", Text="Java"},
                new SelectListItem{Value= "js", Text="JavaScript", Group = dynamic},
                new SelectListItem{Value= "ruby", Text="Ruby"},
            };
        }
    }
}
