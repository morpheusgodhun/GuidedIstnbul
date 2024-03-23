using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebPageComponentController : ControllerBase
    {
        private readonly ICertificateService _certificateService;
        private readonly IPageService _pageService;
        private readonly IConstantValueService _constantValueService;
        private readonly ICommentService _commentService;
        private readonly IInfoCardService _infoCardService;
        private readonly IDestinationService _destinationService;
        private readonly ISystemOptionService _systemOptionService;
        private readonly ITourCategoryLanguageService _tourCategoryLanguageService;

        public WebPageComponentController(ICertificateService certificateService, IPageService pageService, IConstantValueService constantValueService, ICommentService commentService, IInfoCardService infoCardService, IDestinationService destinationService, ISystemOptionService systemOptionService, ITourCategoryLanguageService tourCategoryLanguageService)
        {
            _certificateService = certificateService;
            _pageService = pageService;
            _constantValueService = constantValueService;
            _commentService = commentService;
            _infoCardService = infoCardService;
            _destinationService = destinationService;
            _systemOptionService = systemOptionService;
            _tourCategoryLanguageService = tourCategoryLanguageService;
        }

        [HttpGet] //Done
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]

        public CustomResponseDto<List<CertificateDto>> GetCertificateList()
        {
            List<CertificateDto> value = (from certificate in _certificateService.GetAll(x => x.Status && !x.IsDeleted)
                                          orderby certificate.Order ascending
                                          select new CertificateDto()
                                          {
                                              Title = certificate.Title,
                                              ImagePath = certificate.ImagePath,
                                          }).ToList();

            return CustomResponseDto<List<CertificateDto>>.Success(200, value);
        }


        [HttpGet("{LanguageID}")]  //Done
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]
        public async Task<CustomResponseDto<GetVisitTurkeyCardDto>> GetVisitTurkeyCard(int LanguageID)
        {
            var getVisitTurkeyCardDto = new GetVisitTurkeyCardDto()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Visit Turkey Partial", LanguageID),
            };

            return CustomResponseDto<GetVisitTurkeyCardDto>.Success(200, getVisitTurkeyCardDto);
        }

        [HttpGet("{LanguageID}")] //Done
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]
        public async Task<CustomResponseDto<GetStartedCardDto>> GetStartedCard(int languageID)
        {
            var getStartedCardDto = new GetStartedCardDto()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Get Started Partial", languageID),
                Slug = await _tourCategoryLanguageService.GetSlugByTourCategoryAsync(Guid.Parse("0ecc705a-4b88-4113-bacc-baf8d28bc8e1"), languageID) // istanbul turları
            };

            return CustomResponseDto<GetStartedCardDto>.Success(200, getStartedCardDto);
        }

        [HttpGet("{LanguageID}")] //Done
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]
        public async Task<CustomResponseDto<GetDiscoverPotentialCardDto>> GetDiscoverPotentialCard(int languageID)
        {
            var getDiscoverPotentialCardDto = new GetDiscoverPotentialCardDto()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Discover Potential Partial", languageID),
                DiscoverPotentialSlug = await _tourCategoryLanguageService.GetSlugByTourCategoryAsync(Guid.Parse("0ecc705a-4b88-4113-bacc-baf8d28bc8e1"), languageID) // istanbul turları


            };
            return CustomResponseDto<GetDiscoverPotentialCardDto>.Success(200, getDiscoverPotentialCardDto);
        }

        [HttpGet("{LanguageID}")] //Done
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]
        public async Task<CustomResponseDto<GetInfoCardDto>> GetInfoCard(int LanguageID)
        {
            var getInfoCardDto = new GetInfoCardDto()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Info Card Partial", LanguageID),
                InfoCards = _infoCardService.GetInfoCardDtoList(LanguageID)
            };




            return CustomResponseDto<GetInfoCardDto>.Success(200, getInfoCardDto);
        }

        [HttpGet("{LanguageID}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache60)]
        public async Task<CustomResponseDto<GetFindTourFormDto>> GetFindTourForm(int LanguageID)
        {
            var getFindTourFormDto = new GetFindTourFormDto()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Tour Filter Component", LanguageID),
                WhereToOptions = _destinationService.GetDestinationSelectList(LanguageID),
                SelectMonthOptions = _systemOptionService.GetSystemOptionByCategoryId(7, LanguageID),
                TourTypeOptions = _systemOptionService.GetSystemOptionByCategoryId(1, LanguageID),
            };

            return CustomResponseDto<GetFindTourFormDto>.Success(200, getFindTourFormDto);
        }

        [HttpGet("{LanguageID}")]  //Done
        public async Task<CustomResponseDto<GetCommentListDto>> GetCommentList(int LanguageID)
        {
            var getCommentListDto = new GetCommentListDto()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Comment Partial", LanguageID),
                Comments = (from comment in _commentService.GetAll(x => !x.IsDeleted && x.Status)
                            select new CommentListItemDto()
                            {
                                CustomerName = comment.SenderName,
                                Comment = comment.CommentContent,
                                CustomerImagePath = comment.SenderImagePath,
                                Country = CountryList.Countries.FirstOrDefault(x => x.ID == comment.CountryID).Value
                            }).ToList()

            };


            return CustomResponseDto<GetCommentListDto>.Success(200, getCommentListDto);
        }

    }
}
