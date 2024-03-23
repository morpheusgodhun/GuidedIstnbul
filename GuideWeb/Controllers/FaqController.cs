using Dto.ApiWebDtos.ApiToWebDtos.Faq;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace GuideWeb.Controllers
{
    public class FaqController : CustomBaseController
    {
        public FaqController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie) : base(apiHandler, configuration, cookie)
        {
        }

        public async Task<IActionResult> Index()
        {
            string url = _url + "WebFaq/GetFaq/1";
            CustomResponseDto<GetFaqDto> getFaqDto = await _apiHandler.GetAsync<CustomResponseDto<GetFaqDto>>(url);

            if (getFaqDto is null)
            {
                return View();
            }
            else
            {
                return View(getFaqDto.Data);
            }
        }
    }
}
