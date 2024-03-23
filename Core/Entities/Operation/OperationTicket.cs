using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Operation
{
    public class OperationTicket : BaseEntity
    {
        [ForeignKey("Operation")]
        public Guid OperationId { get; set; }
        public int Day { get; set; }
        public DateTime? Date { get; set; }
        public string? Note { get; set; }
        public decimal? Price { get; set; }
        public Operation Operation { get; set; }
    }
}
