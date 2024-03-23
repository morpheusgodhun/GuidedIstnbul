
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Tour
{
    public class TourListDto
    {
        public TourListDto()
        {

        }
        public Guid ProductID { get; set; }
        public string CardImagePath { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Rate { get; set; }
        public string Slug { get; set; }
        public TourTypeDto TourType { get; set; }  //Custom (3) - Private (2) - Top Seller (1)
        public Guid TourId { get; set; }
        public int Order { get; set; }
        public bool AskForPrice { get; set; }
    }

    public class TourTypeDto
    {
        public string TypeName { get; set; }
        public string TypeClass { get; set; }
    }
}
