using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Tag : BaseEntity
    {
        public string IconPath { get; set; }
        public string TagCategories { get; set; }

        //TagLanguage
        public ICollection<TagLanguageItem> TagLanguageItems { get; set; }

        //Blog Many-To-Many
        public ICollection<Many_Blog_Tag> Many_Blog_Tags { get; set; }

        ////Product Many-To-Many
        //public ICollection<Many_Product_Tag> Many_Product_Tags { get; set; }

    }
}
