using Core.Entities;
using Dto.ApiWebDtos.ApiToWebDtos.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IAdditionalServiceOptionPriceRepository : IGenericRepository<AdditionalServiceOptionPrice>
    {
        public List<AdditionalServicePriceDto> GetOptionPriceList(Guid optionId);
    }
}
