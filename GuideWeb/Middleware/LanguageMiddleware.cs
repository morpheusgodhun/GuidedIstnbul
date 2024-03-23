using Core.StaticClass;

namespace GuideWeb.Middleware
{
    public class LanguageMiddleware
    {
        //private readonly RequestDelegate _next;
        //public LanguageMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //}

        //public async Task Invoke(HttpContext context)
        //{
        //    var langCookie = context.Request.Cookies["language"];

        //    if (langCookie is null) //cookie yok, ilk giriş veya cookie expired oldu
        //    {
        //        KeyValuePair<string, string> languageKeyValue = new("language", LanguageList.BaseLanguage.UrlPrefix);
        //        context.Request.Cookies.Append(languageKeyValue);
        //    }
        //    await _next(context);
        //}
        //private bool IsAjaxRequest(HttpContext context)
        //{
        //    return context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        //}
    }
}
