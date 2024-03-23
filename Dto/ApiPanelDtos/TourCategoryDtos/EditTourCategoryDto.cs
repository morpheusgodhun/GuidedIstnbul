using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.TourCategoryDtos
{
    public class EditTourCategoryDto
    {
        public Guid TourCategoryID { get; set; }
        public string IconPath { get; set; }
        public string BannerImagePath { get; set; }
    }
}
