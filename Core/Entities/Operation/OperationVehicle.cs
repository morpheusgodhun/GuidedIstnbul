using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Operation
{
    public class OperationVehicle : BaseEntity
    {
        public Guid OperationId { get; set; }
        public Guid? VehicleId { get; set; }
        public int Day { get; set; }
        public DateTime? Date { get; set; }
        public string? Note { get; set; }
        public int VehicleStatus { get; set; }
        public Operation Operation { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
