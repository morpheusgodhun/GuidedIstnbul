using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Discount_TourCategory : BaseEntity
    {
        [ForeignKey("Discount")]
        public Guid DiscountId { get; set; }
        public Discount Discount { get; set; }

        [ForeignKey("TourCategory")]
        public Guid TourCategoryID { get; set; }
        public TourCategory TourCategory { get; set; }
    }
}
