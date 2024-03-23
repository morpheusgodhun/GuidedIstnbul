using Core.Entities;
using Dto.ApiWebDtos.WebToApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IBlogCommentRepository : IGenericRepository<BlogComment>
    {
        void AddBlogComment(BlogCommentPostDto blogCommentPostDto);
    }
}
