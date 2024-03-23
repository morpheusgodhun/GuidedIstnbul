using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.ContactMessageDtos;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelContactMessageController : ControllerBase
    {

        private readonly IContactMessageService _contactMessageService;

        public PanelContactMessageController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        [HttpGet]
        public CustomResponseDto<List<ContactMessageListDto>> ContactMessageList()
        {
            List<ContactMessageListDto> contactMessageListDtos = _contactMessageService.GetContactMessageListDtos();
            return CustomResponseDto<List<ContactMessageListDto>>.Success(200, contactMessageListDtos);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<ContactMessageDetailDto> ContactMessageDetail(Guid id)
        {

            ContactMessageDetailDto contactMessageDetailDto = _contactMessageService.ContactMessageDetail(id);
            return CustomResponseDto<ContactMessageDetailDto>.Success(200, contactMessageDetailDto);
        }

    }
}
