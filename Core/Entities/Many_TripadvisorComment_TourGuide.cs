using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_TripadvisorComment_TourGuide : BaseEntity
    {

        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public User User { get; set; }


        [ForeignKey("TripadvisorComment")]
        public Guid TripadvisorCommentID { get; set; }
        public TripadvisorComment TripadvisorComment { get; set; }
    }
}
