using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Discount_TourCategoryRepository : GenericRepository<Many_Discount_TourCategory>, IMany_Discount_TourCategoryRepository
    {
        public Many_Discount_TourCategoryRepository(Context context) : base(context)
        {
        }
    }
}
