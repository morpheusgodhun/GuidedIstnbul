using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Service
{
    public class GetServiceListDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<ServiceListItemDto> Services { get; set; }
        public string BannerImagePath { get; set; }
        public string PageName { get; set; }
    }

    public class ServiceListItemDto
    {
        public Guid ServiceID { get; set; }
        public string CardImagePath { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public string Slug { get; set; }
    }
}
