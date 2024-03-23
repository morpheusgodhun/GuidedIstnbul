using Dto.ApiPanelDtos.ProductDtos.TourDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace GuidePanel.ViewComponents.Tours
{
    public class TourDetailHeaderViewComponent : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;
        private readonly string _pId = string.Empty;

        public TourDetailHeaderViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];

        }
        public async Task<IViewComponentResult >InvokeAsync()
        {
            string productId = HttpContext.Request.RouteValues.Where(x => x.Key == "id").First().Value.ToString();
            string currentPage = HttpContext.Request.RouteValues.Where(x => x.Key == "action").First().Value.ToString();
            ViewBag.ProductID = productId;
            ViewBag.CurrentPage = currentPage;
            string url = _url + "PanelTour/TourDetail/" + productId;
            CustomResponseDto<TourDetailDto> tourDetailDto =  _apiHandler.GetApi<CustomResponseDto<TourDetailDto>>(url);

            string url2 = _url + "PanelOtherOption/RoleTemplateSelectList";
            CustomResponseDto<List<SelectListOptionDto>> roleTemplateSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url2);


            ViewBag.RoleTemplateSelectList = roleTemplateSelectList.Data;


            if (tourDetailDto is null)
            {
                return View();
            }
            else
            {
                return View(tourDetailDto.Data);
            }
        }
    }
}
