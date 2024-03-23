using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiWebDtos.ApiToWebDtos.Home;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebHomeController : ControllerBase
    {
        private readonly IConstantValueService _constantValueService;
        private readonly ITourService _tourService;
        private readonly IBlogService _blogService;
        private readonly IPageService _pageService;
        private readonly ISystemOptionService _systemOptionService;
        private readonly IDestinationService _destionsService;
        private readonly IMany_Product_RoleTemplateService _many_Product_RoleTemplateService;
        private readonly IUserService _userService;
        private readonly ITourCategoryLanguageService _tourCategoryLanguageService;

        public WebHomeController(IConstantValueService constantValueService, ITourService tourService, IBlogService blogService, IPageService pageService, ISystemOptionService systemOptionService, IDestinationService destionsService, IMany_Product_RoleTemplateService many_Product_RoleTemplateService, IUserService userService, ITourCategoryLanguageService tourCategoryLanguageService)
        {
            _constantValueService = constantValueService;
            _tourService = tourService;
            _blogService = blogService;
            _pageService = pageService;
            _systemOptionService = systemOptionService;
            _destionsService = destionsService;
            _many_Product_RoleTemplateService = many_Product_RoleTemplateService;
            _userService = userService;
            _tourCategoryLanguageService = tourCategoryLanguageService;
        }

        [HttpGet("{languageId}")]  //Done
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]
        public async Task<CustomResponseDto<GetHomeDto>> GetHome(int languageId)
        {
            var bestDeals = await _tourService.BestDealTourListAsync(languageId);
            var privateTours = await _tourService.PrivateTourListAsync(languageId);
            var turkeyTours = await _tourService.TurkeyTourListAsync(languageId);

            var userId = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            List<Guid> manyTableProductIds = new();

            if (userId is not null)
            {
                var user = await _userService.GetByIdAsync(new Guid(userId));
                manyTableProductIds = (await _many_Product_RoleTemplateService.GetAllAsync(x => x.RoleTemplateId == user.RoleTemplateId)).Select(x => x.ProductId).ToList();
            }
            else
                manyTableProductIds = (await _many_Product_RoleTemplateService.GetAllAsync(x => x.RoleTemplateId == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Customer).OptionID)).Select(x => x.ProductId).ToList();

            bestDeals.RemoveAll(x => !manyTableProductIds.Contains(x.ProductID));
            privateTours.RemoveAll(x => !manyTableProductIds.Contains(x.ProductID));
            turkeyTours.RemoveAll(x => !manyTableProductIds.Contains(x.ProductID));
            var getHomeDto = new GetHomeDto()
            {

                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Home", languageId),
                BestDealTours = bestDeals,
                PrivateTours = privateTours,
                TurkeyTourPackages = turkeyTours,
                Blogs = await _blogService.HomeBlogListAsync(languageId),
                AboutPage = _pageService.GetByPageName("About", languageId),
                IstanbulToursSlug = await _tourCategoryLanguageService.GetSlugByTourCategoryAsync(Guid.Parse("0ecc705a-4b88-4113-bacc-baf8d28bc8e1"), languageId), // istanbul turları
                TurkeyToursSlug = await _tourCategoryLanguageService.GetSlugByTourCategoryAsync(Guid.Parse("e95be291-0f19-4492-b402-d4b0c27e82e7"), languageId) // türkiye turları
            };

            return CustomResponseDto<GetHomeDto>.Success(200, getHomeDto);
        }

        [HttpGet("{LanguageID}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]

        public async Task<CustomResponseDto<GetCustomMadeTourDto>> GetCustomMadeTour(int LanguageID)
        {
            GetCustomMadeTourDto getCustomMadeTourDto = new()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Home", LanguageID),
                Destinations = _destionsService.DestinationSelectListForCustomTour(LanguageID),
                Countries = CountryList.Countries,
                Languages = new LanguageSelectList().Languages,
                HowFindUs = _systemOptionService.GetSystemOptionByCategoryId(5, LanguageID),
                Interests = _systemOptionService.GetSystemOptionByCategoryId(6, LanguageID),

            };

            return CustomResponseDto<GetCustomMadeTourDto>.Success(200, getCustomMadeTourDto);
        }

    }
}
