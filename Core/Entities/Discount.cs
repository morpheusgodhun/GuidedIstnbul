using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Discount : BaseEntity
    {
        public string DiscountName { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public string DiscountType { get; set; }
        public DateTime StartDate { get; set; }  //booking start date --> rezervasyon alım tarihi booking start ve end arasında olmalı
        public DateTime EndDate { get; set; }   //booking end date
        public DateTime? UsingStartDate { get; set; } //using discount start date
        public DateTime? UsingEndDate { get; set; } //using discount end date
        public string Explanation { get; set; }
        public string? ImagePath { get; set; } //?
        public int MaxUsageCount { get; set; }
        public int UsageLeft { get; set; } // kalan kullanım --> db ye kaydederken maxUsageCount değeri ile başlatacam
        public ICollection<Many_Discount_TourCategory> Many_Discount_TourCategories { get; set; }
        public ICollection<Many_Discount_Product> Many_Discount_Products { get; set; } //tours to benefit

    }
}
