
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Faq:BaseEntity 
    {
        public int Order { get; set; }

        //FaqLanguageItem
        public ICollection<FaqLangaugeItem> FaqLangaugeItems { get; set; }

        //FaqCategory
        [ForeignKey("FaqCategory")]
        public Guid FaqCategoryID { get; set; }
        public FaqCategory FaqCategory { get; set; }
    }
}
