using Core.Entities;
using Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ProductLanguageRepository : GenericRepository<ProductLanguageItem>, IProductLanguageRepository
    {

        //private readonly IProductRepository _productRepository;

        public ProductLanguageRepository(Context context) : base(context)
        {
        }
        public override ProductLanguageItem GetById(Guid id)
        {
            return _context.ProductLanguageItems.Include(x => x.Product).FirstOrDefault(x => x.Id == id);
        }
    }
}
