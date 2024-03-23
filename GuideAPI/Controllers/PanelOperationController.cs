using Core.IService;
using Dto.ApiPanelDtos.OperationDtos;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelOperationController : CustomBaseController
    {
        readonly IOperationService _operationService;

        public PanelOperationController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpGet]
        public CustomResponseDto<List<OperationListDto>> OperationList()
        {
            var data = _operationService.GetOperations();
            return CustomResponseDto<List<OperationListDto>>.Success(200, data);
        }

        [HttpPost]
        public CustomResponseNullDto AssignGuide(AssignGuideToOperationDto assignDto)
        {
            _operationService.AssignGuide(assignDto);
            return CustomResponseNullDto.Success(200);
        }

        [HttpPost]
        public CustomResponseNullDto AssignVehicle(AssignVehicleToOperationDto assignDto)
        {
            _operationService.AssignVehicle(assignDto);
            return CustomResponseNullDto.Success(200);
        }

        [HttpPost]
        public CustomResponseNullDto SaveDailyOperationNote(SaveDailyOperationNoteDto noteDto)
        {
            _operationService.SaveDailyOperationNote(noteDto);
            return CustomResponseNullDto.Success(200);
        }

        [HttpPost]
        public CustomResponseNullDto SaveTicketInfo(SaveTicketInfoDto ticketInfo)
        {
            _operationService.SaveTicketInfo(ticketInfo);
            return CustomResponseNullDto.Success(200);
        }

        [HttpPost]
        public CustomResponseNullDto SaveHourInfo(SaveHourInfoDto hourInfo)
        {
            _operationService.SaveHourInfo(hourInfo);
            return CustomResponseNullDto.Success(200);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<AssignGuideToOperationDto> GetOperationGuide(string id)
        {
            var responseData = _operationService.GetOperationGuideForEdit(id);
            return CustomResponseDto<AssignGuideToOperationDto>.Success(200, responseData);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<AssignVehicleToOperationDto> GetOperationVehicle(string id)
        {
            var responseData = _operationService.GetOperationVehicleForEdit(id);
            return CustomResponseDto<AssignVehicleToOperationDto>.Success(200, responseData);
        }

        public CustomResponseNullDto ChangeShopStatus(ChangeShopStatusDto changeShopStatusDto)
        {
            _operationService.ChangeShopStatus(changeShopStatusDto);
            return CustomResponseNullDto.Success(200);
        }
    }
}
