using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductImage : BaseEntity
    {
        public string ImagePath { get; set; }
        public int Order { get; set; }

        // ProductImageLanguageItem
        public ICollection<ProductImageLanguageItem> ProductImageLanguageItems { get; set; }

        //Product

        [ForeignKey("Product")]
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
    }
}
