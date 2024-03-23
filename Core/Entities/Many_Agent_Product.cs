using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Agent_Product:BaseEntity
    {
        public Guid AgentId { get; set; }
        public Guid ProductId { get; set; }
        public int DiscountRate { get; set; }
    }
}
