using Core.IService;
using Core.StaticValues;
using Dto.ApiPanelDtos.Discount;
using Dto.ApiPanelDtos.InfoCardDtos;
using Dto.ApiPanelDtos.VehicleDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.DiscountManagement)]
    public class DiscountController : CustomBaseController
    {
        private readonly IFileUpload _fileUploadHandler;

        public DiscountController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUploadHandler) : base(apiHandler, configuration)
        {
            _fileUploadHandler = fileUploadHandler;
        }
        public IActionResult Index()
        {
            string requestUrl = $"{this._url}PanelDiscount/DiscountList";
            CustomResponseDto<List<DiscountListDto>> discountList = _apiHandler.GetApi<CustomResponseDto<List<DiscountListDto>>>(requestUrl);
            return View(discountList.Data);
        }
        [HttpGet]
        public IActionResult AddDiscount()
        {
            string tourCategoryRequestUrl = $"{this._url}PanelTourCategory/TourCategorySelectList";
            CustomResponseDto<List<SelectListOptionDto>> tourCategorySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tourCategoryRequestUrl);
            ViewBag.TourCategorySelectList = tourCategorySelectList.Data;

            string toursRequestUrl = $"{this._url}PanelTour/TourSelectList";
            CustomResponseDto<List<SelectListOptionDto>> tourSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(toursRequestUrl);
            ViewBag.TourSelectList = tourSelectList.Data;

            ViewBag.DiscountTypes = new DiscountTypeSelectList().DiscountTypes;

            return View();
        }
        [HttpPost]
        public IActionResult AddDiscount(AddDiscountDto addDiscountDto, IFormFile image)
        {
            if (image is not null)
                addDiscountDto.ImagePath = _fileUploadHandler.FileUploads(image);
            string discountPostUrl = $"{this._url}PanelDiscount/AddDiscount";

            CustomResponseNullDto response = _apiHandler.PostApi<CustomResponseNullDto>(addDiscountDto, discountPostUrl);
            if (response.StatusCode == StatusCodes.Status400BadRequest)
            {
                string errorMsg = response.Errors.FirstOrDefault();
                string tourCategoryRequestUrl = $"{this._url}PanelTourCategory/TourCategorySelectList";
                CustomResponseDto<List<SelectListOptionDto>> tourCategorySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tourCategoryRequestUrl);
                ViewBag.TourCategorySelectList = tourCategorySelectList.Data;

                string toursRequestUrl = $"{this._url}PanelTour/TourSelectList";
                CustomResponseDto<List<SelectListOptionDto>> tourSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(toursRequestUrl);
                ViewBag.TourSelectList = tourSelectList.Data;

                ViewBag.DiscountTypes = new DiscountTypeSelectList().DiscountTypes;
                return View();
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteDiscount(string id)
        {
            string url = $"{this._url}PanelDiscount/DeleteDiscount/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditDiscount(string id)
        {
            string url = _url + "PanelTourCategory/TourCategorySelectList";
            CustomResponseDto<List<SelectListOptionDto>> tourCategorySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url);

            ViewBag.TourCategorySelectList = tourCategorySelectList.Data;

            string url2 = _url + "PanelTour/TourSelectList";
            CustomResponseDto<List<SelectListOptionDto>> tourSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url2);

            ViewBag.TourSelectList = tourSelectList.Data;
            ViewBag.DiscountTypes = new DiscountTypeSelectList().DiscountTypes;


            string url3 = _url + "PanelDiscount/EditDiscount/" + id;
            CustomResponseDto<EditDiscountDto> editDiscountDto = _apiHandler.GetApi<CustomResponseDto<EditDiscountDto>>(url3);
            if (editDiscountDto is null)
            {
                return View();
            }
            else
            {
                return View(editDiscountDto.Data);
            }
        }
        [HttpPost]
        public IActionResult EditDiscount(EditDiscountDto editDiscountDto)
        {
            string url = _url + "PanelDiscount/EditDiscount";
            _apiHandler.PostApi<CustomResponseNullDto>(editDiscountDto, url);
            return RedirectToAction("Index");
        }
        public IActionResult ToggleDiscountStatus(string id)
        {
            string url = _url + "PanelDiscount/ToggleDiscountStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");

        }
    }
}
