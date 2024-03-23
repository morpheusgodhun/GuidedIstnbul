using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Tour_Inclusion : BaseEntity
    {

        [ForeignKey("SystemOption")]
        public Guid SystemOptionID { get; set; }
        public SystemOption SystemOption { get; set; }

        [ForeignKey("Tour")]
        public Guid TourID { get; set; }
        public Tour Tour { get; set; }
    }
}
