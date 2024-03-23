using Core.Entities;
using Core.IRepository;
using Dto.ApiPanelDtos.DestinationDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_CustomTourRequest_DestinationRepository : GenericRepository<Many_CustomTourRequest_Destination>, IMany_CustomTourRequest_DestinationRepository
    {
        public Many_CustomTourRequest_DestinationRepository(Context context) : base(context)
        {
        }

        public List<DestinationListDto> GetDestinationsByRequestId(Guid customTourRequestId, int languageId = 1)
        {
            var destinations = _context.Many_CustomTourRequest_Destinations.Include(x => x.Destination).ThenInclude(d => d.DestinationLanguageItems).
                Where(x => x.CustomTourRequestId == customTourRequestId).Select(x => new DestinationListDto
                {
                    DestinationName = x.Destination.DestinationLanguageItems.FirstOrDefault(x => x.LangaugeID == languageId).DisplayName,
                    DestinationID = x.Id,
                }).ToList();
            return destinations;
        }
    }
}
