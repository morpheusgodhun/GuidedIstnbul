using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Tour_Destination : BaseEntity
    {
        [ForeignKey("Tour")]
        public Guid TourID { get; set; }
        public Tour Tour { get; set; }

        [ForeignKey("Destinations")]
        public Guid DestinationID { get; set; }
        public Destination Destinations { get; set; }
    }
}
