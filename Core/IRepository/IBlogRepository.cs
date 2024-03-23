using Core.Entities;
using Dto.ApiPanelDtos.BlogDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Blog;
using Dto.ApiWebDtos.WebToApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        //List<BlogListDto> GetBlogListDtos();
        //void AddBlog(AddBlogDto addBlogDto);
        //EditBlogDto GetEditBlogDto(Guid id);
        //void EditBlog(EditBlogDto editBlogDto);
        //LanguageEditBlogDto GetLanguageEditBlogDto(Guid id, int languageId);
        //void LanguageEditBlog(LanguageEditBlogDto languageEditBlogDto);
        //void ToggleShowOnFaq(Guid id);

        Task<List<BlogListItemDto>> HomeBlogList(int languageId);
        List<TrendBlogDto> FaqBlogList(int languageId);
        List<BlogsWithNameAndIdDto> GetBlogsWithNameAndId(int languageId);
        List<BlogListItemDto> GetBlogList(int languageId);
        List<BlogListItemDto> GetBlogListPage(int currentPage, int pageSize, int languageId);
        List<BlogCommentListDto> GetBlogCommentList(Guid blogId, int languageId);

        //Task AddBlogComment(BlogCommentPostDto blogCommentPostDto);

    }
}
