using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AdditionalService : BaseEntity
    {
        public string Name { get; set; }
        public bool IsPerPerson { get; set; } //If 0 its per group
        public bool IsPerDay { get; set; } //If 0 its per product
        public bool IsSpecialNumber { get; set; } //If 0 its according to participant count


        //AdditionalServiceInput
        public ICollection<AdditionalServiceInput> AdditionalServiceInputs { get; set; }


        //AdditionalServiceLanguageItem
        public ICollection<AdditionalServiceLanguageItem> AdditionalServiceLanguageItems { get; set; }

        //AdditionalServiceOption
        public ICollection<AdditionalServiceOption> AdditionalServiceOptions { get; set; }

        //Product Many-To-Many
        public ICollection<Many_Product_AdditionalService> Many_Product_AdditionalServices { get; set; }

    }
}
