using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.TourCategoryDtos
{
    public class LanguageEditTourCategoryDto
    {
        public Guid TourCategoryLanguageID { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public string? Content { get; set; }
        public string? LanguageName { get; set; }
    }
}
