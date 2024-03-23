using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Product_RoleTemplateRepository : GenericRepository<Many_Product_RoleTemplate>, IMany_Product_RoleTemplateRepository
    {
        public Many_Product_RoleTemplateRepository(Context context) : base(context)
        {
        }
    }
}
