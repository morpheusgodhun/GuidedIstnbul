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
    public class Many_Tour_SightToSeeService : GenericService<Many_Tour_SightToSee>, IMany_Tour_SightToSeeService
    {
        public Many_Tour_SightToSeeService(IGenericRepository<Many_Tour_SightToSee> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
