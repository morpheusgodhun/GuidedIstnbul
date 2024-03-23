using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ReservationOptionInputRepository : GenericRepository<ReservationOptionInput>, IReservationOptionInputRepository
    {
        public ReservationOptionInputRepository(Context context) : base(context)
        {
        }
    }
}
