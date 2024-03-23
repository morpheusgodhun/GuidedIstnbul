using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AdditionalServiceOption : BaseEntity
    {
        //AdditionalService
        [ForeignKey("AdditionalService")]
        public Guid AdditionalServiceID { get; set; }
        public AdditionalService AdditionalService { get; set; }
        public int Order { get; set; }

        //AdditionalServiceOptionLanguageItem
        public ICollection<AdditionalServiceOptionLanguageItem> AdditionalServiceOptionLanguageItems { get; set; }

        //AdditionalServiceOptionPrice
        public ICollection<AdditionalServiceOptionPriceDate> AdditionalServiceOptionPrices { get; set; }

        //AdditionalServiceInput  Many-To-Many
        public ICollection<Many_AdditionalServiceOption_AdditionalServiceInput> Many_AdditionalServiceOption_AdditionalServiceInputs { get; set; }

        //Many_Product_AdditionalService Many-To-Many
        public ICollection<Many_Product_AdditionalServiceOption> Many_Product_AdditionalServiceOptions { get; set; }
    }
}
