using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.TourDtos
{
    public class TourListDto2
    {
        public Guid ProductID { get; set; }
        public bool ShowOnHomepage { get; set; }
        public bool Status { get; set; }
        public string TourType { get; set; }
        public string TourTitle { get; set; }
        public bool Uncompleted { get; set; }
        public int Order { get; set; }
        public Guid TourID { get; set; }
    }
}
