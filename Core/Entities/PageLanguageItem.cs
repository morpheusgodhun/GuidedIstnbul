using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PageLanguageItem : BaseEntity
    {
        public string? DisplayName { get; set; }
        public int LanguageId { get; set; }
        public string? Slug { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Content { get; set; }


        //Page

        [ForeignKey("Page")]
        public Guid PageID { get; set; }
        public Page Page { get; set; }
        [NotMapped]
        public bool SitemapInclude { get; set; }
    }
}
