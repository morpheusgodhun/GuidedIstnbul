using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GuideLanguage:BaseEntity
    {
        public Guid GuideId { get; set; }
        public Guid LanguageId { get; set; }
    }
}
