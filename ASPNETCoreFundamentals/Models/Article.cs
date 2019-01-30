using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Models
{
    public class Article
    {
        public string AuthorName { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.Today;
        public string Title { get; set; }
        public List<ArticleSection> Sections { get; set; } = new List<ArticleSection>();

    }
}
