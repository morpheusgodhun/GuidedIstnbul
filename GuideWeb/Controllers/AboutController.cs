using Dto.ApiWebDtos.ApiToWebDtos.About;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace GuideWeb.Controllers
{
    public class AboutController : CustomBaseController
    {
        public AboutController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie) : base(apiHandler, configuration, cookie)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "WebAbout/GetAbout/1";

            CustomResponseDto<GetAboutDto> getAboutDto = _apiHandler.GetApi<CustomResponseDto<GetAboutDto>>(url);

            if (getAboutDto is null)
            {
                return View();
            }
            else
            {
                return View(getAboutDto.Data);
            }
        }
    }
}
