using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.VehicleDtos
{
    public class EditVehicleDto
    {
        public string VehicleID { get; set; }
        public string SupplierID { get; set; }
        public string VehicleName { get; set; }
        public string VehicleTypeID { get; set; }
        public int Capacity { get; set; }
        public string PlateNumber { get; set; }
        public string TursabPlateNumber { get; set; }
        public string DriverUserId { get; set; }
    }
}
