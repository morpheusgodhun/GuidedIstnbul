using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents
{
    [ViewComponent(Name = "VisitTurkeyCardViewComponent")]
    public class VisitTurkeyCardViewComponent : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public VisitTurkeyCardViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];
        }

        //public async Task<IViewComponentResult> Invoke()
        //{
        //    string url = _url + "WebPageComponent/GetVisitTurkeyCard/1";
        //    CustomResponseDto<GetVisitTurkeyCardDto> getVisitTurkeyCardDto = await _apiHandler.GetAsync<CustomResponseDto<GetVisitTurkeyCardDto>>(url);

        //    if (getVisitTurkeyCardDto is null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return View(getVisitTurkeyCardDto.Data);
        //    }
        //}
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string url = _url + "WebPageComponent/GetVisitTurkeyCard/1";
            CustomResponseDto<GetVisitTurkeyCardDto> getVisitTurkeyCardDto = await _apiHandler.GetAsync<CustomResponseDto<GetVisitTurkeyCardDto>>(url);

            if (getVisitTurkeyCardDto is null)
            {
                return View();
            }
            else
            {
                return View(getVisitTurkeyCardDto.Data);
            }
        }
    }
}
