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
    public class Many_Product_AdditionalServiceService : GenericService<Many_Product_AdditionalService>, IMany_Product_AdditionalServiceService
    {
        public Many_Product_AdditionalServiceService(IGenericRepository<Many_Product_AdditionalService> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
