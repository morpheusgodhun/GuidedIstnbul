using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OfferTemplate : BaseEntity
    {
        public int OfferTemplateCategoryId { get; set; }
        public string Name { get; set; }
        public string Program { get; set; }
    }
}
