using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Operation
{
    public class OperationShop : BaseEntity
    {
        [ForeignKey("Operation")]
        public Guid OperationId { get; set; }
        public Guid? SupplierId { get; set; } //sadece supplierType ı 2 olanlar olacak
        public int Day { get; set; }
        public DateTime? Date { get; set; }
        public int OperationShopStatus { get; set; }
        public Operation Operation { get; set; }
    }
}
