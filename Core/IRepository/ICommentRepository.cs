using Core.Entities;
using Dto.ApiPanelDtos.CommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        List<CommentListDto> GetCommentListDtos();
        void AddComment(AddCommentDto addCommentDto);
        EditCommentDto GetEditCommentDto(Guid id);
        void EditComment(EditCommentDto editCommentDto);

    }
}
