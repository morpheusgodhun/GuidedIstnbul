using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductLanguageItem : BaseEntity
    {
        public string DisplayName { get; set; }
        public string Slug { get; set; }
        public int LanguageID { get; set; }

        //Product

        [ForeignKey("Product")]
        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        [NotMapped] //ef core görmesin
        public bool SitemapInclude { get; set; }
    }
}
