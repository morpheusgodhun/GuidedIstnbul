using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.ConstantValueDtos;
using Dto.ApiPanelDtos.FaqCategoryDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelConstantValueController : ControllerBase
    {

        private readonly IConstantValueService _constantValueService;

        public PanelConstantValueController(IConstantValueService constantValueService)
        {
            _constantValueService = constantValueService;
        }


        [HttpGet]
        public CustomResponseDto<List<ConstantValueListDto>> ConstantValueList()
        {
            var constantValueListDtos = _constantValueService.GetConstantValueListDtos();
            return CustomResponseDto<List<ConstantValueListDto>>.Success(200, constantValueListDtos);
        }


        //Will Delete
        [HttpPost]
        public CustomResponseNullDto AddConstantValue(AddConstantValueDto addConstantValueDto)
        {
            _constantValueService.AddConstantValue(addConstantValueDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditConstantValueDto> LanguageEditConstantValue(Guid id, int languageId)
        {
            var languageEditDto = _constantValueService.GetLanguageEditConstantValueDto(id, languageId);
            return CustomResponseDto<LanguageEditConstantValueDto>.Success(200, languageEditDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditConstantValue(LanguageEditConstantValueDto languageEditConstantValueDto)
        {
            _constantValueService.LanguageEditConstantValue(languageEditConstantValueDto);
            return CustomResponseNullDto.Success(204);
        }
    }
}
