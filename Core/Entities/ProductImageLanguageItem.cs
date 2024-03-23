using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductImageLanguageItem : BaseEntity
    {

        //Şu an yok ama eğer istenirse eklenecek
        public string Title { get; set; }

        //ProductImage

        [ForeignKey("ProductImage")]
        public Guid ProductImageID { get; set; }
        public ProductImage ProductImage { get; set; }
    }
}
