using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BlogCategoryLanguageItem : BaseEntity
    {
        public string CategoryName { get; set; }
        public int LanguageID { get; set; }
        public string Slug { get; set; }

        //BlogCategory
        [ForeignKey("BlogCategory")]
        public Guid BlogCategoryID { get; set; }
        public BlogCategory BlogCategory { get; set; }

        [NotMapped]
        public bool SitemapInclude { get; set; }

    }
}
