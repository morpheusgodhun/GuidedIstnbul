using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.Discount
{
    public class EditDiscountDto
    {
        public string Id { get; set; }
        public string DiscountName { get; set; }
        public List<Guid> TourCategoryIds { get; set; }
        public List<Guid> ProductToBenefitIds { get; set; }
        public int MaxUsageCount { get; set; }
        public int DiscountType { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? UsingStartDate { get; set; }
        public DateTime? UsingEndDate { get; set; }
        public string Explanation { get; set; }
        public string? ImagePath { get; set; }
    }
}
