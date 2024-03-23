using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_TripadvisorComment_TourGuideRepository : GenericRepository<Many_TripadvisorComment_TourGuide>, IMany_TripadvisorComment_TourGuideRepository
    {
        public Many_TripadvisorComment_TourGuideRepository(Context context) : base(context)
        {
        }
    }
}
