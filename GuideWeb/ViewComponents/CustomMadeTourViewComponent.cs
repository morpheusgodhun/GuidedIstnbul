using Dto.ApiWebDtos.ApiToWebDtos.Home;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents
{
    [ViewComponent(Name = "CustomMadeTourViewComponent")]
    public class CustomMadeTourViewComponent : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public CustomMadeTourViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string url = _url + "WebHome/GetCustomMadeTour/1";
            CustomResponseDto<GetCustomMadeTourDto> getCustomMadeTourDto = await _apiHandler.GetAsync<CustomResponseDto<GetCustomMadeTourDto>>(url);

            if (getCustomMadeTourDto is null)
            {
                return View();
            }
            else
            {
                return View(getCustomMadeTourDto.Data);
            }
        }
    }
}
