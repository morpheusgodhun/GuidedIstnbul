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
    public class TourCategoryLanguageService : GenericService<TourCategoryLanguageItem>, ITourCategoryLanguageService
    {
        readonly IRouteRepository _routeRepository;
        readonly ISeoRepository _seoRepository;
        readonly ITourCategoryRepository _tourCategoryRepository;
        public TourCategoryLanguageService(IGenericRepository<TourCategoryLanguageItem> repository, IUnitOfWork unitOfWork, IRouteRepository routeRepository, ISeoRepository seoRepository, ITourCategoryRepository tourCategoryRepository) : base(repository, unitOfWork)
        {
            _routeRepository = routeRepository;
            _seoRepository = seoRepository;
            _tourCategoryRepository = tourCategoryRepository;
        }
        public override TourCategoryLanguageItem Add(TourCategoryLanguageItem entity)
        {
            RouteTemplate routeTemplate = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.TourCategory);

            string slugUrl = $"{LanguageList.GetPrefix(entity.LanguageID)}/{entity.Slug}";
            Route route = new(routeTemplate.Controller, routeTemplate.Action, entity.TourCategoryID, entity.LanguageID, slugUrl, true);
            _routeRepository.Add(route);

            Seo seo = new(entity.CategoryName, "", "", slugUrl, route.Id);
            _seoRepository.Add(seo);


            return base.Add(entity);
        }
        public override void Update(TourCategoryLanguageItem entity)
        {
            Route? existingRoute = _routeRepository.Where(x => x.EntityId == entity.TourCategoryID && x.LanguageId == entity.LanguageID).FirstOrDefault();
            RouteTemplate routeTemplate = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.TourCategory);
            string newSlugUrl = GetSlugUrl(entity);

            if (existingRoute is null)
            {

                Route route = new(routeTemplate.Controller, routeTemplate.Action, entity.TourCategoryID, entity.LanguageID, newSlugUrl, true);
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
                Seo? existingSeo = _seoRepository.Where(x => x.RouteId == existingRoute.Id).FirstOrDefault();

                if (existingSeo is null)
                {
                    if (existingRoute.Url != newSlugUrl)
                    {
                        existingRoute.Url = newSlugUrl;
                        _routeRepository.Update(existingRoute);
                    }
                    Seo seo = new(entity.CategoryName, "", "", newSlugUrl, existingRoute.Id);
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
                        existingSeo.MetaTitle = entity.CategoryName;
                        _seoRepository.Update(existingSeo);
                    }
                }
            }


            //Route existingRoute = _routeRepository.Where(x => x.EntityId == entity.TourCategoryID && x.LanguageId == entity.LanguageID).FirstOrDefault();
            //Seo existingSeo = _seoRepository.Where(x => x.RouteId == existingRoute.Id).FirstOrDefault();

            //string newSlugUrl = $"{LanguageList.GetPrefix(entity.LanguageID)}/{entity.Slug}";

            //if (existingRoute is null)
            //{
            //    RouteTemplate routeTemplate = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.TourCategory);

            //    Route route = new(routeTemplate.Controller, routeTemplate.Action, entity.TourCategoryID, entity.LanguageID, newSlugUrl);
            //    _routeRepository.Add(route);
            //    if (existingSeo is null)
            //    {
            //        Seo seo = new(entity.CategoryName, "", "", newSlugUrl, route.Id);
            //        _seoRepository.Add(seo);
            //    }
            //}
            //else
            //{
            //    if (existingSeo is null)
            //    {
            //        if (existingRoute.Url != newSlugUrl)
            //        {
            //            existingRoute.Url = newSlugUrl;
            //            _routeRepository.Update(existingRoute);
            //        }
            //        Seo seo = new(entity.CategoryName, "", "", newSlugUrl, existingRoute.Id);
            //        _seoRepository.Add(seo);
            //    }
            //    else
            //    {
            //        if (existingRoute.Url != newSlugUrl)
            //        {
            //            existingRoute.Url = newSlugUrl;
            //            _routeRepository.Update(existingRoute);

            //        }
            //        if (existingSeo.Link != newSlugUrl)
            //        {
            //            existingSeo.Link = newSlugUrl;
            //            existingSeo.MetaTitle = entity.CategoryName;
            //            _seoRepository.Update(existingSeo);
            //        }
            //    }
            //}
            base.Update(entity);
        }


        string GetSlugUrl(TourCategoryLanguageItem entity)
        {
            return $"{LanguageList.GetPrefix(entity.LanguageID)}/{entity.Slug}";
        }
        public async Task<string> GetSlugByTourCategoryAsync(Guid tourCategoryId, int languageId)
        {
            var tourCategory = await _tourCategoryRepository.GetByIdAsync(tourCategoryId);
            return tourCategory.TourCategoryLanguageItems.FirstOrDefault(x => x.LanguageID == languageId).Slug;
        }
    }
}
