using Core.Entities;
using Core.IService;
using Core.StaticValues;
using Dto.ApiPanelDtos.CommentDtos;
using Dto.ApiWebDtos.ApiToWebDtos.About;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelCommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public PanelCommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public CustomResponseDto<List<CommentListDto>> CommentList()
        {
            List<CommentListDto> commentList = _commentService.GetCommentListDtos();
            return CustomResponseDto<List<CommentListDto>>.Success(200, commentList);
        }

        [HttpPost]
        public CustomResponseNullDto AddComment(AddCommentDto addCommentDto)
        {
            _commentService.AddComment(addCommentDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditCommentDto> EditComment(Guid id)
        {
            EditCommentDto editCommentDto = _commentService.GetEditCommentDto(id);
            return CustomResponseDto<EditCommentDto>.Success(200, editCommentDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditComment(EditCommentDto editCommentDto)
        {
            _commentService.EditComment(editCommentDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleCommentStatus(Guid id)
        {
            _commentService.ToggleStatus(id);
            return CustomResponseNullDto.Success(204);
        }
    }
}
