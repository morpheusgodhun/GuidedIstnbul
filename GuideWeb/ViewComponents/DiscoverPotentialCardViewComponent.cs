using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents
{
    [ViewComponent(Name = "DiscoverPotentialCardViewComponent")]
    public class DiscoverPotentialCardViewComponent : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public DiscoverPotentialCardViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string url = _url + "WebPageComponent/GetDiscoverPotentialCard/1";
            CustomResponseDto<GetDiscoverPotentialCardDto> getDiscoverPotentialCardDto = await _apiHandler.GetAsync<CustomResponseDto<GetDiscoverPotentialCardDto>>(url);

            if (getDiscoverPotentialCardDto is null)
            {
                return View();
            }
            else
            {
                return View(getDiscoverPotentialCardDto.Data);
            }
        }
    }
}
