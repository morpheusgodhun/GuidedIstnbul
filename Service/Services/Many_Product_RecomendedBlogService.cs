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
    public class Many_Product_RecomendedBlogService : GenericService<Many_Product_RecomendedBlog>, IMany_Product_RecomendedBlogService
    {
        public Many_Product_RecomendedBlogService(IGenericRepository<Many_Product_RecomendedBlog> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
