using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public bool IsTour { get; set; }  //If 0 then its Service
        public string ProductName { get; set; }
        public string? YoutubeLink { get; set; }
        public string BannerImagePath { get; set; }
        public string CardImagePath { get; set; }
        public int CutoffHour { get; set; }
        public decimal CustomerDeposito { get; set; }
        public decimal AgentDeposito { get; set; }
        public int PaymentDate { get; set; }
        public decimal MinimumPayoutPercent { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool IsChildPolicyActive { get; set; }
        public bool IsPersonLimited { get; set; }
        public int PersonLimit { get; set; }
        public int Order { get; set; }

        //Tour
        [ForeignKey("Tour")]
        public Guid? TourID { get; set; }
        public Tour? Tour { get; set; }

        //Service

        [ForeignKey("Service")]
        public Guid? ServiceID { get; set; }
        public Service? Service { get; set; }

        //CancellationPolicy

        [ForeignKey("CancellationPolicy")]
        public Guid CancellationPolicyID { get; set; }
        public CancellationPolicy CancellationPolicy { get; set; }

        //Tag Many-To-Many
        public ICollection<Many_Product_Tag> Many_Product_Tags { get; set; }

        //Recomended_Blog
        public ICollection<Many_Product_RecomendedBlog> Many_Product_RecomendedBlogs { get; set; }


        //Blog_RecomendedTour (Blog Many-To-Many)
        public ICollection<Many_Blog_RecomendedTour> Many_Blog_RecomendedTours { get; set; }

        //Discount_Product Many-To-Many
        public ICollection<Many_Discount_Product> Many_Discount_Products { get; set; }

        ////TripadvisorComment 
        //public ICollection<TripadvisorComment> TripadvisorComments { get; set; }

        //ProductLanguageItem 
        public ICollection<ProductLanguageItem> ProductLanguageItems { get; set; }

        //ProductImage
        public ICollection<ProductImage> ProductImages { get; set; }

        //AdditionalService Many-To-Many
        public ICollection<Many_Product_AdditionalService> Many_Product_AdditionalServices { get; set; }

        //ProductFaq
        public ICollection<ProductFaq> ProductFaqs { get; set; }

        //ProductSellLimit
        public ICollection<ProductSellLimit> ProductSellLimits { get; set; }
        //Reservation
        public ICollection<Reservation> Reservations { get; set; }

    }
}
