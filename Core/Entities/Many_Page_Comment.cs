using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Page_Comment : BaseEntity
    {

        [ForeignKey("Page")]
        public Guid PageID { get; set; }
        public Page Page { get; set; }


        [ForeignKey("Comment")]
        public Guid CommentID { get; set; }
        public Comment Comment { get; set; }
    }
}
