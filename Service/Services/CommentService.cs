using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.CommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CommentService : GenericService<Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(IGenericRepository<Comment> repository, IUnitOfWork unitOfWork, ICommentRepository commentRepository) : base(repository, unitOfWork)
        {
            _commentRepository = commentRepository;
        }

        public void AddComment(AddCommentDto addCommentDto)
        {
            _commentRepository.AddComment(addCommentDto);
            _unitOfWork.saveChanges();
        }

        public void EditComment(EditCommentDto editCommentDto)
        {
            _commentRepository.EditComment(editCommentDto);
            _unitOfWork.saveChanges();
        }

        public List<CommentListDto> GetCommentListDtos()
        {
            return _commentRepository.GetCommentListDtos();
        }

        public EditCommentDto GetEditCommentDto(Guid id)
        {
            return _commentRepository.GetEditCommentDto(id);
        }
    }
}
