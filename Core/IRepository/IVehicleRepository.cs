using Core.Entities;
using Dto.ApiPanelDtos.VehicleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        List<VehicleListDto> GetVehiclesList();
        List<VehicleListDto> GetVehiclesList(Guid supplierId);
        Vehicle GetVehicleById(Guid vehicleId);

    }
}
