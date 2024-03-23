using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Product_RecomendedBlogRepository : GenericRepository<Many_Product_RecomendedBlog>, IMany_Product_RecomendedBlogRepository
    {
        public Many_Product_RecomendedBlogRepository(Context context) : base(context)
        {
        }
    }
}
