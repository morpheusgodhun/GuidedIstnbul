using Core.Entities;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IMany_Role_RoleTemplateRepository : IGenericRepository<Many_Role_RoleTemplate>
    {
        List<RolePermissionDto> GetRoleTemplatePermissions();
    }
}
