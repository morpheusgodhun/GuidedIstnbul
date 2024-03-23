using Core.Entities;
using Core.IService;
using Core.StaticValues;
using Dto.ApiPanelDtos.Discount;
using Dto.ApiPanelDtos.VehicleDtos;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelDiscountController : ControllerBase
    {
        readonly IDiscountService _discountService;
        readonly IMany_Discount_ProductService _discountProductService;
        readonly IMany_Discount_TourCategoryService _discountTourCategoryService;

        public PanelDiscountController(IDiscountService discountService, IMany_Discount_ProductService discountProductService, IMany_Discount_TourCategoryService discountTourCategoryService)
        {
            _discountService = discountService;
            _discountProductService = discountProductService;
            _discountTourCategoryService = discountTourCategoryService;
        }
        [HttpGet]
        public CustomResponseDto<List<DiscountListDto>> DiscountList()
        {
            var discounts = _discountService.GetDiscounts();
            return CustomResponseDto<List<DiscountListDto>>.Success(200, discounts);
        }
        [HttpPost]
        public CustomResponseNullDto AddDiscount(AddDiscountDto addDiscountDto)
        {
            if (_discountService.IsCouponCodeExist(addDiscountDto.CouponCode))
                return CustomResponseNullDto.Fail(400, "Coupon code exist!");

            _discountService.AddDiscount(addDiscountDto);
            return CustomResponseNullDto.Success(200);
        }
        [HttpGet("{id}")]
        public CustomResponseNullDto DeleteDiscount(string id)
        {
            _discountService.DeleteDiscount(id);
            return CustomResponseNullDto.Success(200);
        }
        [HttpGet("{id}")]
        public CustomResponseDto<EditDiscountDto> EditDiscount(string id)
        {
            EditDiscountDto editDiscountDto = _discountService.GetForEdit(id);

            return CustomResponseDto<EditDiscountDto>.Success(200, editDiscountDto);
        }
        [HttpPost]
        public CustomResponseNullDto EditDiscount(EditDiscountDto dto)
        {
            _discountService.EditDiscount(dto);
            return CustomResponseNullDto.Success(204);
        }
        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleDiscountStatus(string id)
        {
            _discountService.ToggleStatus(Guid.Parse(id));
            return CustomResponseNullDto.Success(204);
        }
    }
}
