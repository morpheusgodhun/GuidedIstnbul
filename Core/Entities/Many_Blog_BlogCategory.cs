using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Blog_BlogCategory : BaseEntity
    {
        [ForeignKey("Blog")]
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }

        [ForeignKey("BlogCategory")]
        public Guid BlogCategoryId { get; set; }
        public BlogCategory BlogCategory { get; set; }
    }
}
