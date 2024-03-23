using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.TourDtos
{
    public class LanguageEditTourDto
    {
        public Guid TourLanguageID { get; set; }
        public string? LanguageName { get; set; }
        public Guid ProductLanguageID { get; set; }
        public string ProductName { get; set; }
        public string DisplayName { get; set; }
        public string Slug { get; set; }
        public string DurationText { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string Content { get; set; }
        public bool SitemapInclude { get; set; }

    }
}
