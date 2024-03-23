using Core.StaticClass;
using Core.StaticValues;
using Data;
using Dto;
using GuideWeb.APIHandler;
using Service.Services;
using System.IO;

namespace GuideWeb.Helpers.Routing
{
    public class MyRouteHandler : IRouter
    {
        readonly IRouter _router;
        readonly RouteHelper _routeHelper;
        public MyRouteHandler(IRouter router, IConfiguration configuration)
        {
            _router = router;
            _routeHelper = new RouteHelper(configuration);

        }

        public VirtualPathData? GetVirtualPath(VirtualPathContext context)
        {
            return _router.GetVirtualPath(context);
        }

        public async Task RouteAsync(RouteContext context)
        {
            string path = context.HttpContext.Request.Path;
            var pathUrlArray = context.HttpContext.Request.Path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (path == "/")
            {
                context.RouteData.Values["controller"] = "Home";
                context.RouteData.Values["action"] = "Index";
                await _router.RouteAsync(context);
            }
            else if (path.EndsWith(".xml") && pathUrlArray.Length == 1) //urlArray length 
            {
                if (IsPathStartsWithLanguagePrefix(path))
                {
                    string xmlReplacedRedirectPath = path.Replace(path[1..3], string.Empty);
                }
                string xmlReplacedPath = path.Replace("/", string.Empty).Replace(".xml", string.Empty);
                if (!SitemapConstants.SitemapInfo.Select(x => x.Location).ToList().Contains(xmlReplacedPath))
                {
                    await CustomRouteAsync(context, new("CustomPage", "Not Found"));
                }
                else
                {
                    context.RouteData.Values["controller"] = "Home";
                    context.RouteData.Values["action"] = "Sitemap";
                    context.HttpContext.Items.Add("sitemapKey", path);
                    await _router.RouteAsync(context);
                }
            }
            else
            {
                //urlde manuel language prefixi değişirse.
                //elle tr en yaptığımızda mesela
                var languageCookieValue = context.HttpContext.Request.Cookies["language"];
                if (IsPathStartsWithLanguagePrefix(path)) //buradan sonra url her türlü languagePrefix ile başlıyor
                {
                    if (!LanguageList.AllLanguages.Select(x => x.UrlPrefix).Contains(pathUrlArray[0]))
                    {
                        await CustomRouteAsync(context, new("CustomPage", "Not Found"));
                        return;
                    }

                    string pathLanguagePrefix = path[1..3];
                    if (!string.IsNullOrEmpty(languageCookieValue)) //language cookie varsa 
                    {
                        if (!pathLanguagePrefix.Equals(languageCookieValue))
                        {
                            context.HttpContext.Response.Cookies.Delete("language");
                            context.HttpContext.Response.Cookies.Append("language", pathLanguagePrefix);
                        }
                    }
                    else //yoksa cookieye language ekle
                        context.HttpContext.Response.Cookies.Append("language", pathLanguagePrefix);

                    RouteTemplateDto routeTemplate;

                    if (CheckPathContainsGuid(path) || CheckPathContainsRouteParameter(path))
                    {
                        string pathWithouParameter = $"{pathUrlArray[0]}/{pathUrlArray[1]}";
                        routeTemplate = await _routeHelper.GetRouteTemplateAsync(pathWithouParameter);
                        if (routeTemplate is not null)
                        {
                            context.RouteData.Values["controller"] = routeTemplate.Controller;
                            context.RouteData.Values["action"] = routeTemplate.Action;
                            context.RouteData.Values["id"] = pathUrlArray[2];
                            await _router.RouteAsync(context);
                        }
                        else
                            await CustomRouteAsync(context, "CustomPage", "NotFound");
                        return;
                    }

                    routeTemplate = await _routeHelper.GetRouteTemplateAsync(path);
                    if (routeTemplate is not null)
                    {
                        await CustomRouteAsync(context, routeTemplate);
                        return;
                    }
                    else if (pathUrlArray.Length > 2 && Guid.TryParse(pathUrlArray[2], out Guid entityId))
                    {
                        routeTemplate = _routeHelper.ConvertPathToRouteTemplate(path);
                        if (routeTemplate is not null)
                            await CustomRouteAsync(context, routeTemplate);
                    }
                    else if (pathUrlArray.Length > 2 && int.TryParse(pathUrlArray[2], out int value))
                    {
                        //todo tüm routelar eklendiğinde silinecek kod
                        context.RouteData.Values["controller"] = pathUrlArray[0];
                        context.RouteData.Values["action"] = pathUrlArray[1];
                        context.RouteData.Values["languageId"] = pathUrlArray[2];

                        await _router.RouteAsync(context);
                    }
                    else
                    {
                        await CustomRouteAsync(context, "CustomPage", "NotFound");
                        return;
                    }
                }

                else
                {
                    RouteTemplateDto routeTemplate;
                    if (pathUrlArray.Length > 2 && Guid.TryParse(pathUrlArray[2], out Guid entityId))
                    {
                        routeTemplate = _routeHelper.ConvertPathToRouteTemplate(path);
                        if (routeTemplate is not null)
                        {
                            await CustomRouteAsync(context, routeTemplate);
                            return;
                        }
                    }
                    else if (pathUrlArray.Length > 2 && int.TryParse(pathUrlArray[2], out int value))
                    {
                        //todo tüm routelar eklendiğinde silinecek kod
                        context.RouteData.Values["controller"] = pathUrlArray[0];
                        context.RouteData.Values["action"] = pathUrlArray[1];
                        context.RouteData.Values["languageId"] = pathUrlArray[2];

                        await _router.RouteAsync(context);
                        return;
                    }
                    var isValidUrl = _routeHelper.IsTourRedirectUrlsContains(path);
                    if (isValidUrl)
                    {
                        string rdrUrl = path;
                        if (pathUrlArray[0] == "tour")
                        {
                            pathUrlArray = pathUrlArray.Skip(1).ToArray();
                            rdrUrl = string.Join('/', pathUrlArray);
                        }
                        string prefix = string.IsNullOrEmpty(languageCookieValue) ? LanguageList.BaseLanguage.UrlPrefix : languageCookieValue;
                        if (!rdrUrl.StartsWith('/'))
                            rdrUrl = $"/{rdrUrl}";
                        string redirectUrl1 = $"/{prefix}{rdrUrl}";

                        await CustomRouteAsync(context, "Home", "RedirectUrl", redirectUrl1, redirectPermanent: true);
                        return;
                    }
                    else
                    {
                        await CustomRouteAsync(context, "CustomPage", "NotFound");
                    }

                    return;
                }
            }
        }

