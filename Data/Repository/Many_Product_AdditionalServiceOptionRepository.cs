using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Product_AdditionalServiceOptionRepository : GenericRepository<Many_Product_AdditionalServiceOption>, IMany_Product_AdditionalServiceOptionRepository
    {
        public Many_Product_AdditionalServiceOptionRepository(Context context) : base(context)
        {
        }
    }
}
