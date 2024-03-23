using Core.Entities;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IDestinationService : IGenericService<Destination>
    {
        List<OrderedSelectListOptionDto> GetDestinationSelectList(int languageId);
        List<SelectListOptionDto> DestinationSelectListForCustomTour(int languageId);

    }
}
