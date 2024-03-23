using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Seo : BaseEntity
    {
        public Seo()
        {

        }
        public Seo(string metaTitle, string link)
        {
            MetaTitle = metaTitle;
            Link = link;
        }
        public Seo(string metaTitle,string metaKey,string metaDescription, string link, Guid routeId)
        {
            MetaTitle = metaTitle;
            MetaKey = metaKey;
            MetaDescription = metaDescription;
            Link = link;
            RouteId = routeId;
        }
        public string MetaTitle { get; set; }
        public string MetaKey { get; set; }
        public string MetaDescription { get; set; }
        public string Link { get; set; }

        [ForeignKey("Route")]
        public Guid? RouteId { get; set; }
        public Route Route { get; set; }
    }
}
