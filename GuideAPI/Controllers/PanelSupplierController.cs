using Core.IService;
using Core.StaticValues;
using Dto.ApiPanelDtos.SupplierDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelSupplierController : ControllerBase
    {
        readonly ISupplierService _supplierService;
        readonly IShopService _shopService;

        public PanelSupplierController(ISupplierService supplierService, IShopService shopService)
        {
            _supplierService = supplierService;
            _shopService = shopService;
        }

        public CustomResponseDto<List<SupplierListDto>> GetSuppliers()
        {
            var data = _supplierService.GetAll().Select(x => new SupplierListDto(x.Id.ToString(), x.Name, new SupplierTypeList().SupplierTypes.Where(supType => supType.ID == x.SupplierType).FirstOrDefault().Value, x.Status)).ToList();

            return CustomResponseDto<List<SupplierListDto>>.Success(200, data);
        }
        [HttpPost]
        public CustomResponseNullDto AddSupplier(AddSupplierDto dto)
        {
            var addedSupplier = _supplierService.Add(new Core.Entities.Supplier
            {
                Name = dto.Name,
                SupplierType = dto.SupplierType,
            });
            return CustomResponseNullDto.Success(200);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditSupplierDto> EditSupplier(string id)
        {
            var supplier = _supplierService.GetById(Guid.Parse(id));
            return CustomResponseDto<EditSupplierDto>.Success(200, new EditSupplierDto(supplier.Id.ToString(), supplier.Name, supplier.SupplierType));
        }
        [HttpPost]
        public CustomResponseNullDto EditSupplier(EditSupplierDto dto)
        {
            _supplierService.Update(new Core.Entities.Supplier
            {
                Id = new Guid(dto.Id),
                Name = dto.Name,
                SupplierType = dto.SupplierType,
            });
            return CustomResponseNullDto.Success(200);
        }
        public CustomResponseNullDto ToggleSupplierStatus(string id)
        {
            _supplierService.ToggleStatus(Guid.Parse(id));
            return CustomResponseNullDto.Success(204);
        }
    }
}
