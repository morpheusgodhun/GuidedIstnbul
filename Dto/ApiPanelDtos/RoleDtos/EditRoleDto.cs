using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.RoleDtos
{
    public class EditRoleDto
    {
        public Guid RoleTemplateID { get; set; }
        public string RoleTemplateName { get; set; }
        public List<Guid> RoleIDs { get; set; }
    }
}
