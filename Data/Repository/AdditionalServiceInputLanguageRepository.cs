using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AdditionalServiceInputLanguageRepository : GenericRepository<AdditionalServiceInputLanguageItem>, IAdditionalServiceInputLanguageRepository
    {
        public AdditionalServiceInputLanguageRepository(Context context) : base(context)
        {
        }
    }
}
