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
    public class Many_Agent_ProductService : GenericService<Many_Agent_Product>, IMany_Agent_ProductService
    {
        public Many_Agent_ProductService(IGenericRepository<Many_Agent_Product> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
