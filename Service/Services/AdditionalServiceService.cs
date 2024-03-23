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
    public class AdditionalServiceService : GenericService<AdditionalService>, IAdditionalServiceService
    {
        public AdditionalServiceService(IGenericRepository<AdditionalService> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {

        }
    }
}