        static bool CheckPathContainsGuid(string path)
        {
            var pathUrlArray = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (pathUrlArray.Length > 2)
            {
                var valToParse = pathUrlArray[2].ToString();
                return Guid.TryParse(valToParse, out Guid guid);
            }
            return false;
        }
        static bool CheckPathContainsRouteParameter(string path)
        {
            var pathUrlArray = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (pathUrlArray[1] == "category")
                return false;
            return pathUrlArray.Length > 2;
        }
        bool IsPathStartsWithLanguagePrefix(string path) => path.StartsWith("/en") || path.StartsWith("/tr"); //split edilmiş path ile check et

        async Task CustomRouteAsync(RouteContext context, RouteTemplateDto routeTemplate)
        {
            SetRouteData(context.RouteData, routeTemplate);
            if (context.HttpContext.Request.Query.Keys is not null)
            {

            }

            await _router.RouteAsync(context);
        }

        async Task CustomRouteAsync(RouteContext context, string controller, string action, string? url = null, bool redirectPermanent = false)
        {
            context.RouteData.Values["controller"] = controller;
            context.RouteData.Values["action"] = action;

            if (!string.IsNullOrEmpty(url))
                context.RouteData.Values["url"] = url;
            if (redirectPermanent)
                context.RouteData.Values["redirectPermanent"] = redirectPermanent;



            await _router.RouteAsync(context);
        }
        static void SetRouteData(RouteData routeData, RouteTemplateDto routeTemplate)
        {
            routeData.Values["controller"] = routeTemplate.Controller;
            routeData.Values["action"] = routeTemplate.Action;

            if (routeTemplate.EntityId != Guid.Empty)
                routeData.Values["id"] = routeTemplate.EntityId;

            if (routeTemplate.SeoListDtos is not null)
                routeData.Values["seoList"] = routeTemplate.SeoListDtos;


        }

        static bool DoesRequestContainsQueryParameter(HttpContext context)
        {
            if (context == null || context.Request == null)
                return false;

            return context.Request.Query.Keys.Count > 0;
        }



