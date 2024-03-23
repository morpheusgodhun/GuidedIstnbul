using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos
{
    public class ForgotPasswordPostDto
    {
        public string Email { get; set; }
        public string LanguagePrefix { get; set; }
    }
}
