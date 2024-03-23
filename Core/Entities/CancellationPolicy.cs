using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CancellationPolicy : BaseEntity
    {
        public string Name { get; set; }
        public int UncancellableHours { get; set; }

        //CancellationPolicyLanguageItem
        public ICollection<CancellationPolicyLanguageItem> CancellationPolicyLanguageItems { get; set; }

        //Product
        public ICollection<Product> Products { get; set; }
    }
}
