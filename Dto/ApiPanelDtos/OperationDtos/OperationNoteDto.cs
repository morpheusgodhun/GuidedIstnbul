using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.OperationDtos
{
    public class OperationNoteDto
    {
        public string OperationNoteId { get; set; }
        public string? Note { get; set; }
        public int Day { get; set; }
        public DateTime? Date { get; set; }
        public string? UserId { get; set; }
        public string? Hour { get; set; }
    }
}
