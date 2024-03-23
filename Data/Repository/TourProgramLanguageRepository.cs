using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class TourProgramLanguageRepository : GenericRepository<TourProgramLanguageItem>, ITourProgramLanguageRepository
    {
        public TourProgramLanguageRepository(Context context) : base(context)
        {
        }
    }
}
