using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.OperationDtos
{
    public class OperationTicketDto
    {
        public string OperationTicketId { get; set; }
        public string Price { get; set; }
        public int Day { get; set; }
        public DateTime? Date { get; set; }
    }
}
