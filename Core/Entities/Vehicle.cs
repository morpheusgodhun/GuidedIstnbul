using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Vehicle : BaseEntity
    {
        public int Capacity { get; set; }
        public string Name { get; set; }
        public string PlateNumber { get; set; }
        public string TursabPlateNumber { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("Supplier")]
        public Guid SupplierId { get; set; } //System Option
        [ForeignKey("VehicleType")]
        public Guid VehicleTypeId { get; set; } //System Option
        public User User { get; set; }
        public Supplier Supplier { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}
