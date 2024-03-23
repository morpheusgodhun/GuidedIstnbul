using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_ReservationForm_AlsoInterestedRepository : GenericRepository<Many_ReservationForm_AlsoInterested>, IMany_ReservationForm_AlsoInterestedRepository
    {
        public Many_ReservationForm_AlsoInterestedRepository(Context context) : base(context)
        {
        }
    }
}
