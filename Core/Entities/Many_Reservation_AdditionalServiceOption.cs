using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Reservation_AdditionalServiceOption : BaseEntity
    {
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public Guid AdditionalServiceOptionId { get; set; }
        public AdditionalServiceOption AdditionalServiceOption { get; set; }
        public decimal Price { get; set; }
        public ICollection<ReservationOptionInput> ReservationOptionInputs { get; set; }
        public int ParticipantNumber { get; set; }
        public decimal CalcPrice { get; set; }
        public Guid AdditionalServiceOptionPriceId { get; set; }
    }
}
