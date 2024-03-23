using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AdditionalServiceOptionLanguageItem : BaseEntity
    {
        public string DisplayName { get; set; }
        public int LanguageID { get; set; }

        //AdditionalServiceOption

        [ForeignKey("AdditionalServiceOption")]
        public Guid AdditionalServiceOptionID { get; set; }
        public AdditionalServiceOption AdditionalServiceOption { get; set; }


    }
}
