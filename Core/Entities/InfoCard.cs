using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class InfoCard : BaseEntity
    {
        public string IconPath { get; set; }
        public ICollection<InfoCardLanguageItem> InfoCardLanguageItems { get; set; }
    }
}
