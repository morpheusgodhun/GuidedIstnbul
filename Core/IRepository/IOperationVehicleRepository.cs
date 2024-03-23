using Core.Entities.Operation;
using Dto.ApiPanelDtos.OperationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IOperationVehicleRepository : IGenericRepository<OperationVehicle>
    {
        AssignVehicleToOperationDto GetForEdit(Guid id);
    }
}
