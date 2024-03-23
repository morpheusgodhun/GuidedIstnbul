using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.BlogDtos
{
    public class LanguageEditBlogDto
    {
        public Guid BlogLanguageID { get; set; }
        public string? LanguageName { get; set; }
        public string BlogTitle { get; set; }
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public string Content1 { get; set; }
        public string? Content2 { get; set; }
        public bool SitemapInclude { get; set; }
    }
}
