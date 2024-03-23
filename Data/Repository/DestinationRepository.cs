using Core.Entities;
using Core.IRepository;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class DestinationRepository : GenericRepository<Destination>, IDestinationRepository
    {
        Context _context;

        public DestinationRepository(Context context) : base(context)
        {
            _context = context;
        }

        public List<OrderedSelectListOptionDto> DestinationSelectList(int languageId)
        {
            List<OrderedSelectListOptionDto> value = (from destination in _context.Destinations.ToList()
                                                      where !destination.IsDeleted && destination.Status
                                                      join destinationLanguageItem in _context.DestinationLanguageItems.ToList()
                                                      on destination.Id equals destinationLanguageItem.DestinationID
                                                      where destinationLanguageItem.LangaugeID == languageId
                                                      orderby destination.Order
                                                      select new OrderedSelectListOptionDto()
                                                      {
                                                          OptionID = destination.Id,
                                                          Option = destinationLanguageItem.DisplayName,
                                                          Order = destination.Order
                                                      }).ToList();
            return value;
        }
        public List<SelectListOptionDto> DestinationSelectListForCustomTour(int languageId)
        {
            List<SelectListOptionDto> value = (from destination in _context.Destinations.ToList()
                                               where !destination.IsDeleted && destination.Status && destination.ShowOnCustomMade
                                               join destinationLanguageItem in _context.DestinationLanguageItems.ToList()
                                               on destination.Id equals destinationLanguageItem.DestinationID
                                               where destinationLanguageItem.LangaugeID == languageId
                                               select new SelectListOptionDto()
                                               {
                                                   OptionID = destination.Id,
                                                   Option = destinationLanguageItem.DisplayName
                                               }).ToList();
            return value;
        }
    }
}
