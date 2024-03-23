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
    public class Many_Discount_ProductService : GenericService<Many_Discount_Product>, IMany_Discount_ProductService
    {
        public Many_Discount_ProductService(IGenericRepository<Many_Discount_Product> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
