
using Dto.ApiPanelDtos.SeoDtos;
using Dto.ApiPanelDtos.UserDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.UserManagement)]
    public class UserController : CustomBaseController
    {
        public UserController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelUser/PanelUserList";
            CustomResponseDto<List<UserListDto>> userListDto = _apiHandler.GetApi<CustomResponseDto<List<UserListDto>>>(url);
            if (userListDto is null)
            {
                return View();
            }
            else
            {
                return View(userListDto.Data);
            }
        }

        public IActionResult FindUser(string email)
        {
            string url = _url + "PanelUser/FindUser/" + email;
            CustomResponseDto<AddUserDto> response = _apiHandler.GetApi<CustomResponseDto<AddUserDto>>(url);


            string url1 = _url + "PanelRole/RoleSelectList";
            CustomResponseDto<List<SelectListOptionDto>> roleSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url1);

            ViewBag.Roles = roleSelectList.Data;
            return Json(response);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            string url = _url + "PanelRole/RoleSelectList";
            CustomResponseDto<List<SelectListOptionDto>> roleSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url);

            ViewBag.Roles = roleSelectList.Data;

            return View();
        }

        [HttpPost] //bitir
        public IActionResult AddUser(AddUserDto addUserDto)
        {
            string url = _url + "PanelUser/AddUserToPanel";
            var response = _apiHandler.PostApi<CustomResponseNullDto>(addUserDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditUser(string id)
        {
            string url1 = _url + "PanelRole/RoleSelectList";
            CustomResponseDto<List<SelectListOptionDto>> roleSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url1);

            ViewBag.Roles = roleSelectList.Data;

            string url = _url + "PanelUser/EditUser/" + id;
            CustomResponseDto<EditUserDto> editUserDto = _apiHandler.GetApi<CustomResponseDto<EditUserDto>>(url);
            if (editUserDto is null)
            {
                return View();
            }
            else
            {
                return View(editUserDto.Data);
            }
        }
        [HttpPost]
        public IActionResult EditUser(EditUserDto editUserDto)
        {
            string url = _url + "PanelUser/EditUser";
            _apiHandler.PostApi<CustomResponseNullDto>(editUserDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleUserStatus(string id)
        {
            string url = _url + "PanelUser/ToggleUserStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}
