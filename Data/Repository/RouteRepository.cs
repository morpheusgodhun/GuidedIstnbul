using Core.Entities;
using Core.IRepository;
using Core.StaticValues.Route;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class RouteRepository : GenericRepository<Route>, IRouteRepository
    {
        public RouteRepository(Context context) : base(context)
        {
        }

        public async Task<List<Route>> GetMatchingRoutesAsync(List<string> urls, int languageId)
        {
            var routes = await _context.Routes.Where(x => x.LanguageId == languageId).ToListAsync();

            var matchingRoutes = (from route in routes
                                  join url in urls on route.Url equals url
                                  select route).ToList();

            return matchingRoutes;
        }

        public async Task<List<Route>> GetSitemapRoutesByRouteTemplateAsync(RouteTemplate routeTemplate, int languageId)
        {
            var routes = await _context.Routes.Where(x => x.Controller == routeTemplate.Controller && x.Action == routeTemplate.Action && x.LanguageId == languageId).ToListAsync();
            return routes;
        }
    }
}
