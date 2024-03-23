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
    public class Many_Tour_TourCategoryService : GenericService<Many_Tour_TourCategory>, IMany_Tour_TourCategoryService
    {
        public Many_Tour_TourCategoryService(IGenericRepository<Many_Tour_TourCategory> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
