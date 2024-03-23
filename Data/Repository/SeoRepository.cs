using Core.Entities;
using Core.IRepository;
using Core.IService;
using Dto.ApiPanelDtos.SeoDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SeoRepository : GenericRepository<Seo>, ISeoRepository
    {
        DbSet<Seo> _seos;
        public SeoRepository(Context context) : base(context)
        {
            _seos = _context.Seos;
        }

        public void AddSeo(AddSeoDto addSeoDto)
        {
            Seo seo = new Seo()
            {
                MetaDescription = addSeoDto.MetaDescription,
                MetaKey = addSeoDto.MetaKey,
                MetaTitle = addSeoDto.MetaTitle,
                Link = addSeoDto.PageLink,
                //RouteId = addSeoDto.RouteId,
            };

            _seos.Add(seo);
        }

        public void EditSeo(EditSeoDto editSeoDto)
        {
            Seo seo = _seos.Find(editSeoDto.SeoID);

            seo.MetaDescription = editSeoDto.MetaDescription;
            seo.MetaTitle = editSeoDto.MetaTitle;
            seo.MetaKey = editSeoDto.MetaKey;
            seo.Link = editSeoDto.PageLink;
         //   seo.RouteId = editSeoDto.RouteId;

            _seos.Update(seo);
        }

        public EditSeoDto GetEditSeoDto(Guid id)
        {
            Seo seo = _seos.Find(id);

            EditSeoDto editSeoDto = new EditSeoDto()
            {
                SeoID = seo.Id,
                MetaDescription = seo.MetaDescription,
                MetaTitle = seo.MetaTitle,
                MetaKey = seo.MetaKey,
                PageLink = seo.Link,
                RouteId = seo.RouteId
            };
            return editSeoDto;
        }

        public List<SeoListByRouteIdDto> GetSeoListByRouteId(Guid routeId)
        {

            var ss = _context.Seos;

            List<SeoListByRouteIdDto> seoListDtos = (from seo in _seos.ToList()
                                            where !seo.IsDeleted && seo.RouteId == routeId
                                            select new SeoListByRouteIdDto()
                                            {
                                                MetaTitle = seo.MetaTitle,
                                                MetaKey=seo.MetaKey,
                                                MetaDescription=seo.MetaDescription,
                                                Link = seo.Link,
                                                RouteId = seo.RouteId
                                            }).ToList();
            return seoListDtos;
        }

        public List<SeoListDto> GetSeoListDto()
        {
            List<SeoListDto> seoListDtos = (from seo in _seos.ToList()
                                            where !seo.IsDeleted
                                            select new SeoListDto()
                                            {
                                                SeoID = seo.Id,
                                                MetaKey = seo.MetaKey,
                                                MetaTitle = seo.MetaTitle,
                                                MetaDescription = seo.MetaDescription,
                                                PageLink = seo.Link
                                            }).ToList();
            return seoListDtos;
        }
    }
}
