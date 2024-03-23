using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_CustomTourRequest_Destination : BaseEntity
    {
        [ForeignKey("Destination")]
        public Guid DestinationId { get; set; }
        public Destination Destination { get; set; }

        [ForeignKey("CustomTourRequest")]
        public Guid CustomTourRequestId { get; set; }
        public CustomTourRequest CustomTourRequest { get; set; }
        
    }
}
