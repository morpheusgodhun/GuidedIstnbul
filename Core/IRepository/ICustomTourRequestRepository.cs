using Core.Entities;
using Dto.ApiPanelDtos.CustomTourDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Home;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.WebToApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface ICustomTourRequestRepository : IGenericRepository<CustomTourRequest>
    {
        public CustomMadeTourPostDto AddRequest(CustomMadeTourPostDto customTour);
        public List<CustomTourRequestListItemDto> CustomTourRequestList();
        public List<OfferListDto> OfferListByRequestId(Guid requestId);
        public void AddOffer(AddOfferDto addOfferDto);
        public void AnswerOffer(AnswerOfferDto dto);
        public CustomTourOfferCustomerAnswer AddCustomerAnswer(CustomTourOfferCustomerAnswer dto);
        public GetCustomTourRequestDetailDto CustomTourRequestDetail(Guid requestId, int languageId);
    }
}
