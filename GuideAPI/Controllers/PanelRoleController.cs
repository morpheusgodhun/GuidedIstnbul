using Core.IService;
using Dto.ApiPanelDtos.RoleDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelRoleController : ControllerBase
    {
        private readonly IRolePermissionService _roleService;
        private readonly IRoleTemplateService _roleTemplateService;

        public PanelRoleController(IRolePermissionService roleService, IRoleTemplateService roleTemplateService)
        {
            _roleService = roleService;
            _roleTemplateService = roleTemplateService;
        }

        [HttpGet]
        public CustomResponseDto<List<RoleListDto>> RoleList()
        {
            List<RoleListDto> roleListDtos = _roleService.GetRoleListDtos();

            return CustomResponseDto<List<RoleListDto>>.Success(200, roleListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddRole(AddRoleDto addRoleDto)
        {
            _roleService.AddRole(addRoleDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditRoleDto> EditRole(Guid id)
        {
            EditRoleDto editRoleDto = _roleService.GetEditRole(id);
            return CustomResponseDto<EditRoleDto>.Success(200, editRoleDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditRole(EditRoleDto editRoleDto)
        {
            _roleService.EditRole(editRoleDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleRoleStatus(Guid id)
        {
            _roleTemplateService.ToggleStatus(id);
            return CustomResponseNullDto.Success(204);

        }

        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> RoleSelectList()
        {
            List<SelectListOptionDto> roleSelectList = _roleService.RoleSelectList();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, roleSelectList);
        }

        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> AllowSelectList()
        {
            List<SelectListOptionDto> roleSelectList = _roleService.AllowSelectList();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, roleSelectList);
        }
    }
}
