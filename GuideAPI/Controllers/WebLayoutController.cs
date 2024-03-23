using Core.IService;
using Dto.ApiWebDtos.ApiToWebDtos.Layout;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebLayoutController : ControllerBase
    {

        private readonly IPageService _pageService;
        private readonly ITourCategoryService _categoryService;
        private readonly IConstantValueService _constantValueService;

        public WebLayoutController(IPageService pageService, ITourCategoryService categoryService, IConstantValueService constantValueService)
        {
            _pageService = pageService;
            _categoryService = categoryService;
            _constantValueService = constantValueService;
        }

        [HttpGet("{languageId}")]
        public async Task<CustomResponseDto<NavbarDto>> Navbar(int languageId)
        {
            var constantValues = await _constantValueService.GetConstantValueByPageNameAsync("Navbar", languageId);

           // Dictionary<static,string> pagesAndUrls = _pageService.GetDisplayNameByPageName("Home", languageId),


            NavbarDto navbarDto = new NavbarDto()
            {
                ConstantValues = constantValues,
                HomePage = _pageService.GetDisplayNameByPageName("Home", languageId),
                About = _pageService.GetDisplayNameByPageName("About", languageId),
                Service = _pageService.GetDisplayNameByPageName("Service", languageId),
                Blog = _pageService.GetDisplayNameByPageName("Blog List", languageId),
                Faq = _pageService.GetDisplayNameByPageName("Faq", languageId),
                Contact = _pageService.GetDisplayNameByPageName("Contact", languageId),
                Categories = _categoryService.GetTourCategoriesForNavbar(languageId),
                AboutSlug = _pageService.GetPageSlug("About", languageId),
                ServiceSlug = _pageService.GetPageSlug("Service", languageId),
                BlogSlug = _pageService.GetPageSlug("Blog List", languageId),
                ContactSlug = _pageService.GetPageSlug("Contact", languageId),
                FaqSlug = _pageService.GetPageSlug("Faq", languageId),
            };
            navbarDto.IstanbulTour = navbarDto.Categories.FirstOrDefault(x => x.CategoryName.ToUpper().Contains("TANBU"));
            return CustomResponseDto<NavbarDto>.Success(200, navbarDto);
        }
    }
}
