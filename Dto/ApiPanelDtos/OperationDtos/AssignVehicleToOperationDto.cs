using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.OperationDtos
{
    public class AssignVehicleToOperationDto
    {
        //assign etme ve update için de aynı nesneleri kullanıyorum.
        //assign ederken supplierId ye ihtiyacım yok. update işleminde supplier dropDown u doldurmak için kullanacağım.
        public string? SupplierId { get; set; }
        public string OperationVehicleId { get; set; }
        public string VehicleId { get; set; }
    }
}
