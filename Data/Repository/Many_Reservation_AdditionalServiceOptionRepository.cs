using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Reservation_AdditionalServiceOptionRepository : GenericRepository<Many_Reservation_AdditionalServiceOption>, IMany_Reservation_AdditionalServiceOptionRepository
    {
        public Many_Reservation_AdditionalServiceOptionRepository(Context context) : base(context)
        {
        }
    }
}
