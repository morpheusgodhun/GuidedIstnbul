using Core.Entities;
using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IInfoCardService : IGenericService<InfoCard>
    {
        List<InfoCardDto> GetInfoCardDtoList(int languageId);
    }
}
