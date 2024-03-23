using Core.Entities;
using Core.Entities.Operation;
using Dto.ApiPanelDtos.OperationDtos;

namespace Core.IService
{
    public interface IOperationService : IGenericService<Operation>
    {
        List<OperationListDto> GetOperations();
        void CreateOperation(Reservation reservation);
        void AssignGuide(AssignGuideToOperationDto assignGuideDto);
        void AssignVehicle(AssignVehicleToOperationDto assignVehicleDto);
        void SaveDailyOperationNote(SaveDailyOperationNoteDto dto);
        void SaveTicketInfo(SaveTicketInfoDto dto);
        void SaveHourInfo(SaveHourInfoDto dto);
        void ChangeShopStatus(ChangeShopStatusDto dto);
        AssignGuideToOperationDto GetOperationGuideForEdit(string operationGuideId);
        AssignVehicleToOperationDto GetOperationVehicleForEdit(string operationGuideId);
    }
}
