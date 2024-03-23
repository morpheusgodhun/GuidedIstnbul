using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents
{
    [ViewComponent(Name = "InfoCardViewComponent")]
    public class InfoCardViewComponent : ViewComponent
    {

        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public InfoCardViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string url = _url + "WebPageComponent/GetInfoCard/1";
            CustomResponseDto<GetInfoCardDto> getInfoCardDto = await _apiHandler.GetAsync<CustomResponseDto<GetInfoCardDto>>(url);

            if (getInfoCardDto is null)
            {
                return View();
            }
            else
            {
                return View(getInfoCardDto.Data);
            }
        }
    }
}
