using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class Many_Tour_DestinationService : GenericService<Many_Tour_Destination>, IMany_Tour_DestinationService
    {
        public Many_Tour_DestinationService(IGenericRepository<Many_Tour_Destination> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
