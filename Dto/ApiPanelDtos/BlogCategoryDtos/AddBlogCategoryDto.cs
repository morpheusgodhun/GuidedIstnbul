using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.BlogCategoryDtos
{
    public class AddBlogCategoryDto
    {
        public string BlogCategoryName { get; set; }
        public string Slug { get; set; }
        public int Order { get; set; }
        public bool SitemapInclude { get; set; }
    }
}
