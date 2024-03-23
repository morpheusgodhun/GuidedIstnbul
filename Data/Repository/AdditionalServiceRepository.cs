using Core.Entities;
using Core.IRepository;
using Data.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AdditionalServiceRepository : GenericRepository<AdditionalService>, IAdditionalServiceRepository
    {
        public AdditionalServiceRepository(Context context) : base(context)
        {
        }
    }
}
