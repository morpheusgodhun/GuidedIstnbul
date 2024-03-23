using Core.Entities;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IDestinationRepository : IGenericRepository<Destination>
    {
        List<OrderedSelectListOptionDto> DestinationSelectList(int languageId);
        public List<SelectListOptionDto> DestinationSelectListForCustomTour(int languageId);
    }
}
