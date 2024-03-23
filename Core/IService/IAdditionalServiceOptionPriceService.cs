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
    public interface IAdditionalServiceOptionPriceService : IGenericService<AdditionalServiceOptionPrice>
    {
        public List<AdditionalServicePriceDto> OptionAllPriceList(Guid optionId); // tüm fiyatları listeleyelim
        public AdditionalServicePriceDto OptionMinPriceForList(List<AdditionalServicePriceDto> priceList);
        public List<AdditionalServiceDto> SetOptionalPricesForTourDetail(List<AdditionalServiceDto> additionalServices); // tur detayı çekerken en ucuz fiyatı ve diğer tüm fiyatları çekmek için
    }
}
