using Dto.ApiPanelDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.TourGuideManagement)]

    public class TourGuideController : CustomBaseController
    {

        public TourGuideController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }
        [HttpGet]
        public IActionResult Index()
        {
            string url = _url + "PanelTourGuide/GetAllGuides";
            var response = _apiHandler.GetApi<CustomResponseDto<List<GetGuideDto>>>(url);
            return View(response.Data);
        }
        [HttpGet]
        public IActionResult ApproveGuide(string Id)
        {
            string url = _url + "PanelTourGuide/ApproveGuide/" + Id;
            var response = _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult RejectGuide(Guid Id)
        {
            string url = _url + "PanelTourGuide/RejectGuide/" + Id;
            var response = _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult ChangeGuideStatus(Guid Id)
        {
            string url = _url + "PanelTourGuide/ChangeGuideStatus/" + Id;
            var response = _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction(nameof(Index));
        }
    }
}
