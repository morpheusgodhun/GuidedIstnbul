using Core.Entities;
using Dto.ApiPanelDtos.SeoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface ISeoRepository : IGenericRepository<Seo>
    {
        List<SeoListDto> GetSeoListDto();
        List<SeoListByRouteIdDto> GetSeoListByRouteId(Guid routeId);
        void AddSeo(AddSeoDto addSeoDto);
        EditSeoDto GetEditSeoDto(Guid id);
        void EditSeo(EditSeoDto editSeoDto);

    }
}
