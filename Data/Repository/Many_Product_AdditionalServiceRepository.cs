using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Product_AdditionalServiceRepository : GenericRepository<Many_Product_AdditionalService>, IMany_Product_AdditionalServiceRepository
    {
        public Many_Product_AdditionalServiceRepository(Context context) : base(context)
        {
        }
    }
}
