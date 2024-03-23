using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AdditionalServiceInput : BaseEntity
    {
        public int Order { get; set; }
        public int InputTypeID { get; set; }
        public bool IsRequired { get; set; }

        //AdditionalService

        [ForeignKey("AdditionalService")]
        public Guid AdditionalServiceID { get; set; }
        public AdditionalService AdditionalService { get; set; }

        //AdditionalServiceInputLanguageItem
        public ICollection<AdditionalServiceInputLanguageItem> AdditionalServiceInputLanguageItems { get; set; }

        //AdditionalServiceOption  Many-To-Many
        public ICollection<Many_AdditionalServiceOption_AdditionalServiceInput> Many_AdditionalServiceOption_AdditionalServiceInputs { get; set; }
        public ICollection<ReservationOptionInput> ReservationOptionInputs { get; set; }
    }
}
