using Core.Entities;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core.IService
{
    public interface IRouteService : IGenericService<Route>
    {
        RouteTemplateDto GetRouteTemplateBySlug(string slug);
    }
}
