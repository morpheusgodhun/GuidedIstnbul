using Core.IService;
using Dto.ApiPanelDtos.Driver;
using Dto.ApiPanelDtos.DriverDtos;
using Dto.ApiPanelDtos.VehicleDtos;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.ComponentModel.DataAnnotations;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelDriverController : ControllerBase
    {
        readonly IDriverService _driverService;

        public PanelDriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public CustomResponseDto<List<DriverListDto>> DriverList()
        {
            List<DriverListDto> driverListDto = _driverService.GetDrivers();
            return CustomResponseDto<List<DriverListDto>>.Success(200, driverListDto);
        }
        [HttpPost]
        public CustomResponseNullDto AddDriver(AddDriverDto addDriverDto)
        {
            _driverService.AddDriver(addDriverDto);
            return CustomResponseNullDto.Success(204);
        }
        [HttpGet("{id}")]
        public CustomResponseDto<EditDriverDto> EditDriver(string id)
        {
            var driverDto = _driverService.GetEditDriver(id);
            return CustomResponseDto<EditDriverDto>.Success(200, driverDto);
        }
        [HttpPost]
        public CustomResponseNullDto EditDriver(EditDriverDto editDriverDto)
        {
            _driverService.EditDriver(editDriverDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleDriverStatus(string id)
        {
            _driverService.ToggleDriverStatus(Guid.Parse(id));
            return CustomResponseNullDto.Success(204);
        }
    }
}
