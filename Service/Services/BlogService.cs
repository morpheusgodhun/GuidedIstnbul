using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Data.Repository;
using Dto.ApiPanelDtos.BlogDtos;
using Dto.ApiPanelDtos.TagManagementDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Blog;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BlogService : GenericService<Blog>, IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IGenericRepository<Blog> repository, IUnitOfWork unitOfWork, IBlogRepository blogRepository) : base(repository, unitOfWork)
        {
            _blogRepository = blogRepository;
        }

        public List<TrendBlogDto> FaqBlogList(int languageId)
        {
            return _blogRepository.FaqBlogList(languageId);
        }

        public async Task<List<BlogListItemDto>> HomeBlogListAsync(int languageId)
        {
            return await _blogRepository.HomeBlogList(languageId);
        }
        public CustomResponseDto<List<BlogsWithNameAndIdDto>> GetBlogsWithNameAndId(int languageId)
        {
            var blogs = _blogRepository.GetBlogsWithNameAndId(languageId);
            return CustomResponseDto<List<BlogsWithNameAndIdDto>>.Success(200, blogs);
        }

        public List<BlogListItemDto> GetBlogList(int languageId)
        {
            return _blogRepository.GetBlogList(languageId);
        }

        public List<BlogListItemDto> GetBlogListPage(int currentPage, int pageSize, int languageId)
        {
            return _blogRepository.GetBlogListPage(currentPage, pageSize, languageId);
        }

        public List<BlogCommentListDto> GetBlogCommentList(Guid blogId, int languageId)
        {
            return _blogRepository.GetBlogCommentList(blogId, languageId);
        }



        //public async Task AddBlogComment(BlogCommentPostDto blogCommentPostDto)
        //{
        //    await _blogRepository.AddBlogComment(blogCommentPostDto);
        //     _unitOfWork.saveChanges();
        //}
    }
}
