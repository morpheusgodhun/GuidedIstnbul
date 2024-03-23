using Core.Entities;
using Dto.ApiPanelDtos.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IDiscountRepository : IGenericRepository<Discount>
    {
        Task<List<DiscountListDto>> GetDiscounts();
        Discount GetDiscountById(Guid id);
        Discount GetDiscountByCouponCode(string couponCode);
    }
}
