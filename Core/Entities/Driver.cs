using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Driver : BaseEntity //kullanılmayacak
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //[ForeignKey("VehicleType")]
        //public Guid VehicleTypeId { get; set; }
        //public VehicleType VehicleType { get; set; }
    }
}
