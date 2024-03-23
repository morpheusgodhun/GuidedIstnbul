using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.BlogDtos
{
    public class EditBlogDto
    {
        public Guid BlogID { get; set; }
        public Guid BlogCategoryID { get; set; }
        public List<Guid> TagIDs { get; set; }
        public List<Guid> RecommendedTourIDs { get; set; }
        public string CardImagePath { get; set; }
        public string BannerImagePath { get; set; }
        public DateTime Date { get; set; }

        [ValidateNever]
        public string? BlogTitle { get; set; }
    }
}
