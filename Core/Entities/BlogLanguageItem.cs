using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BlogLanguageItem : BaseEntity
    {

        public string BlogTitle { get; set; }
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public int LanguageID { get; set; }

        //Blog

        [ForeignKey("Blog")]
        public Guid BlogID { get; set; }
        public Blog Blog { get; set; }

        [NotMapped] //ef core görmesin
        public bool SitemapInclude { get; set; }

    }
}
