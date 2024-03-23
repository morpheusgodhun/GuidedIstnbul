using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Tour
{
    public class TourDto
    {
        public Guid ProductID { get; set; }
        public TourTypeDto TourSegment { get; set; }
        public string TourImagePath { get; set; }
        public string TourName { get; set; }
        public Decimal TourRate { get; set; }
        public int lId { get; set; }

        public decimal TourPrice { get; set; }

    }


}
