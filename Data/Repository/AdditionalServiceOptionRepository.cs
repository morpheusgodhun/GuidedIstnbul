using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AdditionalServiceOptionRepository : GenericRepository<AdditionalServiceOption>, IAdditionalServiceOptionRepository
    {
        public AdditionalServiceOptionRepository(Context context) : base(context)
        {
        }
    }
}
