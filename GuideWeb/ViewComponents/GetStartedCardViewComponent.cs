using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents
{
    [ViewComponent(Name = "GetStartedCardViewComponent")]
    public class GetStartedCardViewComponent:ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public GetStartedCardViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];
        }

        public IViewComponentResult Invoke()
        {
            string url = _url + "WebPageComponent/GetStartedCard/1";
            CustomResponseDto<GetStartedCardDto> getStartedCardDto = _apiHandler.GetApi<CustomResponseDto<GetStartedCardDto>>(url);

            if (getStartedCardDto is null)
            {
                return View();
            }
            else
            {
                return View(getStartedCardDto.Data);
            }
        }
    }
}
