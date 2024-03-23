using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ContactInfo : BaseEntity
    {
        public int Type { get; set; }


        //LanguageItem
        public ICollection<ContactInfoLanguageItem> ContactInfoLanguageItems { get; set; }
    }
}
