using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Blog_Tag : BaseEntity
    {

        [ForeignKey("Blog")]
        public Guid BlogID { get; set; }
        public Blog Blog { get; set; }


        [ForeignKey("Tag")]
        public Guid TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
