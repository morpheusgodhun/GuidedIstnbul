using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.TourCategoryDtos
{
    public class TourCategoryListDto
    {
        public Guid TourCategoryID { get; set; }
        public bool Status { get; set; }
        public string CategoryName { get; set; }
        public string IconPath { get; set; }
    }
}
