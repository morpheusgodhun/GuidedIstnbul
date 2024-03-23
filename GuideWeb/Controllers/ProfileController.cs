using Dto.ApiWebDtos.ApiToWebDtos.GeneralPages;
using Dto.ApiWebDtos.ApiToWebDtos.Profile;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using FluentValidation.Results;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using GuideWeb.Validations.UserValidations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.Controllers
{
    public class ProfileController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;
        private readonly ICookie _cookie;
        readonly ChangePasswordValidator _changePasswordValidator;
        public ProfileController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload, ICookie cookie) : base(apiHandler, configuration, cookie)
        {
            _fileUpload = fileUpload;
            _cookie = cookie;
            _changePasswordValidator = new ChangePasswordValidator();
        }

        [HttpGet]
        public async Task<IActionResult> ProfileMember()
        {
            var memberId = _cookie.getMemberId();
            string url = _url + "WebProfile/GetProfileMember/" + memberId + "/1";

            string url1 = _url + "WebCustomPage/CustomPageInfo/1";
            CustomResponseDto<GetProfileUserDto> getProfileUserDto = await _apiHandler.GetAsync<CustomResponseDto<GetProfileUserDto>>(url);
            CustomResponseDto<CustomPageConstantValueDto> customPage = await _apiHandler.GetAsync<CustomResponseDto<CustomPageConstantValueDto>>(url1);

            var pageModel = (getProfileUserDto.Data, customPage.Data);

            if (getProfileUserDto is null)
            {
                return View();
            }
            else
            {
                return View(pageModel);
            }
        }

        [HttpPost]
        public IActionResult ProfileMember(ProfileUserPostDto profileUserPostDto)
        {
            // var memberId = _cookie.getMemberId(); //burada inputtan gelen guidle iş yapılmasa daha iyi olur.
            string url = _url + "WebProfile/PostProfileMember";
            _apiHandler.PostApi<CustomResponseNullDto>(profileUserPostDto, url);
            return RedirectToAction("ProfileMember", "Profile"); //TODO: Linkler Ayarlanacak
        }

        [HttpGet]
        public IActionResult ProfileGuide()
        {
            var memberId = _cookie.getMemberGuideId();
            string url = _url + "WebProfile/GetProfileGuide/" + memberId + "/1";
            CustomResponseDto<GetProfileGuideDto> getProfileGuideDto = _apiHandler.GetApi<CustomResponseDto<GetProfileGuideDto>>(url);

            if (getProfileGuideDto is null)
            {
                return View();
            }
            else
            {
                return View(getProfileGuideDto.Data);
            }
        }

        [HttpPost]
        public IActionResult ProfileGuide(ProfileGuidePostDto profileGuidePostDto, IFormFile ProfilPhoto, IFormFile LicenseFront, IFormFile LicenseBack)
        {
            var memberId = _cookie.getMemberGuideId();
            profileGuidePostDto.UserID = memberId;
            string url = _url + "WebProfile/PostProfileGuide";
            if (ProfilPhoto != null)
            {
                profileGuidePostDto.ProfilPhotoPath = _fileUpload.FileUploads(ProfilPhoto);
            }
            if (LicenseFront != null)
            {
                profileGuidePostDto.LicenseFrontImagePath = _fileUpload.FileUploads(LicenseFront);
            }
            if (LicenseBack != null)
            {
                profileGuidePostDto.LicenseBackImagePath = _fileUpload.FileUploads(LicenseBack);
            }
            _apiHandler.PostApi<CustomResponseNullDto>(profileGuidePostDto, url);
            return RedirectToAction("Index", "Faq");
        }
        [HttpGet]
        public IActionResult AgentInformation()
        {
            var memberId = _cookie.getMemberAgentId(); // c5a71994-cfe1-41e3-a1af-14f4a66490ef



            string url = _url + $"WebProfile/GetAgentInformation/{memberId}/1";
            CustomResponseDto<GetProfileAgentDto> getProfileAgentDto = _apiHandler.GetApi<CustomResponseDto<GetProfileAgentDto>>(url);

            if (getProfileAgentDto is null)
            {
                return View();
            }
            else
            {
                return View(getProfileAgentDto.Data);
            }
        }

        [HttpPost]
        public IActionResult AgentInformation(ProfileAgentPostDto profileAgentPostDto, IFormFile Logo)
        {
            var memberId = _cookie.getMemberAgentId();
            profileAgentPostDto.AgentID = memberId;
            string url = _url + "WebProfile/PostAgentInformation";
            if (Logo != null)
            {
                profileAgentPostDto.LogoPath = _fileUpload.FileUploads(Logo);
            }
            _apiHandler.PostApi<CustomResponseNullDto>(profileAgentPostDto, url);
            return RedirectToAction("Index", "Faq");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var userId = _cookie.getMemberId();
            string url = _url + $"WebProfile/GetChangePassword/{userId}/1";
            CustomResponseDto<GetChangePasswordDto> getChangePasswordDto = _apiHandler.GetApi<CustomResponseDto<GetChangePasswordDto>>(url);

            if (getChangePasswordDto is null)
            {
                return View();
            }
            else
            {
                return View(getChangePasswordDto.Data);
            }
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordPostDto changePasswordPostDto)
        {
            ValidationResult validationResult = _changePasswordValidator.Validate(changePasswordPostDto);

            if (validationResult.IsValid)
            {
                string url = _url + "WebProfile/PostChangePassword";
                var response = _apiHandler.PostApi<CustomResponseNullDto>(changePasswordPostDto, url);
                HttpContext.SignOutAsync();
                HttpContext.Response.Cookies.Delete("security-token");
                return RedirectToAction("Login", "CustomPage");
            }
            else
            {
                ViewBag.PasswordsNotMatchError = "Passwords are not matching!";

                var userId = changePasswordPostDto.UserID;
                string url = _url + $"WebProfile/GetChangePassword/{userId}/1";
                CustomResponseDto<GetChangePasswordDto> getChangePasswordDto = _apiHandler.GetApi<CustomResponseDto<GetChangePasswordDto>>(url);
                return View(getChangePasswordDto.Data);

            }
        }
    }
}
