
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.PageComponets
{
    public class GetInfoCardDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<InfoCardDto> InfoCards { get; set; }
    }

    public class InfoCardDto
    {
        public string IconPath { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

}
