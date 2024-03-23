using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Product_RecomendedBlog : BaseEntity
    {
        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        public Guid BlogID { get; set; }
        public Blog Blog { get; set; }
    }
}
