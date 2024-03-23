using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Discount_ProductRepository : GenericRepository<Many_Discount_Product>, IMany_Discount_ProductRepository
    {
        public Many_Discount_ProductRepository(Context context) : base(context)
        {
        }
    }
}
