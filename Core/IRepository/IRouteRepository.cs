using Core.Entities;
using Core.StaticValues.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IRouteRepository : IGenericRepository<Route>
    {
        Task<List<Route>> GetSitemapRoutesByRouteTemplateAsync(RouteTemplate routeTemplate, int languageId);
        Task<List<Route>> GetMatchingRoutesAsync(List<string> urls, int languageId);
    }
}
