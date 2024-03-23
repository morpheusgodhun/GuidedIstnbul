using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AdditionalServiceLanguageItem : BaseEntity
    {
        public string DisplayName { get; set; }
        public int LanguageID { get; set; }

        //AdditionalService
        [ForeignKey("AdditionalService")]
        public Guid AdditionalServiceID { get; set; }
        public AdditionalService AdditionalService { get; set; }


    }
}
