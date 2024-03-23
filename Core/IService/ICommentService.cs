using Core.Entities;
using Dto.ApiPanelDtos.CommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface ICommentService : IGenericService<Comment>
    {
        List<CommentListDto> GetCommentListDtos();
        void AddComment(AddCommentDto addCommentDto);
        EditCommentDto GetEditCommentDto(Guid id);
        void EditComment(EditCommentDto editCommentDto);
    }
}
