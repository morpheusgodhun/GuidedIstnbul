using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Product_TagRepository : GenericRepository<Many_Product_Tag>, IMany_Product_TagRepository
    {
        public Many_Product_TagRepository(Context context) : base(context)
        {
        }
    }
}
