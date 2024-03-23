using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductFaqLanguageItem : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int LanguageID { get; set; }

        //ProductFaq

        [ForeignKey("ProductFaq")]
        public Guid ProductFaqID { get; set; }
        public ProductFaq ProductFaq { get; set; }

    }
}
