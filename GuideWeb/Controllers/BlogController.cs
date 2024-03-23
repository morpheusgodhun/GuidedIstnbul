using Azure;
using Core.IService;
using Dto.ApiWebDtos.ApiToWebDtos.About;
using Dto.ApiWebDtos.ApiToWebDtos.Blog;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Security.Claims;

namespace GuideWeb.Controllers
{
    public class BlogController : CustomBaseController
    {
        private readonly IGoogleRecaptchaService _recaptchaService;
        public BlogController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie, IGoogleRecaptchaService recaptchaService) : base(apiHandler, configuration, cookie)
        {
            _recaptchaService = recaptchaService;
        }
        [HttpGet]

        //public IActionResult Index(Guid id, [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        public IActionResult Index(Guid id, [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {
            string url = string.Empty;
            if (id != Guid.Empty)
                url = _url + $"WebBlog/GetBlogListByCategoryId/{id}/{currentPage}/{pageSize}/1";
            else
                url = _url + $"WebBlog/GetBlogList/{currentPage}/{pageSize}/1";

            CustomResponseDto<GetBlogListDto> getBlogListDto = _apiHandler.GetApi<CustomResponseDto<GetBlogListDto>>(url);

            if (getBlogListDto is null)
            {
                return View();
            }
            else
            {
                return View(getBlogListDto.Data);
            }
        }
        public IActionResult BlogCategory(Guid id, [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {
            string url = string.Empty;
            if (id != Guid.Empty)
                url = _url + $"WebBlog/GetBlogListByCategoryId/{id}/{currentPage}/{pageSize}/1";
            else
                url = _url + $"WebBlog/GetBlogList/{currentPage}/{pageSize}/1";

            CustomResponseDto<GetBlogListDto> getBlogListDto = _apiHandler.GetApi<CustomResponseDto<GetBlogListDto>>(url);

            if (getBlogListDto is null)
            {
                return View("Index");
            }
            else
            {
                return View("Index", getBlogListDto.Data);
            }
        }

        public IActionResult Tag(Guid id, [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {
            string url = _url + $"WebBlog/GetBlogListByTagId/{id}/{currentPage}/{pageSize}/1";



            CustomResponseDto<GetBlogListDto> getBlogListDto = _apiHandler.GetApi<CustomResponseDto<GetBlogListDto>>(url);

            if (getBlogListDto is null)
            {
                return View();
            }
            else
            {
                return View("Index", getBlogListDto.Data);
            }
        }


        [HttpGet]
        public async Task<IActionResult> BlogDetail(string id)
        {
            var siteKey = _configuration.GetSection("GoogleRecaptcha:SiteKey").Value;
            ViewBag.Sitekey = siteKey;
            int languageId = 1;
            string url = _url + "WebBlog/GetBlogDetail/" + id + "/" + languageId;


            CustomResponseDto<GetBlogDetailDto> getBlogDetailDto = await _apiHandler.GetAsync<CustomResponseDto<GetBlogDetailDto>>(url);

            if (getBlogDetailDto is null)
            {
                return View();
            }
            else
            {
                TempData["blogSlug"] = getBlogDetailDto.Data.Slug;
                return View(getBlogDetailDto.Data);
            }
        }
        [HttpPost]
        public async Task<IActionResult> BlogComment(BlogCommentPostDto blogCommentPostDto,string CommentContent)
        {

            // TODO : Reply yapılınca AnsweredBlogCommentId değerinin doldurulması lazım. şuan null gönderiliyor
            string url = _url + "WebBlog/AddBlogComment";
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string userNameSurname = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            string userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var a = _cookie;

            blogCommentPostDto.UserID = Guid.Parse(userId);
            blogCommentPostDto.BlogID = blogCommentPostDto.BlogID;
            blogCommentPostDto.NameSurname = userNameSurname;
            blogCommentPostDto.Email = userEmail;
            blogCommentPostDto.CommentContent = CommentContent;
            string blogSlug = TempData["blogSlug"] as string;
            string redirectUrl = $"/{_cookie.GetLanguage().UrlPrefix}/{blogSlug}";


            List<string> errorMessages = new();
            var captchaResult = await _recaptchaService.CheckCaptcha2();
            if (captchaResult["error-codes"] != null)
            {
                string captchaErrorMessage = captchaResult["error-codes"].ToString();
                captchaErrorMessage = captchaErrorMessage.Replace("[", "").Replace("]", "").Replace("-", " ");
                errorMessages.Add(captchaErrorMessage);
            }



            TempData["ErrorMessages"] = errorMessages;

            if (captchaResult["error-codes"] != null)
            {
                ModelState.AddModelError("GoogleReCaptcha", "Doğrulama Başarısız");
                return Redirect(redirectUrl);
            }


            _apiHandler.PostApi<CustomResponseNullDto>(blogCommentPostDto, url);
            return Redirect(redirectUrl);
        }

    }
}
