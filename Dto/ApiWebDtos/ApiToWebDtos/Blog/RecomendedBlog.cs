using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Blog
{
    public class RecomendedBlog
    {
        public Guid BlogID { get; set; }
        public string BlogImagePath { get; set; }
        public string BlogTitle { get; set; }
        public string BlogShortDescription { get; set; }
    }
}
