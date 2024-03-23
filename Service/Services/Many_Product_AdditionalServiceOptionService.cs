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
    public class Many_Product_AdditionalServiceOptionService : GenericService<Many_Product_AdditionalServiceOption>, IMany_Product_AdditionalServiceOptionService
    {
        public Many_Product_AdditionalServiceOptionService(IGenericRepository<Many_Product_AdditionalServiceOption> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
