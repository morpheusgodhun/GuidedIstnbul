using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues.Route;

namespace Service.Services
{
    public class BlogLanguageService : GenericService<BlogLanguageItem>, IBlogLanguageService
    {
        readonly IRouteRepository _routeRepository;
        readonly ISeoRepository _seoRepository;
        public BlogLanguageService(IGenericRepository<BlogLanguageItem> repository, IUnitOfWork unitOfWork, IRouteRepository routeRepository, ISeoRepository seoRepository) : base(repository, unitOfWork)
        {
            _routeRepository = routeRepository;
            _seoRepository = seoRepository;
        }
        public override BlogLanguageItem Add(BlogLanguageItem entity)
        {
            RouteTemplate template = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.Blog);
            string slugUrl = GetSlugUrl(entity);
            Route route = new(template.Controller, template.Action, entity.BlogID, entity.LanguageID, slugUrl, entity.SitemapInclude);
            _routeRepository.Add(route);
            Seo seo = new(entity.BlogTitle, "", "", slugUrl, route.Id);
            _seoRepository.Add(seo);
            //return base.Add(entity);S
            return base.Add(entity);
        }
        public override void Update(BlogLanguageItem entity)
        {
            Route? existingRoute = _routeRepository.Where(x => x.EntityId == entity.BlogID && x.LanguageId == entity.LanguageID).FirstOrDefault();
            RouteTemplate routeTemplate = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.Blog);
            string newSlugUrl = GetSlugUrl(entity);

            if (existingRoute is null)
            {

                Route route = new(routeTemplate.Controller, routeTemplate.Action, entity.BlogID, entity.LanguageID, newSlugUrl, entity.SitemapInclude);
                _routeRepository.Add(route); //routeId oluştu
                Seo? existingSeo = _seoRepository.Where(x => x.RouteId == route.Id).FirstOrDefault();

                if (existingSeo is null)
                {
                    Seo seo = new(entity.BlogTitle, "", "", newSlugUrl, route.Id);
                    _seoRepository.Add(seo);
                }
            }

            else //varsa güncelle
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
                    Seo seo = new(entity.BlogTitle, "", "", newSlugUrl, existingRoute.Id);
                    _seoRepository.Add(seo);
                }
                else
                {
                    if (existingSeo.Link != newSlugUrl)
                    {
                        existingSeo.Link = newSlugUrl;
                        existingSeo.MetaTitle = entity.BlogTitle;
                        _seoRepository.Update(existingSeo);
                    }
                }
            }
            base.Update(entity);
        }
        string GetSlugUrl(BlogLanguageItem entity)
        {
            return $"{LanguageList.GetPrefix(entity.LanguageID)}/{entity.Slug}";
        }
    }
}
