using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ShopRepository : GenericRepository<Shop>, IShopRepository
    {
        public ShopRepository(Context context) : base(context)
        {
        }
    }
}
