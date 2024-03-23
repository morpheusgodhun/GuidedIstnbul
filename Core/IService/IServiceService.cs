using Core.Entities;
using Dto.ApiWebDtos.ApiToWebDtos.AdditionalService;
using Dto.ApiWebDtos.ApiToWebDtos.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IServiceService : IGenericService<Service>
    {
        public List<ServiceListItemDto> GetServiceList(int LanguageId);
        public GetServiceDetailDto GetServiceDetail(Guid serviceID, int LanguageID);
        public List<AdditionalServiceDto> GetServiceAdditionals(Guid ProductId, int LanguageID);

        //servis listeleme sayfasında min price hesaplamak için
        //addtional gönderdiğimizde onların ilgili en iyi fiyatlarını getirsin
        public decimal ServiceAdditionalsMinPriceForList(Guid ProductId, int LanguageID);

    }
}
