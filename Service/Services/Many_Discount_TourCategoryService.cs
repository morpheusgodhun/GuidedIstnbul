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
    public class Many_Discount_TourCategoryService : GenericService<Many_Discount_TourCategory>, IMany_Discount_TourCategoryService
    {
        public Many_Discount_TourCategoryService(IGenericRepository<Many_Discount_TourCategory> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
