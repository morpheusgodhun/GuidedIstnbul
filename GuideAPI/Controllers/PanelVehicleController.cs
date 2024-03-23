using Core.IService;
using Dto.ApiPanelDtos.CancellationPolicyDtos;
using Dto.ApiPanelDtos.VehicleDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelVehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public PanelVehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        [HttpGet]
        public CustomResponseDto<List<VehicleListDto>> VehicleList()
        {
            List<VehicleListDto> vehicleListDtos = _vehicleService.GetVehicles();
            return CustomResponseDto<List<VehicleListDto>>.Success(200, vehicleListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddVehicle(AddVehicleDto addVehicleDto)
        {
            _vehicleService.AddVehicle(addVehicleDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditVehicleDto> EditVehicle(string id)
        {
            var vehicle = _vehicleService.GetById(Guid.Parse(id));
            EditVehicleDto editVehicleDto = new()
            {
                Capacity = vehicle.Capacity,
                VehicleName = vehicle.Name,
                VehicleTypeID = vehicle.VehicleTypeId.ToString(),
                PlateNumber = vehicle.PlateNumber,
                TursabPlateNumber = vehicle.TursabPlateNumber,
                SupplierID = vehicle.SupplierId.ToString(),
                VehicleID = vehicle.Id.ToString(),
                DriverUserId = vehicle.UserId.ToString(),
            };

            return CustomResponseDto<EditVehicleDto>.Success(200, editVehicleDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditVehicle(EditVehicleDto editVehicleDto)
        {
            _vehicleService.EditVehicle(editVehicleDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleVehicleStatus(string id)
        {
            _vehicleService.ToggleStatus(id);
            return CustomResponseNullDto.Success(204);
        }
    }
}
