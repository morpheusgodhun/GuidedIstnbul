using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Blog
{
    public class TrendBlogDto
    {
        public Guid BlogID { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Slug { get; set; }
    }
}
