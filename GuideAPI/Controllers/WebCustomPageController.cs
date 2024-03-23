using Core.IService;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebCustomPageController : CustomBaseController
    {
        readonly IPageService _pageService;
        readonly IConstantValueService _constantValueService;
        public WebCustomPageController(IPageService pageService, IConstantValueService constantValueService)
        {
            _pageService = pageService;
            _constantValueService = constantValueService;
        }

        [HttpGet("{pageId}/{languageId}")]
        public CustomResponseDto<PageDto> GetPage(string pageId, int languageId)
        {
            PageDto dto = _pageService.GetPageById(pageId, languageId);
            return CustomResponseDto<PageDto>.Success(200, dto);
        }

        [HttpGet("{pageName}/{languageId}")]
        public CustomResponseDto<PageDto> GetSlugByPageName(string pageName, int languageId)
        {
            PageDto dto = _pageService.GetSlugByPageName(pageName, languageId);
            return CustomResponseDto<PageDto>.Success(200, dto);
        }
        //[HttpGet("{languageId}")]
        //public async Task<CustomResponseDto<FooterDto>> FooterPagesInfo(int languageId)
        //{
        //    FooterDto dto1 = new();
        //    dto1.ConstantValues = await _constantValueService.GetAllConstantValue(languageId);
        //    dto1.SlugDict.Add("applyGuideSlug", _pageService.GetSlugByPageName("Apply As Guide", languageId).Slug);
        //    dto1.SlugDict.Add("applyAgentSlug", _pageService.GetSlugByPageName("Apply As Agent", languageId).Slug);
        //    dto1.SlugDict.Add("cancellationPolicySlug", _pageService.GetSlugByPageName("Cancellation Policy", languageId).Slug);
        //    dto1.SlugDict.Add("privacyPolicySlug", _pageService.GetSlugByPageName("Privacy Policy", languageId).Slug);
        //    dto1.SlugDict.Add("reservationInquirySlug", _pageService.GetSlugByPageName("Reservation Inquiry", languageId).Slug);
        //    dto1.SlugDict.Add("termsAndConditionsSlug", _pageService.GetSlugByPageName("Terms and Conditions", languageId).Slug);

        //    return CustomResponseDto<FooterDto>.Success(200, dto1);
        //}

        //[HttpGet("{languageId}")]
        //public async Task<CustomResponseDto<TopbarDto>> TopbarPagesInfo(int languageId)
        //{
        //    TopbarDto dto1 = new();
        //    dto1.ConstantValues = await _constantValueService.GetAllConstantValue(languageId);
        //    dto1.SlugDict.Add("loginSlug", _pageService.GetSlugByPageName("Login", languageId).Slug);
        //    dto1.SlugDict.Add("registerSlug", _pageService.GetSlugByPageName("Register", languageId).Slug);
        //    dto1.SlugDict.Add("agentReservationsSlug", _pageService.GetSlugByPageName("Agent Reservations", languageId).Slug);

        //    return CustomResponseDto<TopbarDto>.Success(200, dto1);
        //}

        //[HttpGet("{languageId}")]
        //public async Task<CustomResponseDto<CustomPageConstantValueDto>> CustomPageInfo(int languageId)
        //{
        //    CustomPageConstantValueDto dto = new();
        //    dto.ConstantValues = await _constantValueService.GetAllConstantValue(languageId);
        //    dto.SlugDict.Add("loginSlug", _pageService.GetSlugByPageName("Login", languageId).Slug);
        //    dto.SlugDict.Add("registerSlug", _pageService.GetSlugByPageName("Register", languageId).Slug);
        //    dto.SlugDict.Add("forgotPasswordSlug", _pageService.GetSlugByPageName("Forgot Password", languageId).Slug);
        //    dto.SlugDict.Add("agentReservationsSlug", _pageService.GetSlugByPageName("Agent Reservations", languageId).Slug);

        //    dto.SlugDict.Add("applyGuideSlug", _pageService.GetSlugByPageName("Apply As Guide", languageId).Slug);
        //    dto.SlugDict.Add("applyAgentSlug", _pageService.GetSlugByPageName("Apply As Agent", languageId).Slug);
        //    dto.SlugDict.Add("cancellationPolicySlug", _pageService.GetSlugByPageName("Cancellation Policy", languageId).Slug);
        //    dto.SlugDict.Add("privacyPolicySlug", _pageService.GetSlugByPageName("Privacy Policy", languageId).Slug);
        //    dto.SlugDict.Add("reservationInquirySlug", _pageService.GetSlugByPageName("Reservation Inquiry", languageId).Slug);
        //    dto.SlugDict.Add("termsAndConditionsSlug", _pageService.GetSlugByPageName("Terms and Conditions", languageId).Slug);



        //    return CustomResponseDto<CustomPageConstantValueDto>.Success(200, dto);
        //}

        [HttpGet("{languageId}")]
        public async Task<CustomResponseDto<CustomPageConstantValueDto>> CustomPageInfo(int languageId)
        {
            var dto = new CustomPageConstantValueDto();
            dto.ConstantValues = await _constantValueService.GetAllConstantValue(languageId);

            var pageSlugPairs = GetPageSlugPairs();

            foreach (var pair in pageSlugPairs)
            {
                var slug = _pageService.GetSlugByPageName(pair.Key, languageId);
                dto.SlugDict.Add(pair.Value, slug.Slug);
            }

            return CustomResponseDto<CustomPageConstantValueDto>.Success(200, dto);
        }

        public Dictionary<string, string> GetPageSlugPairs()
        {
            // Key : PageName , Value : Slug(biz belirliyoruz)
            var pageSlugPairs = new Dictionary<string, string>
               {
                   { "Login", "loginSlug" },
                   { "Register", "registerSlug" },
                   //{ "Forgot Password", "forgotPasswordSlug" },
                   //{ "Agent Reservations", "agentReservationsSlug" },
                   { "Apply As Guide", "applyGuideSlug" },
                   { "Apply As Agent", "applyAgentSlug" },
                   { "Cancellation Policy", "cancellationPolicySlug" },
                   { "Privacy Policy", "privacyPolicySlug" },
                   //{ "Reservation Inquiry", "reservationInquirySlug" },
                   //{ "Terms and Conditions", "termsAndConditionsSlug" },
                   //{"MyReservation","myReservationSlug" },
                   //{"Logout","logoutSlug" },
                   {"Profile Member","profileMemberSlug" },
                   //{"Profile Guide","profileGuideSlug" },
                   //{"User Management","userManagementSlug" },
                   //{"Agent Information","agentInformationSlug" },
                   //{"Change Password","changePasswordSlug" },
                // eklenecekler
                //{"Terms Of Use","termsofUseSlug" },
               

               };

            return pageSlugPairs;
        }


    }
}
