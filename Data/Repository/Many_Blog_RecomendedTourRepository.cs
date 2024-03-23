using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Blog_RecomendedTourRepository : GenericRepository<Many_Blog_RecomendedTour>, IMany_Blog_RecomendedTourRepository
    {
        public Many_Blog_RecomendedTourRepository(Context context) : base(context)
        {
        }
    }
}
