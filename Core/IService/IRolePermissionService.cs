using Core.Entities;
using Dto.ApiPanelDtos.RoleDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IRolePermissionService : IGenericService<RolePermission>
    {
        List<RoleListDto> GetRoleListDtos();
        void AddRole(AddRoleDto addRoleDto);
        EditRoleDto GetEditRole(Guid id);
        void EditRole(EditRoleDto editRoleDto);
        List<SelectListOptionDto> RoleSelectList();
        List<SelectListOptionDto> AllowSelectList();
    }
}
