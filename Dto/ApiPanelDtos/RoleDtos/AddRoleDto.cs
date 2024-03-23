using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.RoleDtos
{
    public class AddRoleDto
    {
        public string RoleTemplateName { get; set; }
        public List<Guid> RoleIDs { get; set; }
    }
}
