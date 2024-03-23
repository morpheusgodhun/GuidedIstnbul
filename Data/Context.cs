
using Core.Entities;
using Core.Entities.Operation;
using Core.StaticClass;
using Core.StaticValues.Route;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        //Mail işleri
        #region DbSets
        public DbSet<SendMail> SendMails { get; set; }
        public DbSet<Many_Agent_Product> Many_Agent_Products { get; set; }
        public DbSet<SendMailTemplate> SendMailTemplates { get; set; }
        public DbSet<SendMailTemplateLanguageItem> SendMailTemplateLanguageItems { get; set; }

        //CustomTour
        public DbSet<OfferTemplate> OfferTemplates { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<CustomTourRequest> CustomTourRequests { get; set; }
        public DbSet<Many_CustomTourRequest_AlsoInterested> Many_CustomTourRequest_AlsoInteresteds { get; set; }
        public DbSet<Many_CustomTourRequest_Destination> Many_CustomTourRequest_Destinations { get; set; }
        //CustomTour End

        //Reservation
        public DbSet<ReservationParticipant> ReservationParticipants { get; set; }
        public DbSet<ReservationPayment> ReservationPayments { get; set; }
        public DbSet<ReservationBillingInfo> ReservationBillingInfos { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationOptionInput> ReservationOptionInputs { get; set; }
        public DbSet<Many_Reservation_AdditionalServiceOption> Many_Reservation_AdditionalServiceOptions { get; set; }
        public DbSet<Many_ReservationForm_AlsoInterested> Many_ReservationForm_AlsoInteresteds { get; set; }

        //Reservation End

        public DbSet<InfoCard> InfoCards { get; set; }
        public DbSet<InfoCardLanguageItem> InfoCardLanguageItems { get; set; }
        public DbSet<AdditionalService> AdditionalServices { get; set; }
        public DbSet<AdditionalServiceInput> AdditionalServiceInputs { get; set; }
        public DbSet<AdditionalServiceInputLanguageItem> AdditionalServiceInputLanguageItems { get; set; }
        public DbSet<AdditionalServiceLanguageItem> AdditionalServiceLanguageItems { get; set; }
        public DbSet<AdditionalServiceOption> AdditionalServiceOptions { get; set; }
        public DbSet<AdditionalServiceOptionLanguageItem> AdditionalServiceOptionLanguageItems { get; set; }
        public DbSet<AdditionalServiceOptionPriceDate> AdditionalServiceOptionPriceDates { get; set; }
        public DbSet<AdditionalServiceOptionPrice> AdditionalServiceOptionPrices { get; set; }
        public DbSet<Many_AdditionalServiceOption_AdditionalServiceInput> Many_AdditionalServiceOption_AdditionalServiceInputs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<DestinationLanguageItem> DestinationLanguageItems { get; set; }
        public DbSet<CancellationPolicy> CancellationPolicies { get; set; }
        public DbSet<CancellationPolicyLanguageItem> CancellationPolicyLanguageItems { get; set; }
        public DbSet<ChildPolicy> ChildPolicies { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<FaqLangaugeItem> FaqLangaugeItems { get; set; }
        public DbSet<FaqCategory> FaqCategories { get; set; }
        public DbSet<FaqCategoryLanguageItem> FaqCategoryLanguageItems { get; set; }
        public DbSet<PersonPolicy> PersonPolicies { get; set; }
        public DbSet<Survey> Surveies { get; set; }
        public DbSet<SurveyLanguageItem> SurveyLanguageItems { get; set; }
        public DbSet<ConstantValue> ConstantValues { get; set; }
        public DbSet<ConstantValueLanguageItem> ConstantValueLanguageItems { get; set; }
        public DbSet<SystemOption> SystemOptions { get; set; }
        public DbSet<SystemOptionLanguageItem> SystemOptionLanguageItems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagLanguageItem> TagLanguageItems { get; set; }
        public DbSet<TourCategory> TourCategories { get; set; }
        public DbSet<TourCategoryLanguageItem> TourCategoryLanguageItems { get; set; }
        public DbSet<Seo> Seos { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactInfoLanguageItem> ContactInfoLanguageItems { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageLanguageItem> PageLanguageItems { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogCategoryLanguageItem> BlogCategoryLanguageItems { get; set; }
        public DbSet<Many_Blog_Tag> Many_Blog_Tags { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        //public DbSet<Driver> Drivers { get; set; } //driver rolü açtık, artık kullanmıyoruz
        public DbSet<BlogLanguageItem> BlogLanguageItems { get; set; }
        //Product
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceLanguageItem> ServiceLanguageItems { get; set; }
        public DbSet<Many_Blog_RecomendedTour> Many_Blog_RecomendedTours { get; set; }
        public DbSet<Many_Product_AdditionalService> Many_Product_AdditionalServices { get; set; }
        public DbSet<Many_Product_RoleTemplate> Many_Product_RoleTemplates { get; set; }
        public DbSet<Many_Product_AdditionalServiceOption> Many_Product_AdditionalServiceOptions { get; set; }
        public DbSet<Many_Product_Tag> Many_Product_Tags { get; set; }
        public DbSet<Many_Tour_Destination> Many_Tour_Destinations { get; set; }
        public DbSet<Many_Tour_TourCategory> Many_Tour_TourCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFaq> ProductFaqs { get; set; }
        public DbSet<ProductFaqLanguageItem> ProductFaqLanguageItems { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductLanguageItem> ProductLanguageItems { get; set; }
        public DbSet<ProductSellLimit> ProductSellLimits { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourLanguageItem> TourLanguageItems { get; set; }
        public DbSet<TourPrice> TourPrices { get; set; }
        public DbSet<TourPriceItem> TourPriceItems { get; set; }
        public DbSet<TourProgram> TourPrograms { get; set; }
        public DbSet<TourProgramLanguageItem> TourProgramLanguageItems { get; set; }
        public DbSet<Many_Tour_Exclusion> Many_Tour_Exclusions { get; set; }
        public DbSet<Many_Tour_Inclusion> Many_Tour_Inclusions { get; set; }
        public DbSet<Many_Tour_SightToSee> Many_Tour_SightToSees { get; set; }
        public DbSet<Many_Product_RecomendedBlog> Many_Product_RecomendedBlogs { get; set; }
        public DbSet<Many_Discount_TourCategory> Many_Discount_TourCategory { get; set; }
        public DbSet<Many_Discount_Product> Many_Discount_Products { get; set; }
        public DbSet<Many_Blog_BlogCategory> Many_Blog_BlogCategories { get; set; }
        public DbSet<TripadvisorComment> TripadvisorComments { get; set; }
        public DbSet<Many_TripadvisorComment_TourGuide> Many_TripadvisorComment_TourGuides { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoleTemplate> RoleTemplates { get; set; }
        public DbSet<Many_Role_RoleTemplate> Many_Role_RoleTemplates { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<UserExtensionMail> UserExtensionsMails { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<GuideCity> GuideCities { get; set; }
        public DbSet<GuideLanguage> GuideLanguages { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<AgentUserRequest> AgentUserRequests { get; set; }
        public DbSet<ReservationEditRequest> ReservationEditRequests { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationGuide> OperationGuides { get; set; }
        public DbSet<OperationVehicle> OperationVehicles { get; set; }
        public DbSet<OperationNote> OperationNotes { get; set; }
        public DbSet<OperationShop> OperationShops { get; set; }
        public DbSet<OperationTicket> OperationTickets { get; set; }
        public DbSet<OperationAdditionalService> OperationAdditionalServices { get; set; }
        public DbSet<Route> Routes { get; set; }

        //public DbSet<Subscriber> Subscribers { get; set; }
        #endregion
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //Configuration dosyalarını şuan çalıştığımız assembly içerisinden getir diyoruz 
        //    //şuan çalıştığımız proje = data katmanı - klasörü oluyor.
        //    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        //    base.OnModelCreating(modelBuilder);
        //}
        public override int SaveChanges()
        {
            //var hasSlugEntry = entries.Where(entry => entry.GetType().IsAssignableFrom(typeof(IHasSlug))).FirstOrDefault();
            //if (hasSlugEntry is not null)
            //{
            //    TrackForSlugChanges(hasSlugEntry);
            //}

            var entries = ChangeTracker.Entries();
            foreach (var item in entries)
            {
                if (item.Entity is BaseEntity entity)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            entity.Id = Guid.NewGuid();
                            //entity.Status = true; //base entity default değeri true olarak verildi. eklerken false olarak eklemek istediğimiz entityler olabilir
                            entity.CreateDate = DateTime.Now;
                            entity.IsDeleted = false;
                            entity.UpdateDate = null;
                            break;
                        default:
                            break;
                        case EntityState.Modified:
                            entity.UpdateDate = DateTime.Now;
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
        //void TrackForSlugChanges(EntityEntry entry)
        //{
        //    MemberEntry memberEntry = entry.Member("Slug");
        //    string currentSlugValue = memberEntry.CurrentValue as string;
        //    PropertyInfo originalValuePropertyRunTimeProperty = memberEntry.GetType().GetRuntimeProperty("OriginalValue");

        //    if (originalValuePropertyRunTimeProperty is not null)
        //    {
        //        var originalValue = originalValuePropertyRunTimeProperty.GetValue(memberEntry);
        //        if (originalValue.ToString() != currentSlugValue)
        //        {
        //            Type entityType = entry.Entity.GetType();
        //            if (entityType.IsAssignableFrom(typeof(IHasSlug)))
        //            {
        //                BlogLanguageItem blogLanguageItem = entry.Entity as BlogLanguageItem;

        //                Route existingRoute = Routes.Where(x => x.EntityId == blogLanguageItem.BlogID).FirstOrDefault();

        //                if (existingRoute is null)//dbde bu languageItem için bir route yok, yeni ekliyorum
        //                {
        //                    var blogLanguageItemRouteTemplate = RouteTemplateConstants.GetRouteTemplate(No.Blog);
        //                    string slugUrl = $"{LanguageList.GetPrefix(blogLanguageItem.LanguageID)}/{currentSlugValue}";

        //                    Route route = new(blogLanguageItemRouteTemplate.Controller, blogLanguageItemRouteTemplate.Action, blogLanguageItem.BlogID, blogLanguageItem.LanguageID, slugUrl);
        //                }
        //                else //db de bu languageItem için bir route var, route u güncelliyorum.
        //                {

        //                }
        //            }
        //        }
        //    }
        //}

        //static readonly List<Type> willBeTrackingForSlug = new() {
        //         typeof(BlogLanguageItem),
        //         typeof(ProductLanguageItem),
        //         typeof(TourCategoryLanguageItem),
        //         typeof(ServiceLanguageItem)
        //      };
    }
}

/* Seeding vehicleType
 * ctorda çalıştırdım
       //if (!VehicleTypes.Any())
       //{
       //    VehicleTypes.AddRange(new List<VehicleType>()
       //    {
       //        new()
       //        {
       //            Type = "Yacht",
       //        },
       //        new()
       //        {
       //            Type = "Bus",
       //        },
       //        new()
       //        {
       //            Type = "SUV",
       //        },
       //        new()
       //        {
       //            Type = "Van",
       //        },
       //    });
       //    SaveChanges();
       //}
       */
