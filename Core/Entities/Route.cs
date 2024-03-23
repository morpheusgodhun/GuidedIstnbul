using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Route : BaseEntity
    {
        public Route(string controller, string action, Guid entityId, int languageId, string url, bool sitemapInclude)
        {
            Controller = controller;
            Action = action;
            EntityId = entityId;
            LanguageId = languageId;
            Url = url;
            SitemapInclude = sitemapInclude;
        }

        public Route()
        {

        }

        public string Controller { get; set; }
        public string Action { get; set; }
        public Guid? EntityId { get; set; }
        public int LanguageId { get; set; }
        public string Url { get; set; }
        public bool SitemapInclude { get; set; }
        public virtual ICollection<Seo> Seos { get; set; }
    }
}
