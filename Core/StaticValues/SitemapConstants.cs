using Core.StaticValues.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public static class SitemapConstants
    {
        public static List<SitemapInfo> SitemapInfo = new()
        {
            new ()
            {
                Location =  "sitemap_index",
                IsMainLocation = true,
            },
            new ()
            {
                Location =  "post-sitemap",
                RouteTemplateInfo = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.Blog),
            },
            new()
            {
                Location = "page-sitemap",
                RouteTemplateInfo = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.CustomPage),
            },
            new()
            {
                Location = "tour-sitemap",
                RouteTemplateInfo = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.Tour),
            },
            new()
            {
                Location = "category-sitemap",
                RouteTemplateInfo = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.BlogCategory),
            },
        };
        public static SitemapInfo GetByLocation(string location)
        {
            var sitemap = SitemapInfo.FirstOrDefault(x => x.Location == location);
            return sitemap;
        }
    }
    public class SitemapInfo
    {
        public string Location { get; set; }
        public RouteTemplate? RouteTemplateInfo { get; set; }
        public bool IsMainLocation { get; set; } = false;
    }
}
