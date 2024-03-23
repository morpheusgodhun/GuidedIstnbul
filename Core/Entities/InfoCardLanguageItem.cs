using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class InfoCardLanguageItem : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int LanguageID { get; set; }

        public Guid InfoCardID { get; set; }
        public InfoCard InfoCard { get; set; }
    }
}
