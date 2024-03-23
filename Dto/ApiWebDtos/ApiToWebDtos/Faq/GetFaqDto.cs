
using Dto.ApiWebDtos.ApiToWebDtos.Blog;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Faq
{
    public class GetFaqDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<FaqCategoryDto> FaqCategories { get; set; }
        public string BannerImagePath { get; set; }
        public List<TrendBlogDto> Blogs { get; set; }
    }

    public class FaqCategoryDto
    {
        public string FaqCategoryName { get; set; }
        public int Order { get; set; }
        public List<FaqDto> Faqs { get; set; }
    }

    public class FaqDto
    {
        public int Order { get; set; }
        public string Answer { get; set; }
        public string Question { get; set; }
    }
}
