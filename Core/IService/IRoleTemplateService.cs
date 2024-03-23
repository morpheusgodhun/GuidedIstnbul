using Core.Entities;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IRoleTemplateService : IGenericService<RoleTemplate>
    {
        string GetRoleTemplateName(string roleTemplateId);
        /// <summary>
        /// string value 1 --> roleTemplateName 
        /// list<string> value 2 -> permissions
        /// </summary>
        /// <returns></returns>
        Dictionary<string, List<string>> GetRoleTemplatePermissions();
    }
}
