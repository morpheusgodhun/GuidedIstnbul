using Core.Entities;
using Core.IService;
using Dto.ApiPanelDtos.SeoDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelSeoController : ControllerBase
    {

        private readonly ISeoService _seoService;

        public PanelSeoController(ISeoService seoService)
        {
            _seoService = seoService;
        }

        [HttpGet]
        public CustomResponseDto<List<SeoListDto>> SeoList()
        {
            List<SeoListDto> seoListDtos = _seoService.GetSeoListDto();
            return CustomResponseDto<List<SeoListDto>>.Success(200, seoListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddSeo(AddSeoDto addSeoDto)
        {
            _seoService.AddSeo(addSeoDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditSeoDto> EditSeo(Guid id)
        {
            EditSeoDto editSeoDto = _seoService.GetEditSeoDto(id);
            return CustomResponseDto<EditSeoDto>.Success(200, editSeoDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditSeo(EditSeoDto editSeoDto)
        {
            _seoService.EditSeo(editSeoDto);
            return CustomResponseNullDto.Success(204);
        }
    }
}
