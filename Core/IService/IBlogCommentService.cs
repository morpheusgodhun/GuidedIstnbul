using Core.Entities;
using Core.IRepository;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.SeoDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IBlogCommentService : IGenericService<BlogComment>
    {
        void AddBlogComment(BlogCommentPostDto blogCommentPostDto);
        
    }
}
