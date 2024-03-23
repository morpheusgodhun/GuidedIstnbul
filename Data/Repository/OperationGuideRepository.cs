using Core.Entities.Operation;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class OperationGuideRepository : GenericRepository<OperationGuide>, IOperationGuideRepository
    {
        public OperationGuideRepository(Context context) : base(context)
        {
        }
    }
}
