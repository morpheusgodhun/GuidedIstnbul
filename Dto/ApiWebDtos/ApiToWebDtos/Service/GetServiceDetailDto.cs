using Dto.ApiWebDtos.ApiToWebDtos.AdditionalService;
using Dto.ApiWebDtos.ApiToWebDtos.Blog;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Service
{
    public class GetServiceDetailDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public Guid ProductID { get; set; }
        public string ServiceBannerImagePath { get; set; }
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<AdditionalServiceDto> AdditionalService { get; set; }
        public List<ProductImageDto> Images { get; set; }
        public List<SelectListOptionDto> FindUsOptions { get; set; }
        public List<TripadvisorCommentDto> TripadvisorComments { get; set; }

    }
    



}
