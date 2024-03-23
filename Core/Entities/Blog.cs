using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Blog : BaseEntity
    {

        public string CardImagePath { get; set; }
        public string BannerImagePath { get; set; }
        public bool ShowOnFAQ { get; set; }
        public bool ShowOnHomepage { get; set; }
        public DateTime Date { get; set; }

        //BlogLanguageItem
        public ICollection<BlogLanguageItem> BlogLanguageItems { get; set; }

        //BlogCategory

        public ICollection<Many_Blog_BlogCategory> BlogCategories { get; set; }


        [ForeignKey("BlogCategory")]
        public Guid BlogCategoryID { get; set; }
        public BlogCategory BlogCategory { get; set; }

        //Tag Many-To-Many
        public ICollection<Many_Blog_Tag> Many_Blog_Tags { get; set; }


        //Blog_RecomendedTour (Tour Many-To-Many)
        public ICollection<Many_Blog_RecomendedTour> Many_Blog_RecomendedTours { get; set; }

        //Recomended_Blog
        public ICollection<Many_Product_RecomendedBlog> Many_Product_RecomendedBlogs { get; set; }
    }
}
