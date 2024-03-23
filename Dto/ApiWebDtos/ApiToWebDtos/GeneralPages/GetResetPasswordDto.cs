
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.GeneralPages
{
    public class GetResetPasswordDto
    {
        public string UserId { get; set; }
        public string UrlCode { get; set; }
        public List<ConstantValueDto> ConstantValues { get; set; }
    }
}
