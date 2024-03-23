using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TourPriceItem : BaseEntity
    {
        [ForeignKey("PersonPolicy")]
        public Guid? PersonPolicyID { get; set; }
        public PersonPolicy? PersonPolicy { get; set; }

        public decimal Price { get; set; }


        public Guid TourPriceID { get; set; }
        public TourPrice TourPrice { get; set; }
    }
}
