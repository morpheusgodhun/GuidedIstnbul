using Core.Entities;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IProductImageService : IGenericService<ProductImage>
    {
        TourDetailProductImageListDto GetProductImageList(Guid id);
    }
}
