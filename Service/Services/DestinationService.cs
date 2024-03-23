using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Data.Repository;
using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DestinationService : GenericService<Destination>, IDestinationService
    {
        private readonly IDestinationRepository _destinationRepository;
        public DestinationService(IGenericRepository<Destination> repository, IUnitOfWork unitOfWork, IDestinationRepository destinationRepository) : base(repository, unitOfWork)
        {
            _destinationRepository = destinationRepository;
        }

        public List<SelectListOptionDto> DestinationSelectListForCustomTour(int languageId)
        {
            return _destinationRepository.DestinationSelectListForCustomTour(languageId);
        }

        public List<OrderedSelectListOptionDto> GetDestinationSelectList(int languageId)
        {
            return _destinationRepository.DestinationSelectList(languageId);
        }
    }
}
