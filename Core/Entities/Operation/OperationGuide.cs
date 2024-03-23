using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Operation
{
    public class OperationGuide : BaseEntity
    {
        public Guid OperationId { get; set; }
        public Guid? GuideId { get; set; }
        public int Day { get; set; }
        public DateTime? Date { get; set; }
        public string? Note { get; set; }
        public int GuideStatus { get; set; } //operationGuideStatus
        public Operation Operation { get; set; }
        public Guide Guide { get; set; }
    }
}
