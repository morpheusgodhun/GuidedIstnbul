using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Tour_TourCategoryRepository : GenericRepository<Many_Tour_TourCategory>, IMany_Tour_TourCategoryRepository
    {
        public Many_Tour_TourCategoryRepository(Context context) : base(context)
        {
        }
    }
}
