using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Tour_SightToSeeRepository : GenericRepository<Many_Tour_SightToSee>, IMany_Tour_SightToSeeRepository
    {
        public Many_Tour_SightToSeeRepository(Context context) : base(context)
        {
        }
    }
}
