using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_AdditionalServiceOption_AdditionalServiceInputRepository : GenericRepository<Many_AdditionalServiceOption_AdditionalServiceInput>, IMany_AdditionalServiceOption_AdditionalServiceInputRepository
    {
        public Many_AdditionalServiceOption_AdditionalServiceInputRepository(Context context) : base(context)
        {
        }
    }
}
