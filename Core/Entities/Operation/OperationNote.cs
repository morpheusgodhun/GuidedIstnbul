using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Operation
{
    public class OperationNote : BaseEntity
    {
        [ForeignKey("Operation")]
        public Guid OperationId { get; set; }
        public Operation Operation { get; set; }
        public int Day { get; set; }
        public int NoteStatus { get; set; }
        public DateTime? Date { get; set; }
        public string? Note { get; set; }
        public Guid? UserId { get; set; }
        public string? Hour { get; set; }
    }
}
