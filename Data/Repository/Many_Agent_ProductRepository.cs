using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Agent_ProductRepository : GenericRepository<Many_Agent_Product>, IMany_Agent_ProductRepository
    {
        public Many_Agent_ProductRepository(Context context) : base(context)
        {
        }
    }
}
