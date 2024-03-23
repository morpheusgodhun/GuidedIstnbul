using Dto.ApiWebDtos.ApiToWebDtos.About;
using Dto.ApiWebDtos.ApiToWebDtos.Blog;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Home
{
    public class GetHomeDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<TourListDto> BestDealTours { get; set; }
        public List<TourListDto> PrivateTours { get; set; }
        public List<TourListDto> TurkeyTourPackages { get; set; }
        public List<BlogListItemDto> Blogs { get; set; }
        public PageDto AboutPage { get; set; }
        public string IstanbulToursSlug { get; set; }
        public string TurkeyToursSlug { get; set; }
    }
}
