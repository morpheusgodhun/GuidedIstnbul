using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AdditionalServiceOptionPrice : BaseEntity
    {

        public decimal Price { get; set; }


        [ForeignKey("PersonPolicy")]
        public Guid? PersonPolicyID { get; set; }
        public PersonPolicy? PersonPolicy { get; set; }


        [ForeignKey("AdditionalServiceOptionPriceDate")]
        public Guid AdditionalServiceOptionPriceDateID { get; set; }
        public AdditionalServiceOptionPriceDate AdditionalServiceOptionPriceDate { get; set; }

    }
}
