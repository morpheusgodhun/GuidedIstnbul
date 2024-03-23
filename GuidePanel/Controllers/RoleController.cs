using Dto.ApiPanelDtos.RoleDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.RoleManagement)]
    public class RoleController : CustomBaseController
    {

        public RoleController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelRole/RoleList";
            CustomResponseDto<List<RoleListDto>> roleListDto = _apiHandler.GetApi<CustomResponseDto<List<RoleListDto>>>(url);
            if (roleListDto is null)
            {
                return View();
            }
            else
            {
                return View(roleListDto.Data);
            }
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            string url = _url + "PanelRole/AllowSelectList";
            CustomResponseDto<List<SelectListOptionDto>> allowSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url);

            ViewBag.AllowSelectList = allowSelectList.Data;

            return View();
        }

        [HttpPost]
        public IActionResult AddRole(AddRoleDto addRoleDto)
        {
            string url = _url + "PanelRole/AddRole";
            _apiHandler.PostApi<CustomResponseNullDto>(addRoleDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditRole(string id)
        {
            string url = _url + "PanelRole/AllowSelectList";
            CustomResponseDto<List<SelectListOptionDto>> allowSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url);

            ViewBag.AllowSelectList = allowSelectList.Data;

            string url2 = _url + "PanelRole/EditRole/" + id;
            CustomResponseDto<EditRoleDto> editRoleDto = _apiHandler.GetApi<CustomResponseDto<EditRoleDto>>(url2);
            if (editRoleDto is null)
            {
                return View();
            }
            else
            {
                return View(editRoleDto.Data);
            }
        }
        [HttpPost]
        public IActionResult EditRole(EditRoleDto editRoleDto)
        {
            string url = _url + "PanelRole/EditRole";
            _apiHandler.PostApi<CustomResponseNullDto>(editRoleDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleRoleStatus(string id)
        {
            string url = _url + "PanelRole/ToggleRoleStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}
