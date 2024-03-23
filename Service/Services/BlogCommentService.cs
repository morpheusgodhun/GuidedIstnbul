using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiWebDtos.WebToApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BlogCommentService : GenericService<BlogComment>, IBlogCommentService
    {
        private readonly IBlogCommentRepository _blogCommentRepository;
        public BlogCommentService(IGenericRepository<BlogComment> repository, IUnitOfWork unitOfWork, IBlogCommentRepository blogCommentRepository) : base(repository, unitOfWork)
        {
            _blogCommentRepository = blogCommentRepository;
        }

        public void AddBlogComment(BlogCommentPostDto blogCommentPostDto)
        {
            _blogCommentRepository.AddBlogComment(blogCommentPostDto);
            _unitOfWork.saveChanges();
        }
    }
}
