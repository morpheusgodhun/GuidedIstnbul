using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class Many_CustomTourRequest_AlsoInterestedService : GenericService<Many_CustomTourRequest_AlsoInterested>, IMany_CustomTourRequest_AlsoInterestedService
    {
        public Many_CustomTourRequest_AlsoInterestedService(IGenericRepository<Many_CustomTourRequest_AlsoInterested> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
