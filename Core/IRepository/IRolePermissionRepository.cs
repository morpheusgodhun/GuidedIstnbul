using Core.Entities;
using Dto.ApiPanelDtos.RoleDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IRolePermissionRepository : IGenericRepository<RolePermission>
    {
        List<RoleListDto> GetRoleListDtos();
        void AddRole(AddRoleDto addRoleDto);
        EditRoleDto GetEditRole(Guid id);
        void EditRole(EditRoleDto editRoleDto);
        List<SelectListOptionDto> RoleSelectList();
        List<SelectListOptionDto> AllowSelectList();

    }
}
