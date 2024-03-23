using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IMany_Role_RoleTemplateService : IGenericService<Many_Role_RoleTemplate>
    {
        void SaveAdminRolePermissions(List<string> controllerNames);
    }
}
