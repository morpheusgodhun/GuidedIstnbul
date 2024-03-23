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
    public class Many_Blog_BlogCategoryService : GenericService<Many_Blog_BlogCategory>, IMany_Blog_BlogCategoryService
    {
        public Many_Blog_BlogCategoryService(IGenericRepository<Many_Blog_BlogCategory> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
