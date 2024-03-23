using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Shop : BaseEntity
    {
        public string Name { get; set; }
        public string? Address { get; set; }

        [ForeignKey("Supplier")]
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
