using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class RolePermissionDto
    {
        public string RoleTemplateName { get; set; }
        public List<string> Permissions { get; set; }
    }
}
