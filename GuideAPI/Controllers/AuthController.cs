using Core.IService;
using Dto;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMany_Role_RoleTemplateService _rolePermissionService;
        public AuthController(IUserService userService, IMany_Role_RoleTemplateService rolePermissionService)
        {
            _userService = userService;
            _rolePermissionService = rolePermissionService;
        }

        [HttpPost]
        public CustomResponseDto<string> Login(LoginPostDto loginPostDto)
        {
            string token = _userService.LoginUser(loginPostDto);
            if (token == null)
            {
                return CustomResponseDto<string>.Fail(404, "User Not Found");
            }
            else
            {
                return CustomResponseDto<string>.Success(200, token);
            }
        }
        [HttpPost]
        public CustomResponseDto<string> PanelLogin(LoginPostDto loginPostDto)
        {
            var response = _userService.PanelUserLogin(loginPostDto);
            return response;
        }
        [HttpPost]
        public CustomResponseNullDto Register(RegisterPostDto registerDto)
        {
            if (_userService.IsEmailExist(registerDto.Email))
                return CustomResponseNullDto.Fail(400, "Email Already Exist");

            _userService.AddUsers(registerDto);
            return CustomResponseNullDto.Success(200);
        }
        [HttpPost]
        public CustomResponseDto<Dictionary<string, List<string>>> GetRolePermissions()
        {
            //todo
            return new();
        }
        [HttpPost]
        public CustomResponseNullDto PostControllerNames(ControllerNamesPostDto controllerNames)
        {
            //_rolePermissionService.SaveAdminRolePermissions(controllerNames.ControllerNames);
            return CustomResponseNullDto.Success(200);
        }

    }
}
