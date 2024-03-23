using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_ReservationForm_AlsoInterested : BaseEntity
    {
        [ForeignKey("SystemOption")]
        public Guid SystemOptionId { get; set; }
        public SystemOption SystemOption { get; set; }

        [ForeignKey("ReservationForm")]
        public Guid ReservationFormId { get; set; }
        public Reservation ReservationForm { get; set; }
    }
}
