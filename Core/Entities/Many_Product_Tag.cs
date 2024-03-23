using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Product_Tag : BaseEntity
    {

        [ForeignKey("Product")]
        public Guid ProductID { get; set; }
        public Product Product { get; set; }


        [ForeignKey("Tag")]
        public Guid TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
