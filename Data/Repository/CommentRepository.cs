using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.StaticValues;
using Dto.ApiPanelDtos.CommentDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        Context _context;
        DbSet<Comment> _comments;
        public CommentRepository(Context context) : base(context)
        {
            _context = context;
            _comments = _context.Comments;
        }

        public void AddComment(AddCommentDto addCommentDto)
        {
            Comment comment = new Comment()
            {
                CountryID = addCommentDto.CountryID,
                CommentContent = addCommentDto.Comment,
                SenderName = addCommentDto.FullName,
                SenderImagePath = addCommentDto.ImagePath,

            };

            _comments.Add(comment);
        }

        public void EditComment(EditCommentDto editCommentDto)
        {
            Comment comment = _comments.Find(editCommentDto.CommentID);

            comment.CommentContent = editCommentDto.Comment;
            comment.CountryID = editCommentDto.CountryID;
            comment.SenderName = editCommentDto.FullName;
            comment.SenderImagePath = editCommentDto.ImagePath;

            _comments.Update(comment);
        }

        public List<CommentListDto> GetCommentListDtos()
        {
            var commentList = (from comment in _comments.ToList()
                               where !comment.IsDeleted
                               select new CommentListDto()
                               {
                                   CommentID = comment.Id,
                                   Comment = comment.CommentContent,
                                   Country = CountryList.Countries.FirstOrDefault(x => x.ID == comment.CountryID).Value,
                                   FullName = comment.SenderName,
                                   Status = comment.Status
                               }).ToList();
            return commentList;
        }

        public EditCommentDto GetEditCommentDto(Guid id)
        {
            Comment comment = _comments.Find(id);
            EditCommentDto editCommentDto = new EditCommentDto()
            {
                CommentID = comment.Id,
                CountryID = comment.CountryID,
                Comment = comment.CommentContent,
                FullName = comment.SenderName,
                ImagePath = comment.SenderImagePath
            };

            return editCommentDto;
        }
    }
}
