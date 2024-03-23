using Core.Entities.Operation;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class OperationNoteRepository : GenericRepository<OperationNote>, IOperationNoteRepository
    {
        public OperationNoteRepository(Context context) : base(context)
        {
        }
    }
}
