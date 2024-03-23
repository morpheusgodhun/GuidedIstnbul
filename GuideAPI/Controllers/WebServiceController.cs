using Core.IService;
using Core.StaticValues;
using Dto.ApiWebDtos.ApiToWebDtos.Service;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebServiceController : ControllerBase
    {

        private readonly IServiceService _serviceService;
        private readonly IServiceLanguageService _serviceLanguageService;
        private readonly IConstantValueService _constantValueService;
        private readonly IConstantValueLanguageService _constantValueLanguageService;
        private readonly IProductService _productService;
        private readonly IProductLanguageService _productLanguageService;
        private readonly IPageService _pageService;
        private readonly IPageLanguageService _pageLanguageService;
        private readonly IMany_Product_TagService _many_Product_TagService;
        private readonly ITagLanguageService _tagLanguageService;
        private readonly IProductImageService _productImageService;
        private readonly ISystemOptionService _systemOptionService;
        private readonly ISystemOptionLanguageService _systemOptionLanguageService;
        private readonly IMany_Product_AdditionalServiceService _many_Product_AdditionalServiceService;
        private readonly IMany_Product_AdditionalServiceOptionService _many_Product_AdditionalServiceOptionService;
        private readonly IAdditionalServiceLanguageService _additionalServiceLanguageService;
        private readonly IAdditionalServiceService _additionalServiceService;
        private readonly IAdditionalServiceOptionService _additionalServiceOptionService;
        private readonly IAdditionalServiceOptionLanguageService _additionalServiceOptionLanguageService;
        private readonly IAdditionalServiceInputService _additionalServiceInputService;
        private readonly IAdditionalServiceInputLanguageService _additionalServiceInputLanguageService;
        private readonly IMany_AdditionalServiceOption_AdditionalServiceInputService _many_AdditionalServiceOption_AdditionalServiceInputService;

        public WebServiceController(IServiceService serviceService, IServiceLanguageService serviceLanguageService, IConstantValueService constantValueService, IConstantValueLanguageService constantValueLanguageService, IProductService productService, IProductLanguageService productLanguageService, IPageService pageService, IPageLanguageService pageLanguageService, IMany_Product_TagService many_Product_TagService, ITagLanguageService tagLanguageService, IProductImageService productImageService, ISystemOptionService systemOptionService, ISystemOptionLanguageService systemOptionLanguageService, IMany_Product_AdditionalServiceService many_Product_AdditionalServiceService, IMany_Product_AdditionalServiceOptionService many_Product_AdditionalServiceOptionService, IAdditionalServiceLanguageService additionalServiceLanguageService, IAdditionalServiceService additionalServiceService, IAdditionalServiceOptionService additionalServiceOptionService, IAdditionalServiceOptionLanguageService additionalServiceOptionLanguageService, IAdditionalServiceInputService additionalServiceInputService, IAdditionalServiceInputLanguageService additionalServiceInputLanguageService, IMany_AdditionalServiceOption_AdditionalServiceInputService many_AdditionalServiceOption_AdditionalServiceInputService)
        {
            _serviceService = serviceService;
            _serviceLanguageService = serviceLanguageService;
            _constantValueService = constantValueService;
            _constantValueLanguageService = constantValueLanguageService;
            _productService = productService;
            _productLanguageService = productLanguageService;
            _pageService = pageService;
            _pageLanguageService = pageLanguageService;
            _many_Product_TagService = many_Product_TagService;
            _tagLanguageService = tagLanguageService;
            _productImageService = productImageService;
            _systemOptionService = systemOptionService;
            _systemOptionLanguageService = systemOptionLanguageService;
            _many_Product_AdditionalServiceService = many_Product_AdditionalServiceService;
            _many_Product_AdditionalServiceOptionService = many_Product_AdditionalServiceOptionService;
            _additionalServiceLanguageService = additionalServiceLanguageService;
            _additionalServiceService = additionalServiceService;
            _additionalServiceOptionService = additionalServiceOptionService;
            _additionalServiceOptionLanguageService = additionalServiceOptionLanguageService;
            _additionalServiceInputService = additionalServiceInputService;
            _additionalServiceInputLanguageService = additionalServiceInputLanguageService;
            _many_AdditionalServiceOption_AdditionalServiceInputService = many_AdditionalServiceOption_AdditionalServiceInputService;
        }

        [HttpGet("{LanguageID}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]

        public async Task<CustomResponseDto<GetServiceListDto>> GetServiceList(int LanguageID)
        {
            var constantValues = await _constantValueService.GetConstantValueByPageNameAsync("Service", LanguageID);
            var getServiceListDto = new GetServiceListDto()
            {
                BannerImagePath = _pageService.GetAll(x => x.PageName == "Service").FirstOrDefault().ImagePath,
                ConstantValues = constantValues,
                PageName = (from page in _pageService.GetAll(x => x.PageName == "Service")
                            join pageLanguage in _pageLanguageService.GetAll(x => x.LanguageId == LanguageID)
                            on page.Id equals pageLanguage.PageID
                            select pageLanguage.DisplayName).FirstOrDefault(),
                Services = _serviceService.GetServiceList(LanguageID)
            };



            //silinecek buralar not olsun diye yaazdım
            ////tüm option fiyatlarını çekip minpice olarak bireyere yazmam lazım
            //tourDetail.AdditionalServices = _additionalServiceOptionPriceService.SetOptionalPricesForTourDetail(tourDetail.AdditionalServices);


            //if (getServiceListDto.Services is not null)
            //    getServiceListDto.Services.Select(c => { c.Price = _tourPriceService.TourMinPriceForList(c.TourId).Price; return c; }).ToList();


            return CustomResponseDto<GetServiceListDto>.Success(200, getServiceListDto);
        }

        [HttpGet("{serviceID}/{LanguageID}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]

        public CustomResponseDto<GetServiceDetailDto> GetServiceDetail(Guid serviceID, int LanguageID)
        {
            GetServiceDetailDto getServiceDetailDto = _serviceService.GetServiceDetail(serviceID, LanguageID);

            return CustomResponseDto<GetServiceDetailDto>.Success(200, getServiceDetailDto);
        }


    }
}
