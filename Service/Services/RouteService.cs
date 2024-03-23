using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues;
using Core.StaticValues.Route;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Service.Services
{
    public class RouteService : GenericService<Route>, IRouteService
    {
        public RouteService(IGenericRepository<Route> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }



        public RouteTemplateDto GetRouteTemplateBySlug(string slug)

        {
            if (slug.EndsWith("/") || slug.StartsWith("/"))
                slug = slug.TrimEnd('/');
            var route = _repository.Where(x => x.Url.TrimEnd('/') == slug).FirstOrDefault();
            if (route is not null)
            {
                if (route.EntityId == null || route.EntityId == Guid.Empty)
                    return new RouteTemplateDto(route.Id, route.Controller, route.Action);
                else
                    return new RouteTemplateDto(route.Id, route.Controller, route.Action, (Guid)route.EntityId);
            }

            return null;
        }

    }
}
