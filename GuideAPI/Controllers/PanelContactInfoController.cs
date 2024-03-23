using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.ContactInfoDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelContactInfoController : ControllerBase
    {
        private readonly IContactInfoService _contactInfoService;

        public PanelContactInfoController(IContactInfoService contactInfoService)
        {
            _contactInfoService = contactInfoService;
        }

        [HttpGet]
        public CustomResponseDto<List<ContactInfoListDto>> ContactInfoList()
        {
            List<ContactInfoListDto> value = _contactInfoService.GetContactInfoListDtos();
            return CustomResponseDto<List<ContactInfoListDto>>.Success(200, value);
        }

        [HttpGet]
        public CustomResponseDto<List<SelectListOption>> ContactInfoTypeList()
        {
            return CustomResponseDto<List<SelectListOption>>.Success(200, new ContactInfoType().Types);
        }

        [HttpPost]
        public CustomResponseNullDto AddContactInfo(AddContactInfoDto addContactInfoDto)
        {
            _contactInfoService.AddContactInfo(addContactInfoDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditContactInfoDto> LanguageEditContactInfo(Guid Id, int languageId)
        {
            LanguageEditContactInfoDto value = _contactInfoService.GetLanguageEditContactInfoDto(Id, languageId);
            return CustomResponseDto<LanguageEditContactInfoDto>.Success(200, value);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditContactInfo(LanguageEditContactInfoDto languageEditContactInfoDto)
        {
            _contactInfoService.LanguageEditContactInfo(languageEditContactInfoDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{Id}")]
        public CustomResponseNullDto ToggleContactInfoStatus(Guid Id)
        {
            _contactInfoService.ToggleStatus(Id);
            return CustomResponseNullDto.Success(204);
        }
    }
}
