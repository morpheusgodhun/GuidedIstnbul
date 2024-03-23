using Core.Entities;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {
        (List<ProductImageListDto>, string productName) GetProductImageList(Guid id);
    }
}
