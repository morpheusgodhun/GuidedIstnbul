using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.TourCategoryDtos
{
    public class AddTourCategoryDto
    {
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public string IconPath { get; set; }
        public string BannerImagePath { get; set; }
        public string? Content { get; set; }
    }
}
