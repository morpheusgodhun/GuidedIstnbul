using Core.StaticClass;
using Dto.ApiWebDtos.ApiToWebDtos.GeneralPages;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using GuideWeb.Validations.ApplyGuideValidations;
using GuideWeb.Validations.UserValidations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Web;

namespace GuideWeb.Controllers
{
    public class CustomPageController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;
        private readonly ApplyGuideValidator _applyGuideValidator;
        private readonly ResetPasswordValidator _resetPasswordValidator;
        public CustomPageController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload, ICookie cookie) : base(apiHandler, configuration, cookie)
        {
            _fileUpload = fileUpload;
            _applyGuideValidator = new ApplyGuideValidator();
            _resetPasswordValidator = new();
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            string url = $"{_url}WebCustomPage/GetPage/{id}/1";
            CustomResponseDto<PageDto> response = _apiHandler.GetApi<CustomResponseDto<PageDto>>(url);

            if (response is not null)
                return View(response.Data);
            else
                return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register(string? error)
        {
            var siteKey = _configuration.GetSection("GoogleRecaptcha:SiteKey").Value;
            ViewBag.Sitekey = siteKey;

            string url = _url + "WebUserPage/GetRegister/1";
            CustomResponseDto<GetRegisterDto> getRegisterDto = _apiHandler.GetApi<CustomResponseDto<GetRegisterDto>>(url);

            string url1 = _url + $"WebCustomPage/CustomPageInfo/1";
            CustomResponseDto<CustomPageConstantValueDto> results = await _apiHandler.GetAsync<CustomResponseDto<CustomPageConstantValueDto>>(url1);
            var pageModel = (getRegisterDto.Data, results.Data);
            if (getRegisterDto.Data is null)
            {
                return View();
            }
            else
            {
                return View(pageModel);
            }
        }

        [HttpPost]
        public IActionResult Register(RegisterPostDto registerPostDto)
        {
            string url = _url + "WebUserPage/PostRegister";
            _apiHandler.PostApi<CustomResponseNullDto>(registerPostDto, url);
            return RedirectToAction("Index", "Faq");
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //TODO: Eğer Loginse buralara girme diyeceğiz

            var siteKey = _configuration.GetSection("GoogleRecaptcha:SiteKey").Value;
            ViewBag.Sitekey = siteKey;

            var languageID = 1;
            string url = _url + "WebUserPage/GetLogin/" + languageID;
            CustomResponseDto<GetLoginDto> getLoginDto = await _apiHandler.GetAsync<CustomResponseDto<GetLoginDto>>(url);


            string url1 = _url + $"WebCustomPage/CustomPageInfo/1";
            CustomResponseDto<CustomPageConstantValueDto> results = await _apiHandler.GetAsync<CustomResponseDto<CustomPageConstantValueDto>>(url1);
            var pageModel = (getLoginDto.Data, results.Data);
            if (getLoginDto.Data is null)
            {
                return View();
            }
            else
            {
                return View(pageModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginPostDto loginPostDto)
        {
            string url = _url + "WebUserPage/PostLogin";
            _apiHandler.PostApi<CustomResponseNullDto>(loginPostDto, url);
            return RedirectToAction("Index", "Faq");
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            string url = _url + "WebUserPage/GetForgotPassword/1";
            CustomResponseDto<GetForgotPasswordDto> getForgotPasswordDto = await _apiHandler.GetAsync<CustomResponseDto<GetForgotPasswordDto>>(url);

            if (getForgotPasswordDto is null)
            {
                return View();
            }
            else
            {
                return View(getForgotPasswordDto.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordPostDto forgotPasswordPostDto)
        {
            string url = _url + "WebUserPage/PostForgotPassword";
            Language language = _cookie.GetLanguage();
            forgotPasswordPostDto.LanguagePrefix = language is null ? LanguageList.BaseLanguage.UrlPrefix : language.UrlPrefix;

            var response = await _apiHandler.PostAsync<CustomResponseNullDto>(forgotPasswordPostDto, url);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult ResetPasswordValidation(string id)
        {
            string url = _url + $"WebUserPage/GetResetPasswordValidation/{id}";
            CustomResponseNullDto response = _apiHandler.GetApi<CustomResponseNullDto>(url);

            if (response.StatusCode == 404)
            {
                //return RedirectToAction("Index", "Home"); //failed
                return View("NotFound");
            }
            else
            {
                //const valuelar alınabilir
                return View(new ResetPasswordValidationPostDto
                {
                    UrlCode = id
                });
            }
        }

        [HttpPost]
        public IActionResult ResetPasswordValidation(ResetPasswordValidationPostDto resetPasswordValidationPostDto)
        {
            //NameValueCollection queryStringParams = HttpUtility.ParseQueryString(Request.Path);
            //resetPasswordValidationPostDto.UrlCode = queryStringParams["code"];

            string url = _url + "WebUserPage/PostResetPasswordValidation";
            var response = _apiHandler.PostApi<CustomResponseNullDto>(resetPasswordValidationPostDto, url);
            if (response.StatusCode == 400)
            {
                ViewBag.InvalidCodeMessage = response.Errors.FirstOrDefault();
                return View();
            }
            else
            {
                string url1 = _url + $"WebUserPage/GetResetPassword/1/{resetPasswordValidationPostDto.UrlCode}";

                CustomResponseDto<GetResetPasswordDto> getResetPasswordDto = _apiHandler.GetApi<CustomResponseDto<GetResetPasswordDto>>(url1);
                return View("ResetPassword", getResetPasswordDto.Data);
            }
        }

        [HttpGet] //kullanılmıyor
        public IActionResult ResetPassword()
        {
            NameValueCollection queryStringParams = HttpUtility.ParseQueryString(Request.QueryString.Value);
            var uniqueCode = queryStringParams["code"];

            string url = _url + $"WebUserPage/GetResetPassword/1";

            CustomResponseDto<GetResetPasswordDto> getResetPasswordDto = _apiHandler.GetApi<CustomResponseDto<GetResetPasswordDto>>(url);

            if (getResetPasswordDto is null)
            {
                return View();
            }
            else
            {
                return View(getResetPasswordDto.Data);
            }
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordPostDto resetPasswordPostDto)
        {
            var validationResult = _resetPasswordValidator.Validate(resetPasswordPostDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors;
                validationErrors.ForEach(error =>
                {
                    if (error.PropertyName == "") //validator rule tanımlarken RuleFor(x => x)... olduğu için propertyName boş geliyor
                        ViewBag.PasswordMatchError = error.ErrorMessage;
                });

                var urlUniqueCode = resetPasswordPostDto.UrlCode;
                string url1 = _url + $"WebUserPage/GetResetPassword/1/{urlUniqueCode}";
                CustomResponseDto<GetResetPasswordDto> getResetPasswordDto = _apiHandler.GetApi<CustomResponseDto<GetResetPasswordDto>>(url1);
                return View(getResetPasswordDto.Data);
            }
            string url = _url + "WebUserPage/PostResetPassword";
            var response = _apiHandler.PostApi<CustomResponseNullDto>(resetPasswordPostDto, url);
            string loginRedirectUrl = $"/{_cookie.GetLanguage().UrlPrefix}/login";

            if (response.StatusCode == 404) //url geçerliliğini yitirmiş ise
            {
                return Redirect(loginRedirectUrl);
            }
            return Redirect(loginRedirectUrl);
        }

        [HttpGet]
        public IActionResult ApplyAgent()
        {
            string url = _url + "WebUserPage/GetApplyAgent/1";
            CustomResponseDto<GetApplyAgentDto> getApplyAgentDto = _apiHandler.GetApi<CustomResponseDto<GetApplyAgentDto>>(url);
            //get agent faq link
            if (getApplyAgentDto is null)
            {
                return View();
            }
            else
            {

                return View(getApplyAgentDto.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ApplyAgent(ApplyAgentPostDto applyAgentPostDto, IFormFile Logo)
        {
            string url = _url + "WebUserPage/PostApplyAgent";
            if (Logo != null)
            {
                applyAgentPostDto.LogoPath = _fileUpload.FileUploads(Logo);
            }

            _apiHandler.PostApi<CustomResponseNullDto>(applyAgentPostDto, url);
            string agentFaqRdrUrl = $"/{_cookie.GetLanguage().UrlPrefix}/faq-agent";

            return Redirect(agentFaqRdrUrl);
        }

        [HttpGet]
        public IActionResult ApplyGuide()
        {
            string url = _url + "WebUserPage/GetApplyGuide/1";
            CustomResponseDto<GetApplyGuideDto> getApplyGuideDto = _apiHandler.GetApi<CustomResponseDto<GetApplyGuideDto>>(url);
            //get guide faq link
            if (getApplyGuideDto is null)
            {
                return View();
            }
            else
            {
                return View(getApplyGuideDto.Data);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ApplyGuide(ApplyGuidePostDto applyGuidePostDto, IFormFile ProfilPhoto, IFormFile LicenseFront, IFormFile LicenseBack)
        {
            var validationResult = _applyGuideValidator.Validate(applyGuidePostDto);
            if (!validationResult.IsValid)
            {
                int index = 1;
                foreach (var error in validationResult.Errors)
                {
                    TempData[$"error{index}"] = error.ErrorMessage;
                    index++;
                }
                return RedirectToAction(nameof(ApplyGuide));
            }
            string url = _url + "WebUserPage/PostApplyGuide";
            if (ProfilPhoto != null)
            {
                applyGuidePostDto.ProfilPhotoPath = _fileUpload.FileUploads(ProfilPhoto);
            }
            if (LicenseFront != null)
            {
                applyGuidePostDto.LicenseFrontImagePath = _fileUpload.FileUploads(LicenseFront);
            }
            if (LicenseBack != null)
            {
                applyGuidePostDto.LicenseBackImagePath = _fileUpload.FileUploads(LicenseBack);
            }
            _apiHandler.PostApi<CustomResponseNullDto>(applyGuidePostDto, url);
            string agentFaqRdrUrl = $"/{_cookie.GetLanguage().UrlPrefix}/faq-guide";
            return Redirect(agentFaqRdrUrl);



        }
        [HttpGet]
        public IActionResult NotFound()
        {
            return View();
        }


    }
}
