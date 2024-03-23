using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AdditionalServiceOptionPriceDate : BaseEntity
    {

        
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Priority { get; set; }


        [ForeignKey("AdditionalServiceOption")]
        public Guid AdditionalServiceOptionID { get; set; }
        public AdditionalServiceOption? AdditionalServiceOption { get; set; }

        public ICollection<AdditionalServiceOptionPrice> AdditionalServiceOptionPrices { get; set; }



    }
}
