using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues.Route;
using Data.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ServiceLanguageService : GenericService<ServiceLanguageItem>, IServiceLanguageService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly ISeoRepository _seoRepository;
        public ServiceLanguageService(IGenericRepository<ServiceLanguageItem> repository, IUnitOfWork unitOfWork, IRouteRepository routeRepository, ISeoRepository seoRepository) : base(repository, unitOfWork)
        {
            _routeRepository = routeRepository;
            _seoRepository = seoRepository;
        }
        //public override ServiceLanguageItem Add(ServiceLanguageItem entity)
        //{
        //    //RouteTemplate template = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.Service);

        //    //string slugUrl = $"{LanguageList.GetPrefix(entity.LanguageID)}/{"
        //    //Route route = new(template.Controller, template.Action, entity.ServiceID, entity.LanguageID);

        //    //return base.Add(entity);
        //}
        public override void Update(ServiceLanguageItem entity)
        {

            //Route? existingRoute = _routeRepository.Where(x => x.EntityId == entity.ServiceID && x.LanguageId == entity.LanguageID).FirstOrDefault();
            //RouteTemplate routeTemplate = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.Service);
            //string newSlugUrl = GetSlugUrl(entity);

            //if (existingRoute is null)
            //{

            //    Route route = new(routeTemplate.Controller, routeTemplate.Action, entity.ServiceID, entity.LanguageID, newSlugUrl);
            //    _routeRepository.Add(route);
            //    Seo? existingSeo = _seoRepository.Where(x => x.RouteId == route.Id).FirstOrDefault();

            //    if (existingSeo is null)
            //    {
            //        Seo seo = new(entity.ShortDescription, "", "", newSlugUrl, route.Id);
            //        _seoRepository.Add(seo);
            //    }

            //}

            //else
            //{
            //    Seo? existingSeo = _seoRepository.Where(x => x.RouteId == existingRoute.Id).FirstOrDefault();

            //    if (existingSeo is null)
            //    {
            //        if (existingRoute.Url != newSlugUrl)
            //        {
            //            existingRoute.Url = newSlugUrl;
            //            _routeRepository.Update(existingRoute);
            //        }
            //        Seo seo = new(entity.ShortDescription, "", "", newSlugUrl, existingRoute.Id);
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
            //            existingSeo.MetaTitle = entity.ShortDescription;
            //            _seoRepository.Update(existingSeo);
            //        }
            //    }
            //}
            base.Update(entity);
        }
        //string GetSlugUrl(ServiceLanguageItem entity)
        //{
        //    return $"{LanguageList.GetPrefix(entity.LanguageID)}/{entity.Slug}";
        //}
    }
}
