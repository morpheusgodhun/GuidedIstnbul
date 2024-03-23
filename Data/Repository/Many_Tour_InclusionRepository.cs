using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Tour_InclusionRepository : GenericRepository<Many_Tour_Inclusion>, IMany_Tour_InclusionRepository
    {
        public Many_Tour_InclusionRepository(Context context) : base(context)
        {
        }
    }
}
