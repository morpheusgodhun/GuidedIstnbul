using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.GeneralDtos
{
    public class CustomPageConstantValueDto
    {
        public Dictionary<string, string> SlugDict { get; set; } = new Dictionary<string, string>();
        public List<ConstantValueDto> ConstantValues { get; set; }
    }
}
