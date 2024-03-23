
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Profile
{
    public class GetChangePasswordDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public string UserID { get; set; }
    }
}
