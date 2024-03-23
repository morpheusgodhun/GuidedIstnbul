
using Dto.ApiWebDtos.ApiToWebDtos.Service;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.Controllers
{
    public class ServiceController : CustomBaseController
    {
        public ServiceController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie) : base(apiHandler, configuration, cookie)
        {
        }

        public IActionResult Index()
        {
            int languageId = 1;
            string url = _url + "WebService/GetServiceList/" + languageId;

            CustomResponseDto<GetServiceListDto> getServiceListDto = _apiHandler.GetApi<CustomResponseDto<GetServiceListDto>>(url);

            if (getServiceListDto == null)
            {
                return View();
            }
            else
            {
                return View(getServiceListDto.Data);
            }
        }
        public IActionResult ServiceDetail(string id)
        {

            int languageId = 1;
            string url = _url + "WebService/GetServiceDetail/" + id + "/" + languageId;

            CustomResponseDto<GetServiceDetailDto> getServiceDetailDto = _apiHandler.GetApi<CustomResponseDto<GetServiceDetailDto>>(url);

            if (getServiceDetailDto == null)
            {
                return View();
            }
            else
            {
                return View(getServiceDetailDto.Data);
            }
        }
    }
}
