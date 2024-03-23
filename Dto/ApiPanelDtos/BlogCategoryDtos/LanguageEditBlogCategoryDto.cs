using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.BlogCategoryDtos
{
    public class LanguageEditBlogCategoryDto
    {
        public Guid BlogCategoryLanguageItemID { get; set; }
        public string? LanguageName { get; set; }
        public string BlogCategoryName { get; set; }
        public string Slug { get; set; }
        public bool SitemapInclude {get;set;}
    }
}
