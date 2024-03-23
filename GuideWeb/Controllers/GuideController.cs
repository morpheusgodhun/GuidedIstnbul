using Dto.ApiWebDtos.ApiToWebDtos.Agent;
using Dto.ApiWebDtos.ApiToWebDtos.Guide;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.Controllers
{
    public class GuideController : CustomBaseController
    {
        public GuideController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie) : base(apiHandler, configuration, cookie)
        {
        }

        public async Task<IActionResult> TourServed()
        {
            string url = _url + "WebTourServed/GetTourServed/1";

            CustomResponseDto<GetTourServedDto> getTourServedDto = await _apiHandler.GetAsync<CustomResponseDto<GetTourServedDto>>(url);

            if (getTourServedDto is null)
            {
                return View();
            }
            else
            {
                return View(getTourServedDto.Data);
            }
        }
    }
}
