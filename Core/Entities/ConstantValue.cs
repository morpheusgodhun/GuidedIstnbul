using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ConstantValue : BaseEntity
    {
        public string Key { get; set; }


        //ConstantValueLanguageItem
        public ICollection<ConstantValueLanguageItem> ConstantValueLanguageItems { get; set; }


        //Page
        [ForeignKey("Page")]
        public Guid PageID { get; set; }
        public Page Page { get; set; }
    }
}
