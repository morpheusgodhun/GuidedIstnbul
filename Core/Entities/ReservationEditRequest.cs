using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ReservationEditRequest : BaseEntity
    {
        public Guid? ReasonId { get; set; }
        public string Reason { get; set; }
        public int RequestType { get; set; }
        public int RequestStatus { get; set; }
        public string? AdminAnswer { get; set; }
        [ForeignKey("Reservation")]
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
