using Core.IService;
using Dto.ApiPanelDtos.CancellationPolicyDtos;
using Dto.ApiPanelDtos.FaqCategoryDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Core.StaticClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Core.Entities;
using Dto.ApiPanelDtos.FaqManagementDtos;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelCancellationPolicyController : ControllerBase
    {

        private readonly ICancellationPolicyService _cancellationPolicyService;

        public PanelCancellationPolicyController(ICancellationPolicyService cancellationPolicyService)
        {
            _cancellationPolicyService = cancellationPolicyService;
        }

        [HttpGet]
        public CustomResponseDto<List<CancellationPolicyListDto>> CancellationPolicyList()
        {
            return CustomResponseDto<List<CancellationPolicyListDto>>.Success(200, _cancellationPolicyService.GetCancellationPolicyListDtos());
        }

        [HttpPost]
        public CustomResponseNullDto AddCancellationPolicy(AddCancellationPolicyDto addCancellationPolicyDto)
        {
            _cancellationPolicyService.AddCancellationPolicy(addCancellationPolicyDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditCancellationPolicyDto> EditCancellationPolicy(Guid id)
        {
            var value = _cancellationPolicyService.GetEditCancellationPolicyDto(id);
            return CustomResponseDto<EditCancellationPolicyDto>.Success(200, value);
        }

        [HttpPost]
        public CustomResponseNullDto EditCancellationPolicy(EditCancellationPolicyDto editCancellationPolicyDto)
        {
            _cancellationPolicyService.EditCancellationPolicy(editCancellationPolicyDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditCancellationPolicyDto> LanguageEditCancellationPolicy(Guid id, int languageId)
        {

            LanguageEditCancellationPolicyDto value = _cancellationPolicyService.GetLanguageEditCancellationPolicyDto(id, languageId);

            return CustomResponseDto<LanguageEditCancellationPolicyDto>.Success(200, value);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditCancellationPolicy(LanguageEditCancellationPolicyDto languageEditCancellationPolicyDto)
        {
            _cancellationPolicyService.LanguageEditCancellationPolicy(languageEditCancellationPolicyDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleCancellationPolicyStatus(Guid id)
        {
            _cancellationPolicyService.ToggleStatus(id);
            return CustomResponseNullDto.Success(204);
        }

        //CancellationPolicy Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> CancellationPolicySelectList()
        {
            List<SelectListOptionDto> cancellationPolicySelectList = _cancellationPolicyService.GetCancellationPolicySelectList();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, cancellationPolicySelectList);
        }
    }
}
