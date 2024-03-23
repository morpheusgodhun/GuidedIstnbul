using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductFaq : BaseEntity
    {
        public int Order { get; set; }

        //ProductFaqLanguageItem
        public ICollection<ProductFaqLanguageItem> ProductFaqLanguageItems { get; set; }

        //Product
        [ForeignKey("Product")]
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
    }
}
