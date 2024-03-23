
using Dto.ApiWebDtos.ApiToWebDtos.Layout;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents.Layout
{
    [ViewComponent(Name = "_NavbarPartial")]
    public class _NavbarPartial : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public _NavbarPartial(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = _configuration["BaseUrl"];
        }

        public IViewComponentResult Invoke()
        {

            int languageId = 1;

            string url = _url + "WebLayout/Navbar/" + languageId;
            CustomResponseDto<NavbarDto> navbarDto = _apiHandler.GetApi<CustomResponseDto<NavbarDto>>(url);

            if (navbarDto is null)
            {
                return View();
            }
            else
            {
                return View(navbarDto.Data);
            }
        }
    }
}
