using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class FaqCategory : BaseEntity
    {
        public int Order { get; set; }

        [ForeignKey("Page")]
        public Guid? PageId { get; set; }

        public Page? Page { get; set; }

        //FaqLanguageItem
        public ICollection<FaqCategoryLanguageItem> FaqCategoryLanguageItems { get; set; }

        //Faq
        public ICollection<Faq> Faqs { get; set; }
    }
}
