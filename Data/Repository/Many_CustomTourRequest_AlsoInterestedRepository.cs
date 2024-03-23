using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ManyCustomTourRequestAlsoInterestedRepository : GenericRepository<Many_CustomTourRequest_AlsoInterested>, IMany_CustomTourRequest_AlsoInterestedRepository
    {
        public ManyCustomTourRequestAlsoInterestedRepository(Context context) : base(context)
        {
        }
    }
}
