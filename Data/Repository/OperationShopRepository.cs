using Core.Entities.Operation;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class OperationShopRepository : GenericRepository<OperationShop>, IOperationShopRepository
    {
        public OperationShopRepository(Context context) : base(context)
        {
        }
    }
}
