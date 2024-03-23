using Core.StaticValues;
using Dto.ApiWebDtos.ApiToWebDtos.Home;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using GuideWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml;

namespace GuideWeb.Controllers
{
    public class HomeController : CustomBaseController
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie) : base(apiHandler, configuration, cookie)
        {
        }

        [ResponseCache(CacheProfileName = AppConstants.Cache120)]
        public async Task<IActionResult> Index()
        {
            int languageId = 1;
            string url = _url + "WebHome/GetHome/" + languageId;

            CustomResponseDto<GetHomeDto> getHomeDto = await _apiHandler.GetAsync<CustomResponseDto<GetHomeDto>>(url);
            //getHomeDto.Data.BestDealTours = getHomeDto.Data.BestDealTours.OrderBy(x => x.Order).ToList();
            //getHomeDto.Data.PrivateTours = getHomeDto.Data.PrivateTours.OrderBy(x => x.Order).ToList();
            //getHomeDto.Data.TurkeyTourPackages = getHomeDto.Data.TurkeyTourPackages.OrderBy(x => x.Order).ToList();
            //getHomeDto.Data.BestDealTours = getHomeDto.Data.BestDealTours.OrderBy(x => x.Order).ToList();
            if (getHomeDto is null)
            {
                return View();
            }
            else
            {
                return View(getHomeDto.Data);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]

        public IActionResult ChangeLanguage(ChangeLanguagePostDto dto)
        {
            _cookie.ChangeLanguage(dto.LanguageId);
            return Ok();
        }
        public async Task<ContentResult> Sitemap()
        {
            var locationPath = HttpContext.Request.Path.Value;
            locationPath = locationPath.Replace(".xml", string.Empty).Replace("/", string.Empty);

            string endpointUrl = $"{_url}Home/GetSitemap/{locationPath}";

            var response = await _apiHandler.GetAsync<CustomResponseDto<string>>(endpointUrl);
            //Response.ContentType = "text/xml";
            //Response.ContentType = "application/xml";
            return Content(response.Data, "text/xml");
        }
        public IActionResult RedirectUrl(string url, bool redirectPermanent = false)

        {
            if (redirectPermanent)
                return RedirectPermanent(url);
            else
                return Redirect(url);
        }
    }
}