using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
     public class RoleTemplate : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Many_Role_RoleTemplate> Many_Role_RoleTemplates { get; set; }

    }
}
