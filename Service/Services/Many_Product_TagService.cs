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
    public class Many_Product_TagService : GenericService<Many_Product_Tag>, IMany_Product_TagService
    {
        public Many_Product_TagService(IGenericRepository<Many_Product_Tag> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
