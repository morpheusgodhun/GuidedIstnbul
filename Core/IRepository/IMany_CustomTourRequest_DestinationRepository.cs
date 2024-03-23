using Core.Entities;
using Dto.ApiPanelDtos.DestinationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IMany_CustomTourRequest_DestinationRepository : IGenericRepository<Many_CustomTourRequest_Destination>
    {
        List<DestinationListDto> GetDestinationsByRequestId(Guid customTourRequestId, int languageId = 1);
    }
}
