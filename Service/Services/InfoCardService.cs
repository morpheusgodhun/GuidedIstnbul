using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class InfoCardService : GenericService<InfoCard>, IInfoCardService
    {
        private readonly IInfoCardRepository _infoCardRepository;
        public InfoCardService(IGenericRepository<InfoCard> repository, IUnitOfWork unitOfWork, IInfoCardRepository infoCardRepository) : base(repository, unitOfWork)
        {
            _infoCardRepository = infoCardRepository;
        }

        public List<InfoCardDto> GetInfoCardDtoList(int languageId)
        {
            return _infoCardRepository.GetInfoCardDtoList(languageId);
        }
    }
}
