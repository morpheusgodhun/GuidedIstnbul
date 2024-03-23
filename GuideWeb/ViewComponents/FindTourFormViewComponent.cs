using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents
{
    [ViewComponent(Name = "FindTourFormViewComponent")]
    public class FindTourFormViewComponent : ViewComponent
    {

        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public FindTourFormViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string url = _url + "WebPageComponent/GetFindTourForm/1";
            CustomResponseDto<GetFindTourFormDto> getFindTourFormDto = await _apiHandler.GetAsync<CustomResponseDto<GetFindTourFormDto>>(url);

            if (getFindTourFormDto is null)
            {
                return View();
            }
            else
            {
                return View(getFindTourFormDto.Data);
            }
        }
    }
}
