using Dto.ApiPanelDtos.ChildPolicyDtos;
using Dto.ApiPanelDtos.SubscriberDtos;
using Dto.ApiPanelDtos.VehicleDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.VehicleManagement)]
    public class VehicleController : CustomBaseController
    {
        public VehicleController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelVehicle/VehicleList";
            CustomResponseDto<List<VehicleListDto>> vehicleListDto = _apiHandler.GetApi<CustomResponseDto<List<VehicleListDto>>>(url);
            if (vehicleListDto is null)
            {
                return View();
            }
            else
            {
                return View(vehicleListDto.Data);
            }
        }

        [HttpGet]
        public IActionResult AddVehicle()
        {
            string url = _url + "PanelOtherOption/SupplierSelectListForVehicle";
            CustomResponseDto<List<SelectListOptionDto>> supplierSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url);
            ViewBag.SupplierSelectList = supplierSelectList.Data;

            string url2 = _url + "PanelOtherOption/VehicleTypeSelectList";
            CustomResponseDto<List<SelectListOptionDto>> vehicleSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url2);
            ViewBag.VehicleSelectList = vehicleSelectList.Data;

            string url3 = _url + "PanelOtherOption/DriverSelectList";
            CustomResponseDto<List<SelectListOptionDto>> driverSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url3);
            ViewBag.DriverSelectList = driverSelectList.Data;

            return View();
        }

        [HttpPost]
        public IActionResult AddVehicle(AddVehicleDto addVehicleDto)
        {
            string url = _url + "PanelVehicle/AddVehicle";
            _apiHandler.PostApi<CustomResponseNullDto>(addVehicleDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditVehicle(string id)
        {
            string url = _url + "PanelOtherOption/SupplierSelectList";
            CustomResponseDto<List<SelectListOptionDto>> supplierSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url);

            ViewBag.SupplierSelectList = supplierSelectList.Data;

            string url2 = _url + "PanelOtherOption/VehicleTypeSelectList";
            CustomResponseDto<List<SelectListOptionDto>> vehicleSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url2);

            string url3 = _url + "PanelOtherOption/DriverSelectList";
            CustomResponseDto<List<SelectListOptionDto>> driverSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url3);
            ViewBag.DriverSelectList = driverSelectList.Data;

            ViewBag.VehicleSelectList = vehicleSelectList.Data;

            string url4 = _url + "PanelVehicle/EditVehicle/" + id;
            CustomResponseDto<EditVehicleDto> editVehicleDto = _apiHandler.GetApi<CustomResponseDto<EditVehicleDto>>(url4);
            if (editVehicleDto is null)
            {
                return View();
            }
            else
            {
                return View(editVehicleDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditVehicle(EditVehicleDto editVehicleDto)
        {
            string url = _url + "PanelVehicle/EditVehicle";
            _apiHandler.PostApi<CustomResponseNullDto>(editVehicleDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleVehicleStatus(string id)
        {
            string url = _url + "PanelVehicle/ToggleVehicleStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");

        }
    }
}
