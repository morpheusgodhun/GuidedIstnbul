using Dto.ApiWebDtos.ApiToWebDtos.Faq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.GeneralDtos
{
    public class PageDto
    {
        public string BannerImagePath { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Content { get; set; }
        public string? Slug { get; set; }
        public List<FaqCategoryDto>? FaqCategories { get; set; } //her pagede faq olmayabilir
    }


}
