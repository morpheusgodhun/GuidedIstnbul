using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Discount_Product : BaseEntity
    {
        [ForeignKey("Discount")]
        public Guid DiscountId { get; set; }
        public Discount Discount { get; set; }
        [ForeignKey("Droduct")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
