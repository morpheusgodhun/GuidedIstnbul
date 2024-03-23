using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Blog
{
    public class GetBlogSidebarPartialDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<TrendBlogDto> TrendBlogs { get; set; }
        public List<CategoryDto> BlogCategories { get; set; }
        public List<TagDto> BlogTags { get; set; }


    }
}
