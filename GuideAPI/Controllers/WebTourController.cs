using Core.Entities;
using Core.IService;
using Core.StaticValues;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebTourController : ControllerBase
    {
        private readonly ITourCategoryService _tourCategoryService;
        private readonly ITourService _tourService;
        private readonly ICustomTourRequestService _customTourRequestService;

        public WebTourController(ITourCategoryService tourCategoryService, ITourService tourService, ICustomTourRequestService customTourRequestService)
        {
            _tourCategoryService = tourCategoryService;
            _tourService = tourService;
            _customTourRequestService = customTourRequestService;
        }

        [HttpGet("{categoryID}/{languageID}")]  //Done
        public CustomResponseDto<GetCategoryTourListDto> GetCategoryTourList(Guid categoryID, int languageID)
        {
            GetCategoryTourListDto getCategoryTourListDto = _tourCategoryService.GetTourListByCategory(categoryID, languageID);

            return CustomResponseDto<GetCategoryTourListDto>.Success(200, getCategoryTourListDto);
        }


        [HttpGet("{tourId}/{languageID}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache60)]
        public async Task<CustomResponseDto<WebTourDetailDto>> TourDetail(Guid tourId, int languageId)
        {
            WebTourDetailDto webTourDetailDto = await _tourService.GetTourDetail(tourId, languageId);
            return CustomResponseDto<WebTourDetailDto>.Success(200, webTourDetailDto);
        }


        /*
         
         public async Task<CustomResponseDto<List<TourListDto>>> BestDealerTours(int languageId)
        {
            var data = await _tourService.BestDealTourListAsync(languageId);
            return CustomResponseDto<List<TourListDto>>.Success(200, data);
        }
         
         */


        [HttpGet("{languageId}")]  //Done -- tüm filtreler geliyor tour listesini kapattım
        [ResponseCache(CacheProfileName = AppConstants.Cache60)]

        public CustomResponseDto<GetTourFilterDto> GetTourFilter(int languageId)
        {
            GetTourFilterDto getTourFilterDto = _tourService.TourFilters(languageId);

            return CustomResponseDto<GetTourFilterDto>.Success(200, getTourFilterDto);
        }


        [HttpPost]
        public CustomResponseDto<GetTourFilterDto> GetFilteredTourList(GetTourFilterFormDto filters) //, int languageId
        {
            GetTourFilterDto getTourFilterDto = _tourService.TourListFiltered(filters, 1); //TODO: şuanda 1 gidiyor webden böyle gelmesini sağlamalıyım +eren

            return CustomResponseDto<GetTourFilterDto>.Success(200, getTourFilterDto);
        }

        [HttpPost]
        public CustomResponseDto<CustomMadeTourPostDto> CustomMadeTourRequest(CustomMadeTourPostDto customTour)
        {
            var result = _customTourRequestService.AddRequest(customTour);
            return CustomResponseDto<CustomMadeTourPostDto>.Success(200, result);
        }

        [HttpPost]
        public CustomResponseDto<CustomTourOfferCustomerAnswer> CustomMadeTourRequestAnswer(CustomTourOfferCustomerAnswer offerAnswer)
        {
            var result = _customTourRequestService.AddCustomerAnswer(offerAnswer);
            return CustomResponseDto<CustomTourOfferCustomerAnswer>.Success(200, result);
        }
        [HttpGet("{languageId}")]
        public async Task<CustomResponseDto<List<TourListDto>>> BestDealerTours(int languageId)
        {
            var data = await _tourService.BestDealTourListAsync(languageId);
            return CustomResponseDto<List<TourListDto>>.Success(200, data);
        }
    }
}
