using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Tour
{
    public class GetCategoryTourListDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public string TourCategoryBannerImagePath { get; set; }
        public string TourCategoryName { get; set; }
        public string TourCategoryDescription { get; set; }
        public List<TourListDto> Tours { get; set; }
    }
}
