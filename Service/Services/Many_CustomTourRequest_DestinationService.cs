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
    public class Many_CustomTourRequest_DestinationService : GenericService<Many_CustomTourRequest_Destination>, IMany_CustomTourRequest_DestinationService
    {
        public Many_CustomTourRequest_DestinationService(IGenericRepository<Many_CustomTourRequest_Destination> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
