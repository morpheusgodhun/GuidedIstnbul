using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Service : BaseEntity
    {

        //Product

        [ForeignKey("Product")]
        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        //ServiceLanguageItem
        public ICollection<ServiceLanguageItem> ServiceLanguageItems { get; set; }
    }
}
