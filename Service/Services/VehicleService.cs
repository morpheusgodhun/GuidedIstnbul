using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.VehicleDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class VehicleService : GenericService<Vehicle>, IVehicleService
    {
        readonly IVehicleRepository _vehicleRepository;
        public VehicleService(IGenericRepository<Vehicle> repository, IUnitOfWork unitOfWork, IVehicleRepository vehicleRepository) : base(repository, unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
        }

        public void AddVehicle(AddVehicleDto vehicleDto)
        {
            Vehicle vehicle = new()
            {
                Capacity = vehicleDto.Capacity,
                Name = vehicleDto.VehicleName,
                PlateNumber = vehicleDto.PlateNumber,
                SupplierId = Guid.Parse(vehicleDto.SupplierID),
                VehicleTypeId = Guid.Parse(vehicleDto.VehicleTypeID),
                TursabPlateNumber = vehicleDto.TursabPlateNumber,
                UserId = Guid.Parse(vehicleDto.DriverUserId),
            };
            _vehicleRepository.Add(vehicle);
            _unitOfWork.saveChanges();
        }

        public void EditVehicle(EditVehicleDto vehicleDto)
        {
            var vehicle = _vehicleRepository.GetById(Guid.Parse(vehicleDto.VehicleID));
            vehicle.Name = vehicleDto.VehicleName;
            vehicle.Capacity = vehicleDto.Capacity;
            vehicle.PlateNumber = vehicleDto.PlateNumber;
            vehicle.SupplierId = Guid.Parse(vehicleDto.SupplierID);
            vehicle.VehicleTypeId = Guid.Parse(vehicleDto.VehicleTypeID);
            vehicle.UserId = Guid.Parse(vehicleDto.DriverUserId);
            vehicle.TursabPlateNumber = vehicleDto.TursabPlateNumber;
            vehicle.UpdateDate = DateTime.Now;

            _vehicleRepository.Update(vehicle);
            _unitOfWork.saveChanges();
        }
        public List<VehicleListDto> GetVehicles()
        {
            return _vehicleRepository.GetVehiclesList();
        }
        public List<SelectListOptionDto> GetVehicleSelectList()
        {
            var vehicleSelectList = GetVehicles().Select(x => new SelectListOptionDto
            {
                OptionID = Guid.Parse(x.VehicleID),
                Option = $"{x.VehicleName} - {x.Driver}"
            }).ToList();

            return vehicleSelectList;
        }

        public List<SelectListOptionDto> GetVehicleSelectList(string supplierId)
        {
            var vehicleSelectList = _vehicleRepository.GetVehiclesList(Guid.Parse(supplierId)).Select(x => new SelectListOptionDto
            {
                OptionID = Guid.Parse(x.VehicleID),
                Option = $"{x.VehicleName} - {x.Driver}"
            }).ToList();
            return vehicleSelectList;
        }

        public void ToggleStatus(string id)
        {
            var vehicle = _vehicleRepository.GetById(Guid.Parse(id));
            vehicle.Status = !vehicle.Status;
            _vehicleRepository.Update(vehicle);
            _unitOfWork.saveChanges();
        }
    }
}
