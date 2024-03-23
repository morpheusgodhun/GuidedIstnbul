using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RolePermission : BaseEntity
    {
        public RolePermission()
        {

        }
        public RolePermission(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public ICollection<Many_Role_RoleTemplate> Many_Role_RoleTemplates { get; set; }
    }
}
