using Core.Entities;
using Dto.ApiPanelDtos.VehicleDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IVehicleService : IGenericService<Vehicle>
    {
        List<VehicleListDto> GetVehicles();
        List<SelectListOptionDto> GetVehicleSelectList();
        List<SelectListOptionDto> GetVehicleSelectList(string supplierId);
        void AddVehicle(AddVehicleDto vehicleDto);
        void EditVehicle(EditVehicleDto vehicleDto);
        void ToggleStatus(string id);
    }
}
