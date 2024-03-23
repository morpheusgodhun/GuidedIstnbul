using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.BlogDtos
{
    public class AddBlogDto
    {
        public Guid? BlogCategoryID { get; set; }
        public List<Guid>? BlogCategoryIDs { get; set; }
        public List<Guid> TagIDs { get; set; }
        public List<Guid>? RecommendedTourIDs { get; set; }
        public string CardImagePath { get; set; }
        public string BannerImagePath { get; set; }
        public string BlogTitle { get; set; }
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public string Content1 { get; set; }
        public string? Content2 { get; set; }
        public DateTime Date { get; set; }
        public bool SitemapInclude { get; set; }

    }
}
