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
    public class ManyBlogTagService : GenericService<Many_Blog_Tag>, IManyBlogTagService
    {
        public ManyBlogTagService(IGenericRepository<Many_Blog_Tag> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
