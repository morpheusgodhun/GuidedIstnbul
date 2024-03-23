using Core.Entities;
using Core.IRepository;
using Dto.ApiPanelDtos.Discount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(Context context) : base(context)
        {
        }

        public Discount GetDiscountByCouponCode(string couponCode)
        {
            return _context.Discounts.Include(d => d.Many_Discount_TourCategories)
            .Include(d => d.Many_Discount_Products).FirstOrDefault(x => x.CouponCode == couponCode);
        }

        public Discount GetDiscountById(Guid id)
        {
            return _context.Discounts
            .Include(d => d.Many_Discount_TourCategories)
            .Include(d => d.Many_Discount_Products)
            .FirstOrDefault(d => d.Id == id);
        }

        public async Task<List<DiscountListDto>> GetDiscounts()
        {

            var query = from discount in _context.Discounts
                        join mdp in _context.Many_Discount_Products on discount.Id equals mdp.DiscountId into products
                        from product in products.DefaultIfEmpty()
                        join mdtc in _context.Many_Discount_TourCategory on discount.Id equals mdtc.DiscountId into tourCategories
                        from tourCategory in tourCategories.DefaultIfEmpty()
                        join tc in _context.TourCategoryLanguageItems.Where(t => t.LanguageID == 2) on tourCategory.TourCategoryID equals tc.TourCategoryID into tourCategoryDetails
                        from tourCategoryDetail in tourCategoryDetails.DefaultIfEmpty()
                        join p in _context.Products on product.ProductId equals p.Id into productDetails
                        from productDetail in productDetails.DefaultIfEmpty()
                        where discount.IsDeleted == false
                        group new { discount, productDetail, tourCategoryDetail } by new
                        {
                            discount.Id,
                            discount.DiscountName,
                            discount.CouponCode,
                            discount.DiscountAmount,
                            discount.StartDate,
                            discount.EndDate,
                            discount.Explanation,
                            discount.ImagePath,
                            discount.MaxUsageCount,
                            discount.CreateDate,
                            discount.UpdateDate,
                            discount.Status,
                            discount.IsDeleted,
                            discount.DiscountType,
                            discount.UsingStartDate,
                            discount.UsingEndDate,
                        } into grouped
                        select new DiscountListDto
                        {
                            Id = grouped.Key.Id.ToString(),
                            DiscountName = grouped.Key.DiscountName,
                            CouponCode = grouped.Key.CouponCode,
                            DiscountType = grouped.Key.DiscountType,
                            DiscountAmount = grouped.Key.DiscountAmount,
                            StartDate = grouped.Key.StartDate,
                            EndDate = grouped.Key.EndDate,
                            UsingStartDate = grouped.Key.UsingStartDate,
                            UsingEndDate = grouped.Key.UsingEndDate,
                            Explanation = grouped.Key.Explanation,
                            MaxUsageCount = grouped.Key.MaxUsageCount,
                            TourCategories = grouped.Select(tc => tc.tourCategoryDetail.CategoryName).ToList(),
                            ToursToBenefit = grouped.Select(p => p.productDetail.ProductName).ToList(),
                            Status = grouped.Key.Status
                        };



            var result = await query.ToListAsync();
            return result;




        }
    }
}
