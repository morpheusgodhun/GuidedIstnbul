using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues.Route;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PageLanguageService : GenericService<PageLanguageItem>, IPageLanguageService
    {
        readonly IRouteRepository _routeRepository;
        readonly ISeoRepository _seoRepository;
        public PageLanguageService(IGenericRepository<PageLanguageItem> repository, IUnitOfWork unitOfWork, IRouteRepository routeRepository, ISeoRepository seoRepository) : base(repository, unitOfWork)
        {
            _routeRepository = routeRepository;
            _seoRepository = seoRepository;
        }
        public override PageLanguageItem Add(PageLanguageItem entity)
        {
            RouteTemplate routeTemplate = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.CustomPage);

            string slugUrl = $"{LanguageList.GetPrefix(entity.LanguageId)}/{entity.Slug}";
            Route route = new(routeTemplate.Controller, routeTemplate.Action, entity.PageID, entity.LanguageId, slugUrl, entity.SitemapInclude);
            _routeRepository.Add(route);
            Seo seo = new(entity.DisplayName, "", "", slugUrl, route.Id);
            _seoRepository.Add(seo);

            return base.Add(entity);
        }
        public override void Update(PageLanguageItem entity)
        {
            Route? existingRoute = _routeRepository.Where(x => x.EntityId == entity.PageID && x.LanguageId == entity.LanguageId).FirstOrDefault();
            RouteTemplate routeTemplate = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.CustomPage);
            string newSlugUrl = GetSlugUrl(entity);

            if (existingRoute is null)
            {
                Route route = new(routeTemplate.Controller, routeTemplate.Action, entity.PageID, entity.LanguageId, newSlugUrl, entity.SitemapInclude);
                _routeRepository.Add(route);
                Seo? existingSeo = _seoRepository.Where(x => x.RouteId == route.Id).FirstOrDefault();

                if (existingSeo is null)
                {
                    Seo seo = new(entity.DisplayName, "", "", newSlugUrl, route.Id);
                    _seoRepository.Add(seo);
                }

            }

            else
            {
                Seo? existingSeo = _seoRepository.Where(x => x.RouteId == existingRoute.Id).FirstOrDefault();
                bool isRouteUpdated = false;

                if (existingRoute.Url != newSlugUrl)
                {
                    existingRoute.Url = newSlugUrl;
                    isRouteUpdated = true;
                }
                if (existingRoute.SitemapInclude != entity.SitemapInclude)
                {
                    existingRoute.SitemapInclude = entity.SitemapInclude;
                    isRouteUpdated = true;
                }
                if (isRouteUpdated) _routeRepository.Update(existingRoute);

                if (existingSeo is null)
                {
                    Seo seo = new(entity.DisplayName, "", "", newSlugUrl, existingRoute.Id);
                    _seoRepository.Add(seo);
                }
                else
                {
                    if (existingSeo.Link != newSlugUrl)
                    {
                        existingSeo.Link = newSlugUrl;
                        existingSeo.MetaTitle = entity.DisplayName;
                        _seoRepository.Update(existingSeo);
                    }
                }
            }



            base.Update(entity);
        }
        string GetSlugUrl(PageLanguageItem entity)
        {
            return $"{LanguageList.GetPrefix(entity.LanguageId)}/{entity.Slug}";
        }
    }
}
