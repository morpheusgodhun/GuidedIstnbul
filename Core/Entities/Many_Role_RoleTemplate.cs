using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Role_RoleTemplate : BaseEntity
    {
        [ForeignKey("Role")]
        public Guid RolePermissionId { get; set; }
        public RolePermission RolePermission { get; set; }

        [ForeignKey("RoleTemplate")]
        public Guid RoleTemplateId { get; set; }
        public RoleTemplate RoleTemplate { get; set; }
    }
}
