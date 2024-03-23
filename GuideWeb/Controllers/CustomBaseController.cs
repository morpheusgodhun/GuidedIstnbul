using Core.StaticClass;
using Dto.ApiPanelDtos.SeoDtos;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GuideWeb.Controllers
{
    public class CustomBaseController : Controller
    {
        protected readonly IApiHandler _apiHandler;
        protected readonly IConfiguration _configuration;
        protected readonly ICookie _cookie;
        protected readonly string _url;

        public CustomBaseController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];
            _cookie = cookie;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!IsAjaxRequest(HttpContext))
            {
                Language language = _cookie.GetLanguage();
                if (language is null)
                {
                    language = LanguageList.BaseLanguage;
                    _cookie.SetLanguageCookie(language.Id);
                }

                ViewData["language"] = language.UrlPrefix;

                if (HttpContext.Request.RouteValues.ContainsKey("seoList"))
                {
                    var seoListValues = HttpContext.Request.RouteValues["seoList"];

                    if (seoListValues is not null && seoListValues is List<SeoListByRouteIdDto> seoList && seoList.Count > 0)
                    {
                        ViewData["seoList"] = seoList;
                    }
                }
            }
            return base.OnActionExecutionAsync(context, next);
        }
        public Language GetLanguage()
        {
            var language = _cookie.GetLanguage();
            if (language is null)
                return LanguageList.BaseLanguage;

            return language;
        }
        public override ViewResult View()
        {
            if (HttpContext.Request.Method == "GET" && !IsAjaxRequest(HttpContext))
            {
                Language language = _cookie.GetLanguage();
                if (language is null)
                {
                    language = LanguageList.BaseLanguage;
                    _cookie.SetLanguageCookie(language.Id);
                }

                ViewData["language"] = language.UrlPrefix;
            }
            return base.View();
        }
        private bool IsAjaxRequest(HttpContext context)
        {
            return context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
