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
    public class Many_Reservation_AdditionalServiceOptionService : GenericService<Many_Reservation_AdditionalServiceOption>, IMany_Reservation_AdditionalServiceOptionService
    {
        public Many_Reservation_AdditionalServiceOptionService(IGenericRepository<Many_Reservation_AdditionalServiceOption> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
