using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Tour_ExclusionRepository : GenericRepository<Many_Tour_Exclusion>, IMany_Tour_ExclusionRepository
    {
        public Many_Tour_ExclusionRepository(Context context) : base(context)
        {
        }
    }
}
