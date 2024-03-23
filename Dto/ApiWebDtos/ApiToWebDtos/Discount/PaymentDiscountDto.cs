using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Discount
{
    public class PaymentDiscountDto
    {
        public string CouponCode { get; set; }
        public int DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
