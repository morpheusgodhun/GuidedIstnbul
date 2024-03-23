using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.PersonPolicyDtos
{
    public class EditPersonPolicyDto
    {
        public Guid PersonPolicyID { get; set; }
        public int FromPerson { get; set; }
        public int ToPerson { get; set; }
        public int Order { get; set; }
    }
}
