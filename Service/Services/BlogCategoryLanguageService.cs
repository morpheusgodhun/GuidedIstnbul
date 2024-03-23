using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues.Route;
using Dto.ApiPanelDtos.BlogCategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BlogCategoryLanguageService : GenericService<BlogCategoryLanguageItem>, IBlogCategoryLanguageService
    {
        readonly IRouteRepository _routeRepository;
        readonly ISeoRepository _seoRepository;
        public BlogCategoryLanguageService(IGenericRepository<BlogCategoryLanguageItem> repository, IUnitOfWork unitOfWork, IRouteRepository routeRepository, ISeoRepository seoRepository) : base(repository, unitOfWork)
        {
            _routeRepository = routeRepository;
            _seoRepository = seoRepository;
        }
        public override BlogCategoryLanguageItem Add(BlogCategoryLanguageItem entity)
        {
            RouteTemplate template = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.BlogCategory);
            string slugUrl = GetSlugUrl(entity);
            Route route = new(template.Controller, template.Action, entity.BlogCategoryID, entity.LanguageID, slugUrl, entity.SitemapInclude);
            _routeRepository.Add(route);

            Seo seo = new(entity.CategoryName, "", "", slugUrl, route.Id);
            _seoRepository.Add(seo);

            return base.Add(entity);

            //RouteTemplate template = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.BlogCategory);
            //return base.Add(entity);
            //return base.Add(entity);
        }
        public override void Update(BlogCategoryLanguageItem entity)
        {

            Route? existingRoute = _routeRepository.Where(x => x.EntityId == entity.BlogCategoryID && x.LanguageId == entity.LanguageID).FirstOrDefault();
            RouteTemplate routeTemplate = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.BlogCategory);
            string newSlugUrl = GetSlugUrl(entity);

            if (existingRoute is null)
            {
                Route route = new(routeTemplate.Controller, routeTemplate.Action, entity.BlogCategoryID, entity.LanguageID, newSlugUrl, entity.SitemapInclude);
                _routeRepository.Add(route);
                Seo? existingSeo = _seoRepository.Where(x => x.RouteId == route.Id).FirstOrDefault();

                if (existingSeo is null)
                {
                    Seo seo = new(entity.CategoryName, "", "", newSlugUrl, route.Id);
                    _seoRepository.Add(seo);
                }
            }
            else
            {
                bool isRouteUpdated = false;
                //route kontrol
                if (existingRoute.Url != newSlugUrl)
                {
                    existingRoute.Url = newSlugUrl;
                    isRouteUpdated = true;
                }
                if (!existingRoute.SitemapInclude.Equals(entity.SitemapInclude))
                {
                    existingRoute.SitemapInclude = entity.SitemapInclude;
                    isRouteUpdated = true;
                }
                if (isRouteUpdated) _routeRepository.Update(existingRoute);


                //seo kontrol
                Seo? existingSeo = _seoRepository.Where(x => x.RouteId == existingRoute.Id).FirstOrDefault();
                if (existingSeo is null)
                {
                    Seo seo = new(entity.CategoryName, "", "", newSlugUrl, existingRoute.Id);
                    _seoRepository.Add(seo);
                }
                else
                {
                    if (existingSeo.Link != newSlugUrl)
                    {
                        existingSeo.Link = newSlugUrl;
                        existingSeo.MetaTitle = entity.CategoryName;
                        _seoRepository.Update(existingSeo);
                    }
                }
            }

            base.Update(entity);
        }
        string GetSlugUrl(BlogCategoryLanguageItem entity)
        {
            return $"{LanguageList.GetPrefix(entity.LanguageID)}/category/{entity.Slug}";
        }
    }
}
