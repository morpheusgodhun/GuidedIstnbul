
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.ProductDtos.AddProductDtos;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.ProductManagement)]

    public class ProductController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;
        public ProductController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload) : base(apiHandler, configuration)
        {
            _fileUpload = fileUpload;
        }

        [HttpGet]
        public IActionResult AddProduct(string? Id)
        {
            Guid.TryParse(Id, out Guid requestId);
            string agentUrl = _url + "PanelAgent/AgentSelectList";
            CustomResponseDto<List<SelectListOptionDto>> agentSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(agentUrl);

            string cancellationUrl = _url + "PanelCancellationPolicy/CancellationPolicySelectList";
            CustomResponseDto<List<SelectListOptionDto>> cancellationPolicySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(cancellationUrl);

            string tagUrl = _url + "PanelTag/TagSelectListForProduct";
            CustomResponseDto<List<SelectListOptionDto>> tagSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tagUrl);

            ViewBag.AgentSelectList = agentSelectList.Data;
            ViewBag.CancellationPolicySelectList = cancellationPolicySelectList.Data;
            ViewBag.TagSelectList = tagSelectList.Data;
            return View(new AddProductDto() { RequestId = requestId });
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductDto addProductDto, IFormFile BannerImage, IFormFile CardImage)
        {
            if (CardImage != null)
            {
                addProductDto.CardImagePath = _fileUpload.FileUploads(CardImage);
            }
            if (BannerImage != null)
            {
                addProductDto.BannerImagePath = _fileUpload.FileUploads(BannerImage);
            }

            addProductDto.OperatorAgentID = new GIT_ID().ID;

            string url = _url + "PanelProduct/AddPorduct";
            var id = _apiHandler.PostApi<CustomResponseDto<string>>(addProductDto, url);

            if (addProductDto.IsProductTour)
            {
                return RedirectToAction("AddTour", new { id = id.Data });

            }
            else
            {
                return RedirectToAction("AddService", new { id = id.Data });
            }

        }

        [HttpGet]
        public IActionResult AddTour(string id)
        {

            string additionalServiceUrl = _url + "PanelAdditionalService/AdditionalServiceSelectList";
            CustomResponseDto<List<SelectListOptionDto>> additionalServiceSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(additionalServiceUrl);

            string tourTypeUrl = _url + "PanelSystemOption/TourTypeSelectList";
            CustomResponseDto<List<SelectListOptionDto>> tourTypeSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tourTypeUrl);

            string segmentUrl = _url + "PanelSystemOption/SegmentSelectList";
            CustomResponseDto<List<SelectListOptionDto>> segmentSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(segmentUrl);

            string cityUrl = _url + "PanelOtherOption/CitySelectList";
            CustomResponseDto<List<SelectListOption>> citySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(cityUrl);

            string destinationUrl = _url + "PanelDestination/DestinationSelectList";
            CustomResponseDto<List<SelectListOptionDto>> destinationSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(destinationUrl);

            string tourCategoryUrl = _url + "PanelTourCategory/TourCategorySelectList";
            CustomResponseDto<List<SelectListOptionDto>> tourCategorySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tourCategoryUrl);

            string startTimeUrl = _url + "PanelOtherOption/StartTimeSelectList";
            CustomResponseDto<List<SelectListOption>> startTimeSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(startTimeUrl);

            string inclusionExclusionUrl = _url + "PanelSystemOption/InclusionExclusionSelectList";
            CustomResponseDto<List<SelectListOptionDto>> inclusionExclusionSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(inclusionExclusionUrl);

            string sightToSeeUrl = _url + "PanelSystemOption/SightToSeeSelectList";
            CustomResponseDto<List<SelectListOptionDto>> sightToSeeSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(sightToSeeUrl);

            ViewBag.AdditionalServiceSelectList = additionalServiceSelectList.Data;
            ViewBag.TourTypeSelectList = tourTypeSelectList.Data;
            ViewBag.SegmentSelectList = segmentSelectList.Data;
            ViewBag.CitySelectList = citySelectList.Data;
            ViewBag.DestinationSelectList = destinationSelectList.Data;
            ViewBag.TourCategorySelectList = tourCategorySelectList.Data;
            ViewBag.StartTimeSelectList = startTimeSelectList.Data; ;
            ViewBag.InclusionExclusionSelectList = inclusionExclusionSelectList.Data;
            ViewBag.SightToSeeSelectList = sightToSeeSelectList.Data;
            ViewBag.ProductID = id;

            return View();
        }

        [HttpPost]
        public IActionResult AddTour(AddTourDto addTourDto)
        {
            if (addTourDto.IsPersonLimited)
            {
                addTourDto.IsPersonLimited = false;
                addTourDto.PersonLimit = 0;
            }
            if (addTourDto.AdditionalServices != null && addTourDto.AdditionalServices.Any())
                addTourDto.AdditionalServices.RemoveAll(service => service.AdditionalServiceID == Guid.Empty);

            if (!addTourDto.StartTimeIDs.Contains(addTourDto.SuggestedStartTimeID))
                addTourDto.SuggestedStartTimeID = addTourDto.StartTimeIDs.OrderBy(i => i).First();

            addTourDto.Duration ??= 1;

            string url = _url + "PanelProduct/AddTour";
            _apiHandler.PostApi<CustomResponseNullDto>(addTourDto, url);
            return RedirectToAction("Index", "Tour");
        }

        [HttpGet]
        public IActionResult AddService(string id)
        {
            string url = _url + "PanelAdditionalService/AdditionalServiceSelectList";
            CustomResponseDto<List<SelectListOptionDto>> additionalServiceSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url);
            ViewBag.AdditionalServiceSelectList = additionalServiceSelectList.Data;
            ViewBag.ProductID = id;
            return View();

        }

        [HttpPost]
        public IActionResult AddService(AddServiceDto addServiceDto)
        {
            string url = _url + "PanelProduct/AddService";
            _apiHandler.PostApi<CustomResponseNullDto>(addServiceDto, url);
            return RedirectToAction("Index", "Service");

        }

        public IActionResult AdditionalServiceOptionSelectList(string id)
        {
            string url = _url + "PanelAdditionalService/AdditionalServiceOptionSelectList/" + id;
            CustomResponseDto<List<SelectListOptionDto>> additionalServiceOptionSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url);
            return Json(additionalServiceOptionSelectList.Data);
        }

    }
}
