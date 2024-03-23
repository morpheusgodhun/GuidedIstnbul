using Core.Entities;
using Core.IRepository;
using Dto.ApiPanelDtos.BlogDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Blog;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IBlogService : IGenericService<Blog>
    {
        Task<List<BlogListItemDto>> HomeBlogListAsync(int languageId);
        List<BlogListItemDto> GetBlogList(int languageId);
        List<TrendBlogDto> FaqBlogList(int languageId);
        CustomResponseDto<List<BlogsWithNameAndIdDto>> GetBlogsWithNameAndId(int languageId);

        List<BlogListItemDto> GetBlogListPage(int currentPage, int pageSize, int languageId);
        List<BlogCommentListDto> GetBlogCommentList(Guid blogId, int languageId);

        //Task AddBlogComment(BlogCommentPostDto blogCommentPostDto);
    }
}
