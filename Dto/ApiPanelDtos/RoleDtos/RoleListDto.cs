using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.RoleDtos
{
    public class RoleListDto
    {
        public Guid RoleTemplateId { get; set; }
        public bool Status { get; set; }
        public string RoleTemplateName { get; set; }
        public List<string> Roles { get; set; }
    }
}