        /* anlas router
        async void AAAA(RouteContext context)
        {

            var url = context.HttpContext.Request.Path;
            var result = string.Empty;


            var rdUrl = ""; // "/" + result[0] + "/" + result[2];

            switch (string.Join("/", result))
            {
                case "tr/bayiler":
                    rdUrl = "/tr/bayini-bul";
                    break;
                /* case "tr/blog":
                     rdUrl = "/tr/surusunu-gelistir";
                     break;
                case "en/category/blog-en":
                    rdUrl = "/en/blog";
                    break;
                case "en/blog-news":
                    rdUrl = "/en/enhance-your-riding";
                    break;
                case "en/category/press":
                    rdUrl = "/en/basinda-biz";
                    break;
                //case "en/tyre-markings":
                //    rdUrl = "/en/lastik-sembolleri";
                //    break;

                case "en/product-categories/trailer":
                    rdUrl = "/en/trailer2";
                    break;
                case "en/product-categories/bicycle/mtb-en":
                    rdUrl = "/en/mtb";
                    break;
                case "en/product-categories/bicycle/e-bike-en":
                    rdUrl = "/en/e-bike";
                    break;
                case "en/product-categories/bicycle/city-en":
                    rdUrl = "/en/city";
                    break;
                case "en/product-categories/bisiklet/mtb":
                    rdUrl = "/tr/mtb";
                    break;
                case "en/product-categories/bisiklet/e-bike":
                    rdUrl = "/tr/e-bike";
                    break;
                case "en/product-categories/bisiklet/city":
                    rdUrl = "/tr/city";
                    break;
                case "/tr/products/speedway/":
                    rdUrl = "/tr/speedwayy";
                    break;
                case "en/products/speedway-en/":
                    rdUrl = "/en/speedwayy";
                    break;
            }


            if (result.Length > 2 || rdUrl != "")
            {
                var urlstart = (result[0]) switch { "tr" => "1", "en" => "2", _ => "0" };
                if (urlstart != "0")
                {
                    //eğer statik bir yonlendirmemiz yoksa default olarak gelen linkleri yeni haline çiviriyoruz. sadece ekstra olarak "-en" ile biten linklerde temizlik yapıyoruz
                    if (rdUrl == "")
                    {
                        rdUrl = "/" + result[0] + "/" + result[2];
                        if (result[2].Substring(result[2].Length - 3) == "-en")
                        {
                            rdUrl = "/" + result[0] + "/" + result[2].Remove(result[2].Length - 3);
                        }
                    }

                    context.RouteData.Values["controller"] = "Home";
                    context.RouteData.Values["action"] = "RedirectUrl";

                    //context.HttpContext.Response.Redirect(rdUrl);

                    context.RouteData.Values["redirectValue"] = rdUrl;
                    await _router.RouteAsync(context);

                    /*
                    //Bulunamayan sayfalar için 301 yönlendirmesi yapacağğız seo güç kaybının önüne geçmek için
                    //kategoriler
                    "en/product-categories/urban/"
                    "tr/product-categories/motosiklet-moped/"

                        //urunler
                        "en/products/capra-en/"
                        "tr/products/capra/"

                        "/tr/category/haberler/"
                        "tr/category/blog/"
                        "tr/category/basinda-biz/"

                }
                else
                {
                    context.RouteData.Values["controller"] = "Home";
                    context.RouteData.Values["action"] = "Error404";
                    await _router.RouteAsync(context);
                }

            }
            else if (result.LastOrDefault() == "" && result.Length < 2)
            {
                //İlk açılışta anasayfa için bu kod blogu çalışır bundan sonraki yönlendirmeler else alanında çalışır
                //var routeHandling = route.Routes(result[0]);
                context.RouteData.Values["controller"] = "Home";
                context.RouteData.Values["action"] = "Index";
                await _router.RouteAsync(context);
            }
            else if (result.Length == 1 && (result[0] == "en" || result[0] == "tr"))
            {
                var urllang = (result[0]) switch { "tr" => "1", "en" => "2", _ => "0" };
                if (urllang != "0")
                {
                    CookieOptions options = new CookieOptions { Expires = DateTime.Now.AddDays(2), MaxAge = TimeSpan.FromDays(2), HttpOnly = true };
                    context.HttpContext.Response.Cookies.Append("languageId", urllang, options);

                    context.RouteData.Values["controller"] = "Home";
                    context.RouteData.Values["action"] = "RedirectUrl";

                    context.RouteData.Values["redirectValue"] = "/";
                    await _router.RouteAsync(context);

                }
            }
            else
            {
                bool langError = false;
                //eğer urlden gelen ve cookieden gelen dil uymuyorsa urlden al getir
                if (result.Length == 2)
                {
                    var urllang = (result[0]) switch { "tr" => "1", "en" => "2", _ => "0" };
                    var languageId = context.HttpContext.Request.Cookies["languageId"];
                    if (urllang == "0")
                        langError = true;
                    if (languageId != urllang && urllang != "0")
                    {
                        CookieOptions options = new CookieOptions { Expires = DateTime.Now.AddDays(2), MaxAge = TimeSpan.FromDays(2), HttpOnly = true };
                        context.HttpContext.Response.Cookies.Append("languageId", urllang, options);

                        context.HttpContext.Response.Redirect(url);
                    }
                }

                //var routeUrl = url.ToString().Substring(1, url.ToString().Length - 1);

                var routeHandling = route.Routes(string.Join("/", result));

                if (routeHandling.Controller == null || langError)
                {
                    context.RouteData.Values["controller"] = "Home";
                    context.RouteData.Values["action"] = "Error404";
                }
                else
                {
                    context.RouteData.Values["controller"] = routeHandling.Controller;
                    context.RouteData.Values["action"] = routeHandling.Action;
                }

                await _router.RouteAsync(context);
            }

            //todo route not found
        */
    }
}
