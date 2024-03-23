using Core.Entities;
using Core.IRepository;
using Core.IService;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(Context context) : base(context)
        {
        }

        public (List<ProductImageListDto>, string productName) GetProductImageList(Guid id)
        {
            List<ProductImageListDto> productImageListDtos = (from image in _context.ProductImages
                                                              where image.ProductID == id
                                                              select new ProductImageListDto()
                                                              {
                                                                  ProductImageID = image.Id,
                                                                  Status = image.Status,
                                                                  Order = image.Order,
                                                                  ImagePath = image.ImagePath,
                                                              }).ToList();
            string productName = _context.Products.Where(x => x.Id == id).FirstOrDefault().ProductName;
            return (productImageListDtos, productName);
        }
    }
}
