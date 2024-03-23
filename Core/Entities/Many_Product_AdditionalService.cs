using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Product_AdditionalService : BaseEntity
    {
        [ForeignKey("Product")]
        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        [ForeignKey("AdditionalService")]
        public Guid AdditionalServiceID { get; set; }
        public AdditionalService AdditionalService { get; set; }

        public bool IsRequired { get; set; }
        public bool IsMultiSelect { get; set; }
        public bool ComissionRateAbility { get; set; }
        public int Order { get; set; }

        //AdditionalServiceOption Many-To-Many
        public ICollection<Many_Product_AdditionalServiceOption> Many_Product_AdditionalServiceOptions { get; set; }

    }
}
