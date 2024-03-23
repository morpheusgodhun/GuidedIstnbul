using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.GeneralDtos
{
    public class GetRouteTemplateDto
    {
        public GetRouteTemplateDto(string slug)
        {
            Slug = slug;
        }
        public GetRouteTemplateDto()
        {

        }
        public string Slug { get; set; }
    }
}
