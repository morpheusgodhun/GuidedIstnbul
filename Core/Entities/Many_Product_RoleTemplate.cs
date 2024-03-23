using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Product_RoleTemplate : BaseEntity
    {
        public Many_Product_RoleTemplate()
        {

        }
        public Many_Product_RoleTemplate(Guid productId, Guid roleTemplateId)
        {
            ProductId = productId;
            RoleTemplateId = roleTemplateId;
        }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("RoleTemplate")]
        public Guid RoleTemplateId { get; set; }
        public RoleTemplate RoleTemplate { get; set; }
    }
}
