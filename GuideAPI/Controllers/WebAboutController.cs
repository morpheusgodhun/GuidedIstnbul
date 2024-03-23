using Core.IService;
using Dto.ApiWebDtos.ApiToWebDtos.About;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebAboutController : ControllerBase
    {
        private readonly IPageService _pageService;
        private readonly IPageLanguageService _pageLanguageService;

        public WebAboutController(IPageService pageService, IPageLanguageService pageLanguageService)
        {
            _pageService = pageService;
            _pageLanguageService = pageLanguageService;
        }

        [HttpGet("{languageID}")]
        public CustomResponseDto<GetAboutDto> GetAbout(int languageID)
        {
            var getAboutDto = new GetAboutDto()
            {
                AboutPage = _pageService.GetByPageName("About", languageID)
            };

            return CustomResponseDto<GetAboutDto>.Success(200, getAboutDto);
        }
    }
}
