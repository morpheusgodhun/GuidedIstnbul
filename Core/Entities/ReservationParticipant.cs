using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ReservationParticipant : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; } //Will Delete
        public string? Email { get; set; } //Will Delete
        public DateTime? BirthDay { get; set; }

        [ForeignKey("ReservationForm")]
        public Guid ReservationFormId { get; set; }
        public Reservation ReservationForm { get; set; }
    }
}
