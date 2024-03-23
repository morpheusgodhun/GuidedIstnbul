using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Operation
{
    public class OperationAdditionalService : BaseEntity
    {
        public Guid OperationId { get; set; }
        public Operation Operation { get; set; }
        public Guid? AdditionalServiceId { get; set; }
        public AdditionalService AdditionalService { get; set; }
        public int Day { get; set; }
        public DateTime? Date { get; set; }
        public string? Note { get; set; }
        public int AdditionalServiceStatus { get; set; }
    }
}
