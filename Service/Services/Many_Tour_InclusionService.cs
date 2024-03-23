using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class Many_Tour_InclusionService : GenericService<Many_Tour_Inclusion>, IMany_Tour_InclusionService
    {
        public Many_Tour_InclusionService(IGenericRepository<Many_Tour_Inclusion> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
