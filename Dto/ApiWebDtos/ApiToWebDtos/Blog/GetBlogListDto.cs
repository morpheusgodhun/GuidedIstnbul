using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Blog
{
    public class GetBlogListDto
    {
        public PageDto BlogListPage { get; set; }
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<BlogListItemDto> Blogs { get; set; }

        // Pagination
        public PaginateDto Paginate { get; set; }
    }
}
