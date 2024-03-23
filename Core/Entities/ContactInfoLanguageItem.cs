using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ContactInfoLanguageItem : BaseEntity
    {
        public string Value { get; set; }
        public int LanguageID { get; set; }


        //ContactInfo

        [ForeignKey("ContactInfo")]
        public Guid ContactInfoID { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
