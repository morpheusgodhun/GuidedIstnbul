using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductImageService : GenericService<ProductImage>, IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        public ProductImageService(IGenericRepository<ProductImage> repository, IUnitOfWork unitOfWork, IProductImageRepository productImageRepository) : base(repository, unitOfWork)
        {
            _productImageRepository = productImageRepository;
        }

        public TourDetailProductImageListDto GetProductImageList(Guid id)
        {
            var tupleData = _productImageRepository.GetProductImageList(id);
            TourDetailProductImageListDto dto = new()
            {
                ProductImages = tupleData.Item1,
                ProductName = tupleData.productName,
            };
            return dto;
        }
    }
}
