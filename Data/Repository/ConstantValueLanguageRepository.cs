using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ConstantValueLanguageRepository : GenericRepository<ConstantValueLanguageItem>, IConstantValueLanguageRepository
    {
        public ConstantValueLanguageRepository(Context context) : base(context)
        {
        }
    }
}
