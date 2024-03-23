using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TripadvisorComment : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string Comment { get; set; }

        //Tour

        [ForeignKey("Product")]
        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        //User (Sender)

        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public User User { get; set; }

        //User (Guides) Many-To-Many
        public ICollection<Many_TripadvisorComment_TourGuide> Many_TripadvisorComment_TourGuides { get; set; }
    }
}
