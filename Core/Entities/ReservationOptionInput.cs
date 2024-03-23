using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ReservationOptionInput : BaseEntity
    {
        [ForeignKey("AdditionalServiceInput")]
        public Guid AdditionalServiceInputId { get; set; }
        public AdditionalServiceInput AdditionalServiceInput { get; set; }
        public string? Answer { get; set; }

        [ForeignKey("Many_Reservation_AdditionalServiceOption")]
        public Guid Many_Reservation_AdditionalServiceOptionId { get; set; }
        public Many_Reservation_AdditionalServiceOption Many_Reservation_AdditionalServiceOption { get; set; }
    }
}
