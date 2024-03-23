using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Tour_DestinationRepository : GenericRepository<Many_Tour_Destination>, IMany_Tour_DestinationRepository
    {
        public Many_Tour_DestinationRepository(Context context) : base(context)
        {
        }
    }
}
