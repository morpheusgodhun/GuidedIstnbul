using Dto.ApiPanelDtos.CustomTourDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.CustomTourManagement)]

    public class CustomTourController : CustomBaseController
    {

        public CustomTourController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RequestList()
        {
            string url = _url + "PanelCustomTour/CustomTourRequestList";
            CustomResponseDto<List<CustomTourRequestListItemDto>> CustomTourRequestList = _apiHandler.GetApi<CustomResponseDto<List<CustomTourRequestListItemDto>>>(url);

            return View(CustomTourRequestList.Data);
        }

        [HttpGet]
        public IActionResult OfferList(string id)
        {
            string url = _url + $"PanelCustomTour/OfferListByRequestId/{id}";
            CustomResponseDto<List<OfferListDto>> CustomTourRequestList = _apiHandler.GetApi<CustomResponseDto<List<OfferListDto>>>(url);

            return View((CustomTourRequestList.Data, id));
        }

        [HttpGet]
        public IActionResult AddOffer(string id)
        {
            string url = _url + $"PanelCustomTour/OfferListByRequestId/{id}";
            CustomResponseDto<List<OfferListDto>> CustomTourRequestList = _apiHandler.GetApi<CustomResponseDto<List<OfferListDto>>>(url);

            return View(new AddOfferDto() { CustomTourRequestId = new Guid(id) });
        }

        [HttpPost]
        public IActionResult AddOffer(AddOfferDto offer)
        {
            string url = _url + "PanelCustomTour/AddOffer";
            _apiHandler.PostApi<CustomResponseNullDto>(offer, url);

            return Redirect("/CustomTour/OfferList/" + offer.CustomTourRequestId.ToString());
        }
        [HttpPost]
        public IActionResult SaveMailContent(SaveMailContentDto saveMailContentDto)
        {
            string url = _url + "PanelCustomTour/SaveMailContent";
            _apiHandler.PostApi<CustomResponseNullDto>(saveMailContentDto, url);
            return RedirectToAction("RequestList");
        }
    }
}
