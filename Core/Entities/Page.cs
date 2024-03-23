using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Page : BaseEntity
    {
        public string PageName { get; set; }
        public string? ImagePath { get; set; }
        public int Type { get; set; }
        public bool IsHomePage { get; set; }
        public bool IsAllActive { get; set; }
        public bool IncludeSitemap { get; set; } = false;

        //LanguageItem
        public ICollection<PageLanguageItem> PageLanguageItems { get; set; }

        //ConstantValue
        public ICollection<ConstantValue> ConstantValues { get; set; }

    }
}
