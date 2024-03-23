using Core.Entities;
using Dto.ApiPanelDtos.SeoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core.IService
{
    public interface ISeoService : IGenericService<Seo>
    {
        List<SeoListDto> GetSeoListDto();
        List<SeoListByRouteIdDto> GetSeoListByRouteId(Guid routeId);
        void AddSeo(AddSeoDto addSeoDto);
        EditSeoDto GetEditSeoDto(Guid id);
        void EditSeo(EditSeoDto editSeoDto);

        XmlDocument GenerateSitemap(string location);

    }
}
