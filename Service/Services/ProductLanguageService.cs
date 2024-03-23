using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues.Route;
using Dto.ApiWebDtos.WebToApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductLanguageService : GenericService<ProductLanguageItem>, IProductLanguageService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly ISeoRepository _seoRepository;


        public ProductLanguageService(IGenericRepository<ProductLanguageItem> repository, IUnitOfWork unitOfWork, IReservationRepository reservationRepository, IRouteRepository routeRepository, ISeoRepository seoRepository) : base(repository, unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _routeRepository = routeRepository;
            _seoRepository = seoRepository;
        }

        public ProductNameDto GetProductNameForReservation(Guid reservationId, int languageId)
        {
            var reservation = _reservationRepository.GetReservationById(reservationId);
            return new ProductNameDto { ProductName = reservation.Product.ProductName, Id = reservation.ProductId, IsTour = reservation.Product.IsTour };
        }
        public override ProductLanguageItem Add(ProductLanguageItem entity)
        {
            RouteTemplateConstants.No routeTemplateEnum = entity.Product.IsTour ? RouteTemplateConstants.No.Tour : RouteTemplateConstants.No.Service;
            RouteTemplate routeTemplate = RouteTemplateConstants.GetRouteTemplate(routeTemplateEnum);

            string slugUrl = $"{LanguageList.GetPrefix(entity.LanguageID)}/{entity.Slug}";

            Route route = new(routeTemplate.Controller, routeTemplate.Action, entity.ProductID, entity.LanguageID, slugUrl, entity.SitemapInclude);
            _routeRepository.Add(route);
            Seo seo = new(entity.DisplayName, "", "", slugUrl, route.Id);
            _seoRepository.Add(seo);

            return base.Add(entity);
        }

        public override void Update(ProductLanguageItem entity)
        {
            Route? existingRoute = _routeRepository.Where(x => x.EntityId == entity.ProductID && x.LanguageId == entity.LanguageID).FirstOrDefault();
            var routeTemplateEnum = entity.Product.IsTour ? RouteTemplateConstants.No.Tour : RouteTemplateConstants.No.Service;
            RouteTemplate routeTemplate = RouteTemplateConstants.GetRouteTemplate(routeTemplateEnum);
            string newSlugUrl = GetSlugUrl(entity);

            //bu languageItem için bir route & seo yoksa yeni route & seo oluştur

            if (existingRoute is null)
            {
                Route route = new(routeTemplate.Controller, routeTemplate.Action, entity.ProductID, entity.LanguageID, newSlugUrl, entity.SitemapInclude);
                _routeRepository.Add(route); //routeId oluştu
                Seo? existingSeo = _seoRepository.Where(x => x.RouteId == route.Id).FirstOrDefault();

                if (existingSeo is null)
                {
                    Seo seo = new(entity.DisplayName, "", "", newSlugUrl, route.Id);
                    _seoRepository.Add(seo);
                }
            }
            else  //varsa güncelle
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

        string GetSlugUrl(ProductLanguageItem entity)
        {
            return $"{LanguageList.GetPrefix(entity.LanguageID)}/{entity.Slug}";
        }

    }
}
