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
    public class Many_Tour_ExclusionService : GenericService<Many_Tour_Exclusion>, IMany_Tour_ExclusionService
    {
        public Many_Tour_ExclusionService(IGenericRepository<Many_Tour_Exclusion> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
