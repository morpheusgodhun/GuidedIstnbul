using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BlogCategory : BaseEntity
    {
        public int Order { get; set; }

        //Blog
        public ICollection<Blog> Blogs { get; set; }

        //BlogCategoryLanguageItem
        public ICollection<BlogCategoryLanguageItem> BlogCategoryLanguageItems { get; set; }
    }
}
