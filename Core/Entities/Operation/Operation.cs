using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Operation
{
    public class Operation : BaseEntity
    {
        [ForeignKey("Reservation")]
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int OperationStatus { get; set; }
        public string? Note { get; set; }
        public ICollection<OperationGuide> Guides { get; set; } //?
        public ICollection<OperationVehicle> Vehicles { get; set; } //?
        public ICollection<OperationTicket> Tickets { get; set; } //?
    }

}
