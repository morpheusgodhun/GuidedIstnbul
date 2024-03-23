using Core.Entities;
using Core.IRepository;
using Dto.ApiWebDtos.WebToApiDtos;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class BlogCommentRepository : GenericRepository<BlogComment>, IBlogCommentRepository
    {
        DbSet<BlogComment> _blogComments;
        public BlogCommentRepository(Context context) : base(context)
        {
            _blogComments=_context.BlogComments;
        }

        public void AddBlogComment(BlogCommentPostDto blogCommentPostDto)
        {
            Core.Entities.BlogComment blogComment = new()
            {
                Email = blogCommentPostDto.Email,
                NameSurname = blogCommentPostDto.NameSurname,
                //ProfilePhotoPath = blogCommentPostDto.ProfilePhotoPath,
                CommentContent = blogCommentPostDto.CommentContent,
                AnsweredBlogCommentId = null,
                BlogId = blogCommentPostDto.BlogID,


            };
            _blogComments.Add(blogComment);
        }
    }
}
