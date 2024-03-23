using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AdditionalServiceDtos
{
    public class AddAdditionalServiceOptionDto
    {
        public Guid AdditionalServiceID { get; set; }
        public string OptionName { get; set; }
        public List<Guid> FormInputIDs { get; set; }
        public int Order { get; set; }
    }
}
