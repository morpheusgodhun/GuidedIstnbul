using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.DestinationDtos
{
    public class DestinationListDto
    {
        public Guid DestinationID { get; set; }
        public bool Status { get; set; }
        public string DestinationName { get; set; }
        public bool ShowOnCustomMade { get; set; }
        public int Order { get; set; }
    }
}
