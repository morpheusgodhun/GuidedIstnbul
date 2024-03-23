using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AdditionalServiceInputLanguageItem : BaseEntity
    {
        public string DisplayName { get; set; }
        public int LanguageID { get; set; }

        //AdditionalServiceInput
        [ForeignKey("AdditionalServiceInput")]
        public Guid AdditionalServiceInputID { get; set; }
        public AdditionalServiceInput AdditionalServiceInput { get; set; }


    }
}
