using Core.Entities.Operation;
using Dto.ApiPanelDtos.OperationDtos;

namespace Core.IRepository
{
    public interface IOperationRepository : IGenericRepository<Operation>
    {
        List<OperationListDto> GetOperationList();
    }
}
