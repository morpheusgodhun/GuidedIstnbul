using Dto.ApiPanelDtos.Driver;
using Dto.ApiPanelDtos.DriverDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.DriverManagement)]

    public class DriverController : CustomBaseController
    {
        readonly IApiHandler _apiHandler;

        public DriverController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
            _apiHandler = apiHandler;
        }

        public IActionResult Index()
        {
            string requestUrl = $"{this._url}PanelDriver/DriverList";
            CustomResponseDto<List<DriverListDto>> discountList = _apiHandler.GetApi<CustomResponseDto<List<DriverListDto>>>(requestUrl);
            return View(discountList.Data);
        }
        [HttpGet]
        public IActionResult AddDriver()
        {
            string url = _url + "PanelOtherOption/VehicleTypeSelectList";
            CustomResponseDto<List<SelectListOptionDto>> vehicleTypeSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url);

            ViewBag.VehicleTypeSelectList = vehicleTypeSelectList.Data;

            return View();
        }
        [HttpPost]
        public IActionResult AddDriver(AddDriverDto addDriverDto)
        {
            string url = _url + "PanelDriver/AddDriver";
            _apiHandler.PostApi<CustomResponseNullDto>(addDriverDto, url);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditDriver(string id)
        {
            string url = _url + "PanelOtherOption/VehicleTypeSelectList";
            CustomResponseDto<List<SelectListOptionDto>> vehicleTypeSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url);

            ViewBag.VehicleTypeSelectList = vehicleTypeSelectList.Data;

            string url2 = _url + "PanelDriver/EditDriver/" + id;
            CustomResponseDto<EditDriverDto> editDriverDto = _apiHandler.GetApi<CustomResponseDto<EditDriverDto>>(url2);

            if (editDriverDto is null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(editDriverDto.Data);
            }
        }
        [HttpPost]
        public IActionResult EditDriver(EditDriverDto editDriverDto)
        {
            string url = _url + "PanelDriver/EditDriver";
            _apiHandler.PostApi<CustomResponseNullDto>(editDriverDto, url);
            return RedirectToAction("Index");
        }
        public IActionResult ToggleDriverStatus(string id)
        {
            string url = _url + "PanelDriver/ToggleDriverStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}
