using Core.Entities;
using Dto.ApiPanelDtos.Driver;
using Dto.ApiPanelDtos.DriverDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IDriverService : IGenericService<Driver>
    {
        List<DriverListDto> GetDrivers();
        EditDriverDto GetEditDriver(string id);
        void AddDriver(AddDriverDto addDriverDto);
        void EditDriver(EditDriverDto editDriverDto);
        void ToggleDriverStatus(Guid id);
        List<SelectListOptionDto> DriverSelectList();
    }
}
