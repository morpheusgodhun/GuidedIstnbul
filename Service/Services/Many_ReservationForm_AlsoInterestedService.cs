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
    public class Many_ReservationForm_AlsoInterestedService : GenericService<Many_ReservationForm_AlsoInterested>, IMany_ReservationForm_AlsoInterestedService
    {
        public Many_ReservationForm_AlsoInterestedService(IGenericRepository<Many_ReservationForm_AlsoInterested> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
