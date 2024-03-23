using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.VehicleDtos
{
    public class VehicleListDto
    {
        public string VehicleID { get; set; }
        public string VehicleName { get; set; }
        public string Supplier { get; set; }
        public string Driver { get; set; }
        public string VehicleType { get; set; }
        public int Capacity { get; set; }
        public string PlateNumber { get; set; }
        public string TursabPlateNumber { get; set; }
        public bool Status { get; set; }
    }
}
