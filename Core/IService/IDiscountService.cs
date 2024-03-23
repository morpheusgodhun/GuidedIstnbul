using Core.Entities;
using Dto.ApiPanelDtos.Discount;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IDiscountService : IGenericService<Discount>
    {
        List<DiscountListDto> GetDiscounts();
        void AddDiscount(AddDiscountDto discountDto);
        void DeleteDiscount(string discountId);
        void EditDiscount(EditDiscountDto discountDto);
        EditDiscountDto GetForEdit(string id);
        bool IsCouponCodeExist(string couponCode);
        CustomResponseDto<GetCouponDiscountResponseDto> GetCouponDiscountForReservation(GetCouponDiscountDto dto);
        void RemoveCouponDiscountFromReservation(RemoveCouponDiscountDto dto);
    }
}
