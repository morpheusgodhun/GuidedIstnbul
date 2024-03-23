using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents
{
    [ViewComponent(Name = "CertificateViewComponent")]
    public class CertificateViewComponent : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;
        public CertificateViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];
        }

        //public IViewComponentResult Invoke()
        //{
        //    string url = _url + "WebPageComponent/GetCertificateList";
        //    CustomResponseDto<List<CertificateDto>> certificateList = _apiHandler.GetApi<CustomResponseDto<List<CertificateDto>>>(url);

        //    if (certificateList is null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return View(certificateList.Data);
        //    }
        //}

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string url = _url + "WebPageComponent/GetCertificateList";
            CustomResponseDto<List<CertificateDto>> certificateList = await _apiHandler.GetAsync<CustomResponseDto<List<CertificateDto>>>(url);

            if (certificateList is null)
            {
                return View();
            }
            else
            {
                return View(certificateList.Data);
            }
        }
    }
}
