using Core.StaticValues;
using Dto.ApiPanelDtos.SupplierDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.SupplierManagement)]

    public class SupplierController : CustomBaseController
    {
        public SupplierController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = this._url + "PanelSupplier/GetSuppliers";
            var data = _apiHandler.GetApi<CustomResponseDto<List<SupplierListDto>>>(url);

            if (data is not null)
                return View(data.Data);
            else
                return View();
        }

        [HttpGet]
        public IActionResult AddSupplier()
        {
            var supplierTypeSelectList = new SupplierTypeList().SupplierTypes;
            ViewBag.SupplierTypeSelectList = supplierTypeSelectList;

            return View();
        }
        [HttpPost]
        public IActionResult AddSupplier(AddSupplierDto supplierDto)
        {
            string url = this._url + "PanelSupplier/AddSupplier";
            _apiHandler.PostApi<CustomResponseNullDto>(supplierDto, url);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditSupplier(string id)
        {
            string url = _url + "PanelSupplier/EditSupplier/" + id;
            var editDto = _apiHandler.GetApi<CustomResponseDto<EditSupplierDto>>(url);

            var supplierTypeSelectList = new SupplierTypeList().SupplierTypes;
            ViewBag.SupplierTypeSelectList = supplierTypeSelectList;

            return View(editDto.Data);
        }
        [HttpPost]
        public IActionResult EditSupplier(EditSupplierDto editSupplierDto)
        {
            string url = _url + "PanelSupplier/EditSupplier";
            _apiHandler.PostApi<CustomResponseNullDto>(editSupplierDto, url);
            return RedirectToAction("Index");
        }
        public IActionResult ToggleSupplierStatus(string id)
        {
            string url = _url + "PanelSupplier/ToggleSupplierStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");

        }
    }
}
