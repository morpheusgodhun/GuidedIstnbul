using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues.Route;
using Data.Repository;

namespace Service.Services
{
    public class TagLanguageService : GenericService<TagLanguageItem>, ITagLanguageService
    {
        readonly ITagLanguageRepository _tagLanguageRepository;
        readonly IRouteRepository _routeRepository;
        readonly ISeoRepository _seoRepository;
        public TagLanguageService(IGenericRepository<TagLanguageItem> repository, IUnitOfWork unitOfWork, ITagLanguageRepository tagLanguageRepository, IRouteRepository routeRepository, ISeoRepository seoRepository) : base(repository, unitOfWork)
        {
            _tagLanguageRepository = tagLanguageRepository;
            _routeRepository = routeRepository;
            _seoRepository = seoRepository;
        }
        public override TagLanguageItem Add(TagLanguageItem entity)
        {
            RouteTemplate template = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.BlogTag);
            string slugUrl = GetSlugUrl(entity);
            Route route = new(template.Controller, template.Action, entity.TagID, entity.LangaugeID, slugUrl, false);
            _routeRepository.Add(route);

            Seo seo = new(entity.DisplayName, "", "", slugUrl, route.Id);
            _seoRepository.Add(seo);

            return base.Add(entity);
        }
        public override void Update(TagLanguageItem entity)
        {
            Route? existingRoute = _routeRepository.Where(x => x.EntityId == entity.TagID && x.LanguageId == entity.LangaugeID).FirstOrDefault();
            RouteTemplate routeTemplate = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.BlogTag);
            string newSlugUrl = GetSlugUrl(entity);

            if (existingRoute is null)
            {
                Route route = new(routeTemplate.Controller, routeTemplate.Action, entity.TagID, entity.LangaugeID, newSlugUrl, false);
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

                if (existingSeo is null)
                {
                    if (existingRoute.Url != newSlugUrl)
                    {
                        existingRoute.Url = newSlugUrl;
                        _routeRepository.Update(existingRoute);
                    }
                    Seo seo = new(entity.DisplayName, "", "", newSlugUrl, existingRoute.Id);
                    _seoRepository.Add(seo);
                }
                else
                {
                    if (existingRoute.Url != newSlugUrl)
                    {
                        existingRoute.Url = newSlugUrl;
                        _routeRepository.Update(existingRoute);

                    }
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
        public string GetTagDisplayName(Guid tagId, int languageId)
        {
            return _tagLanguageRepository.GetTagDisplayName(tagId, languageId);
        }
        string GetSlugUrl(TagLanguageItem entity)
        {
            return $"{LanguageList.GetPrefix(entity.LangaugeID)}/tag/{entity.Slug}";
        }
    }
}
