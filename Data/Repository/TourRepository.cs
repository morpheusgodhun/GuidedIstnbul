using Core.Entities;
using Core.IRepository;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.ProductDtos.TourDtos;
using Dto.ApiWebDtos.ApiToWebDtos.AdditionalService;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.GeneralDtos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Many_Product_RecomendedBlog = Core.Entities.Many_Product_RecomendedBlog;

namespace Data.Repository
{
    public class TourRepository : GenericRepository<Tour>, ITourRepository
    {
        Context _context;
        DbSet<Product> _products;
        DbSet<ProductLanguageItem> _productLanguageItem;
        DbSet<Tour> _tours;
        DbSet<TourLanguageItem> _tourLanguageItems;

        DbSet<ConstantValue> _constantValues;
        DbSet<ConstantValueLanguageItem> _constantValueLanguageItems;
        DbSet<Page> _pages;

        DbSet<Many_Tour_TourCategory> _many_Tour_TourCategories;
        DbSet<TourCategory> _tourCategories;
        DbSet<TourCategoryLanguageItem> _tourCategoryLanguageItems;

        DbSet<Many_Product_Tag> _many_Product_Tags;
        DbSet<Tag> _tags;
        DbSet<TagLanguageItem> _tagLanguageItems;


        DbSet<Many_Tour_Destination> _many_Tour_Destinations;
        DbSet<DestinationLanguageItem> _destinationLanguageItems;

        DbSet<Many_Tour_Inclusion> _many_Tour_Inclusions;
        DbSet<Many_Tour_Exclusion> _many_Tour_Exclusions;
        DbSet<Many_Tour_SightToSee> _many_Tour_SightToSees;

        DbSet<SystemOption> _systemOptions;
        DbSet<SystemOptionLanguageItem> _systemOptionLanguageItems;

        DbSet<CancellationPolicyLanguageItem> _cancellationPolicyLanguageItems;

        DbSet<ProductFaq> _productFaqs;
        DbSet<ProductFaqLanguageItem> _productFaqLanguageItems;

        DbSet<TourProgram> _tourPrograms;
        DbSet<TourProgramLanguageItem> _tourProgramLanguageItems;

        DbSet<ProductImage> _productImages;

        DbSet<Many_Product_RecomendedBlog> _many_Product_RecomendedBlogs;
        DbSet<Blog> _blogs;
        DbSet<BlogLanguageItem> _blogLanguageItems;


        DbSet<Many_Product_AdditionalService> _many_Product_AdditionalServiceItems;
        DbSet<Many_Product_AdditionalServiceOption> _many_Product_AdditionalServiceOptions;
        DbSet<Many_AdditionalServiceOption_AdditionalServiceInput> _many_Option_Input;
        DbSet<AdditionalService> _additionalServices;
        DbSet<AdditionalServiceLanguageItem> _additionalServiceLanguageItems;
        DbSet<AdditionalServiceOption> _additionalServiceOptions;
        DbSet<AdditionalServiceOptionLanguageItem> _additionalServiceOptionLanguageItems;
        DbSet<AdditionalServiceInput> _additionalServiceInputs;
        DbSet<AdditionalServiceInputLanguageItem> _additionalServiceInputLanguageItems;

        //private readonly ITourPriceService _tourPriceService; //, ITourPriceService tourPriceService

        public TourRepository(Context context) : base(context) //, ITourPriceService tourPriceService
        {
            _context = context;
            _products = _context.Products;
            _productLanguageItem = _context.ProductLanguageItems;
            _tours = _context.Tours;
            _tourLanguageItems = _context.TourLanguageItems;

            _constantValues = _context.ConstantValues;
            _constantValueLanguageItems = _context.ConstantValueLanguageItems;
            _pages = _context.Pages;

            _many_Tour_TourCategories = _context.Many_Tour_TourCategories;
            _tourCategories = _context.TourCategories;
            _tourCategoryLanguageItems = _context.TourCategoryLanguageItems;

            _many_Product_Tags = _context.Many_Product_Tags;
            _tags = _context.Tags;
            _tagLanguageItems = _context.TagLanguageItems;

            _many_Tour_Destinations = _context.Many_Tour_Destinations;
            _destinationLanguageItems = _context.DestinationLanguageItems;

            _many_Tour_Inclusions = _context.Many_Tour_Inclusions;
            _many_Tour_Exclusions = _context.Many_Tour_Exclusions;
            _many_Tour_SightToSees = _context.Many_Tour_SightToSees;
            _systemOptions = _context.SystemOptions;
            _systemOptionLanguageItems = _context.SystemOptionLanguageItems;

            _cancellationPolicyLanguageItems = _context.CancellationPolicyLanguageItems;

            _productFaqs = _context.ProductFaqs;
            _productFaqLanguageItems = _context.ProductFaqLanguageItems;

            _tourPrograms = _context.TourPrograms;
            _tourProgramLanguageItems = _context.TourProgramLanguageItems;
            _productImages = _context.ProductImages;

            _many_Product_RecomendedBlogs = _context.Many_Product_RecomendedBlogs;
            _blogs = _context.Blogs;
            _blogLanguageItems = _context.BlogLanguageItems;

            _many_Product_AdditionalServiceItems = _context.Many_Product_AdditionalServices;
            _many_Product_AdditionalServiceOptions = _context.Many_Product_AdditionalServiceOptions;
            _many_Option_Input = _context.Many_AdditionalServiceOption_AdditionalServiceInputs;
            _additionalServices = _context.AdditionalServices;
            _additionalServiceLanguageItems = _context.AdditionalServiceLanguageItems;
            _additionalServiceOptions = _context.AdditionalServiceOptions;
            _additionalServiceOptionLanguageItems = _context.AdditionalServiceOptionLanguageItems;
            _additionalServiceInputs = _context.AdditionalServiceInputs;
            _additionalServiceInputLanguageItems = _context.AdditionalServiceInputLanguageItems;
            //_tourPriceService = tourPriceService;
        }

        public async Task<List<TourListDto>> BestDealTourListAsync(int languageId)
        {
            var bestDealTourTypeId = TourTypeList.TourTypes.FirstOrDefault(x => x.Option == "Best Deal")?.OptionID;

            var productList = await _products
                .Where(product => !product.IsDeleted && product.Status && product.IsTour && product.ShowOnHomePage)
                .ToListAsync();

            var productLanguageList = await _productLanguageItem
                .Where(productLanguage => productLanguage.LanguageID == languageId)
                .ToListAsync();

            var tourList = (from product in productList
                            join productLanguage in productLanguageList
                                on product.Id equals productLanguage.ProductID
                            join tour in _tours
                                on product.Id equals tour.ProductID
                            where tour.TourTypeID == bestDealTourTypeId && tour.CustomTourRequestId == null
                            orderby product.Order
                            select new TourListDto
                            {
                                ProductID = product.Id,
                                AskForPrice = tour.AskForPrice,
                                CardImagePath = product.CardImagePath,
                                Name = productLanguage.DisplayName,
                                Price = 0, // fiaytı serviste çekip birleştiricem  //_tourPriceService.TourMinPriceForList(tour.Id).Price,
                                Rate = 5,
                                Slug = productLanguage.Slug,
                                TourType = (from typeLanguage in _systemOptionLanguageItems
                                            where typeLanguage.SystemOptionId == tour.TourTypeID && typeLanguage.LanguageID == languageId
                                            select new TourTypeDto
                                            {
                                                TypeName = typeLanguage.Name
                                            }).FirstOrDefault()!,
                                TourId = tour.Id,
                                Order = product.Order
                            }).ToList();

            return tourList;


        }

        public async Task<WebTourDetailDto> GetTourDetail(Guid productId, int languageId)
        {
            var constantValues = await _constantValues.Where(x => x.Status && !x.IsDeleted).ToListAsync();
            var products = await _products.Where(x => x.Id == productId && !x.IsDeleted && x.Status).ToListAsync();
            var productLanguageItems = await _productLanguageItem.Where(x => x.Status && !x.IsDeleted).ToListAsync();


            WebTourDetailDto value = (from product in products
                                      join productLanguage in productLanguageItems
                                      on product.Id equals productLanguage.ProductID
                                      where productLanguage.LanguageID == languageId
                                      join tour in _tours.Where(x => x.ProductID == productId).ToList()
                                      on product.Id equals tour.ProductID
                                      join tourLanguage in _tourLanguageItems.ToList()
                                      on tour.Id equals tourLanguage.TourID
                                      where tourLanguage.LanguageID == languageId
                                      select new WebTourDetailDto()
                                      {
                                          ConstantValues = (from item in _constantValues.ToList()
                                                            join page in _pages.Where(x => x.PageName == "Tour Detail").ToList()
                                                            on item.PageID equals page.Id
                                                            join languageItem in _constantValueLanguageItems.Where(x => x.LanguageID == languageId)
                                                            on item.Id equals languageItem.ConstantValueID
                                                            select new ConstantValueDto()
                                                            {
                                                                Key = item.Key,
                                                                Value = languageItem.Value
                                                            }).ToList(),
                                          ProductID = product.Id,
                                          TourId = tour.Id,
                                          BannerImagePath = product.BannerImagePath,
                                          TourTitle = productLanguage.DisplayName,
                                          AskForPrice = tour.AskForPrice,
                                          Categories = (from many in _many_Tour_TourCategories.Where(x => x.TourID == tour.Id).ToList()
                                                        where many.TourID == tour.Id
                                                        join category in _tourCategories.ToList()
                                                        on many.TourCategoryID equals category.Id
                                                        join categoryLanguage in _tourCategoryLanguageItems.ToList()
                                                        on category.Id equals categoryLanguage.TourCategoryID
                                                        where categoryLanguage.LanguageID == languageId
                                                        select new TourCategoryDto()
                                                        {
                                                            ID = category.Id,
                                                            IconPath = category.IconPath,
                                                            Name = categoryLanguage.CategoryName
                                                        }).ToList(),
                                          Tags = (from many in _many_Product_Tags.Where(x => x.ProductID == product.Id).ToList()
                                                  where many.ProductID == product.Id
                                                  join tag in _tags.ToList()
                                                  on many.TagID equals tag.Id
                                                  join tagLanguageItem in _tagLanguageItems.ToList()
                                                  on tag.Id equals tagLanguageItem.TagID
                                                  where tagLanguageItem.LangaugeID == languageId
                                                  select new TourTagDto()
                                                  {
                                                      ID = tag.Id,
                                                      IconPath = tag.IconPath,
                                                      Name = tagLanguageItem.DisplayName
                                                  }).ToList(),
                                          TourPrice = 0,//_tourPriceService.TourPriceForDays(tour.Id,tour.Duration),
                                          IsPerPerson = tour.IsPerPerson,
                                          IsPerDay = tour.IsPerDay,
                                          Duration = tourLanguage.DurationText,
                                          DurationDay = tour.Duration,
                                          PersonLimit = product.PersonLimit,
                                          IsPersonLimited = product.IsPersonLimited,
                                          Content = tourLanguage.Content,
                                          Destination = (from many in _many_Tour_Destinations.Where(x => x.TourID == tour.Id).ToList()
                                                         where many.TourID == tour.Id
                                                         join destinationLanguage in _destinationLanguageItems.ToList()
                                                         on many.DestinationID equals destinationLanguage.DestinationID
                                                         where destinationLanguage.LangaugeID == languageId
                                                         select destinationLanguage.DisplayName).ToList(),
                                          StartPoint = tourLanguage.TourStartPoint,
                                          EndPoint = tourLanguage.TourEndPoint,
                                          Includes = (from many in _many_Tour_Inclusions.Where(x => x.TourID == tour.Id).ToList()
                                                      where many.TourID == tour.Id
                                                      join systemOptionLanguage in _systemOptionLanguageItems.ToList()
                                                      on many.SystemOptionID equals systemOptionLanguage.SystemOptionId
                                                      where systemOptionLanguage.LanguageID == languageId
                                                      select systemOptionLanguage.Name).ToList(),
                                          Excludes = (from many in _many_Tour_Exclusions.Where(x => x.TourID == tour.Id).ToList()
                                                      where many.TourID == tour.Id
                                                      join systemOptionLanguage in _systemOptionLanguageItems.ToList()
                                                      on many.SystemOptionID equals systemOptionLanguage.SystemOptionId
                                                      where systemOptionLanguage.LanguageID == languageId
                                                      select systemOptionLanguage.Name).ToList(),
                                          SightsToSee = (from many in _many_Tour_SightToSees.Where(x => x.TourID == tour.Id).ToList()
                                                         where many.TourID == tour.Id
                                                         join systemOptionLanguage in _systemOptionLanguageItems.ToList()
                                                         on many.SystemOptionID equals systemOptionLanguage.SystemOptionId
                                                         where systemOptionLanguage.LanguageID == languageId
                                                         select systemOptionLanguage.Name).ToList(),
                                          CancellationPolicy = _cancellationPolicyLanguageItems.Where(x => x.CancellationPolicyID == product.CancellationPolicyID && x.LangaugeID == languageId).FirstOrDefault().Content,
                                          Faqs = ((from faq in _productFaqs.Where(x => x.ProductID == product.Id).ToList()
                                                   where faq.ProductID == product.Id
                                                   join faqLanguage in _productFaqLanguageItems.ToList()
                                                   on faq.Id equals faqLanguage.ProductFaqID
                                                   where faqLanguage.LanguageID == languageId
                                                   select new WebTourDetailFaqDto()
                                                   {
                                                       Order = faq.Order,
                                                       Question = faqLanguage.Question,
                                                       Answer = faqLanguage.Answer
                                                   }).Union(from f in _context.Faqs
                                                            join fli in _context.FaqLangaugeItems on f.Id equals fli.FaqID into fliGroup
                                                            from fli in fliGroup.DefaultIfEmpty()
                                                            where
                                                            fli.LangaugeID == languageId &&
                                                            f.FaqCategoryID == _context.FaqCategories.Where(fc => fc.PageId == new Guid("F30DFBC7-82FF-46CA-879B-26C24B0E6FD1")).Select(fc => fc.Id).FirstOrDefault() //TODO: Sabit value
                                                            select new WebTourDetailFaqDto
                                                            {
                                                                Order = f.Order,
                                                                Question = fli.Question,
                                                                Answer = fli.Answer
                                                            })).OrderBy(x => x.Order).ToList(),
                                          Programs = (from program in _tourPrograms.Where(x => x.TourID == tour.Id).ToList()
                                                      where program.TourID == tour.Id
                                                      join programLanguage in _tourProgramLanguageItems.ToList()
                                                      on program.Id equals programLanguage.TourProgramID
                                                      where programLanguage.LanguageID == languageId
                                                      select new WebTourDetailProgramDto()
                                                      {
                                                          Day = program.Day,
                                                          Title = programLanguage.Title,
                                                          Content = programLanguage.Content
                                                      }).ToList(),
                                          Images = (from image in _productImages.Where(x => x.ProductID == product.Id).ToList()
                                                    where image.ProductID == product.Id
                                                    select new WebTourDetailImageDto()
                                                    {
                                                        Order = image.Order,
                                                        ImagePath = image.ImagePath
                                                    }).ToList(),
                                          TripadvisorComments = new List<WebTourDetailTripadvisorCommenDto>(),
                                          RelatedBlogs = (from many in _many_Product_RecomendedBlogs.Where(x => x.ProductID == product.Id).ToList()
                                                          where many.ProductID == product.Id
                                                          join blog in _blogs.ToList()
                                                          on many.BlogID equals blog.Id
                                                          join blogLanguage in _blogLanguageItems.ToList()
                                                          on blog.Id equals blogLanguage.BlogID
                                                          where blogLanguage.LanguageID == languageId
                                                          select new WebTourDetailBlogDto()
                                                          {
                                                              BlogID = blog.Id,
                                                              CardImagePath = blog.CardImagePath,
                                                              Title = blogLanguage.BlogTitle,
                                                              ShortDescription = blogLanguage.ShortDescription
                                                          }).ToList(),
                                          //AdditionalServices = (from manyService in _many_Product_AdditionalServiceItems.ToList()
                                          //                      where manyService.ProductID == product.Id
                                          //                      && manyService.Status && !manyService.IsDeleted
                                          //                      join service in _additionalServices.ToList()
                                          //                      on manyService.AdditionalServiceID equals service.Id
                                          //                      join serviceLanguage in _additionalServiceLanguageItems.ToList()
                                          //                      on service.Id equals serviceLanguage.AdditionalServiceID
                                          //                      where serviceLanguage.LanguageID == languageId
                                          //                      && service.Status && !service.IsDeleted
                                          //                      orderby manyService.Order
                                          //                      select new AdditionalServiceDto()
                                          //                      {
                                          //                          AdditionalServiceID = service.Id,
                                          //                          AdditionalServiceName = serviceLanguage.DisplayName,
                                          //                          IsPerPerson = service.IsPerPerson,
                                          //                          IsPerDay = service.IsPerDay, // isperday de yoktu ben ekliyorum neden yok acaba
                                          //                          Isrequired = manyService.IsRequired,
                                          //                          IsSpecialNumber = service.IsSpecialNumber,
                                          //                          Options = (from manyOption in _many_Product_AdditionalServiceOptions.ToList()
                                          //                                     where manyOption.Many_Product_AdditionalServiceID == manyService.Id
                                          //                                     join option in _additionalServiceOptions.ToList()
                                          //                                     on manyOption.AdditionalServiceOptionID equals option.Id
                                          //                                     join optionLanguage in _additionalServiceOptionLanguageItems.ToList()
                                          //                                     on option.Id equals optionLanguage.AdditionalServiceOptionID
                                          //                                     where optionLanguage.LanguageID == languageId
                                          //                                     && option.Status && !option.IsDeleted
                                          //                                     orderby option.Order
                                          //                                     select new AdditionalServiceOptionDto()
                                          //                                     {
                                          //                                         OptionID = option.Id,
                                          //                                         OptionName = optionLanguage.DisplayName,
                                          //                                         OptionPrice = 0, //TODO: burada kaldık addtionallerın fiyatlarınıda json gibi yapmak lazım
                                          //                                         Order = option.Order,
                                          //                                         Inputs = (from many in _many_Option_Input.ToList()
                                          //                                                   where many.AdditionalServiceOptionID == option.Id
                                          //                                                   join input in _additionalServiceInputs.ToList()
                                          //                                                   on many.AdditionalServiceInputID equals input.Id
                                          //                                                   join inputLanguage in _additionalServiceInputLanguageItems.ToList()
                                          //                                                   on input.Id equals inputLanguage.AdditionalServiceInputID
                                          //                                                   where inputLanguage.LanguageID == languageId
                                          //                                                   && input.Status && !input.IsDeleted
                                          //                                                   select new AdditionalServiceOptionInputDto()
                                          //                                                   {
                                          //                                                       InputID = input.Id,
                                          //                                                       InputName = inputLanguage.DisplayName,
                                          //                                                       Order = input.Order,
                                          //                                                       InputType = input.InputTypeID
                                          //                                                   }).ToList(),
                                          //                                     }).ToList(),

                                          //                      }).ToList(),

                                          StartTimeSelectList = (from timeID in JsonSerializer.Deserialize<List<int>>(tour.StartTimeIDs)
                                                                 select new SelectListOption()
                                                                 {
                                                                     ID = timeID,
                                                                     Value = StartTimeList.StartTimes.FirstOrDefault(x => x.ID == timeID).Value
                                                                 }).ToList(),
                                          SuggestedStartTimeID = tour.SuggestedStartTimeID ?? default(int),
                                          HowFindUsSelectList = (from systemOption in _systemOptions.Where(x => x.SystemOptionCategoryID == 5).ToList()
                                                                 where !systemOption.IsDeleted && systemOption.Status
                                                                 //where systemOption.SystemOptionCategoryID == 5
                                                                 join languageItem in _systemOptionLanguageItems.ToList()
                                                                 on systemOption.Id equals languageItem.SystemOptionId
                                                                 where languageItem.LanguageID == languageId
                                                                 orderby systemOption.Order
                                                                 select new SelectListOptionDto()
                                                                 {
                                                                     OptionID = systemOption.Id,
                                                                     Option = languageItem.Name
                                                                 }).ToList(),
                                          AlsoInterestedSelectList = (from systemOption in _systemOptions.Where(x => x.SystemOptionCategoryID == 6).ToList()
                                                                      where !systemOption.IsDeleted && systemOption.Status
                                                                      //where systemOption.SystemOptionCategoryID == 6
                                                                      join languageItem in _systemOptionLanguageItems.ToList()
                                                                      on systemOption.Id equals languageItem.SystemOptionId
                                                                      where languageItem.LanguageID == languageId
                                                                      select new SelectListOptionDto()
                                                                      {
                                                                          OptionID = systemOption.Id,
                                                                          Option = languageItem.Name
                                                                      }).ToList(),
                                          DurationSelectList = (from dcount in JsonSerializer.Deserialize<List<int>>(string.IsNullOrWhiteSpace(tour.SelectableDurations) ? "[" + tour.Duration.ToString() + "]" : tour.SelectableDurations)
                                                                select new SelectListOption()
                                                                {
                                                                    ID = dcount,
                                                                    Value = dcount.ToString()
                                                                }).ToList(),
                                          CutoffHour = product.CutoffHour,
                                          Segment = (from systemOptionLanguage in _systemOptionLanguageItems.Where(x => x.SystemOptionId == tour.SegmentID && x.LanguageID == languageId).ToList()
                                                     select systemOptionLanguage.Name).FirstOrDefault(),
                                          Type = (from systemOptionLanguage in _systemOptionLanguageItems.Where(x => x.SystemOptionId == tour.TourTypeID && x.LanguageID == languageId).ToList()
                                                  select systemOptionLanguage.Name).FirstOrDefault()
                                      }).First();

            return value;
        }

        public async Task<List<TourListDto>> PrivateTourListAsync(int languageId)
        {
            var productList = await _products
                .Where(product => !product.IsDeleted && product.Status && product.IsTour && product.ShowOnHomePage)
                .ToListAsync();

            var productLanguageList = await _productLanguageItem
                .Where(productLanguage => productLanguage.LanguageID == languageId)
                .ToListAsync();

            var tourList = await _tours
                .Where(tour => tour.TourTypeID == new Guid("ad813663-6348-45f9-a2bb-00e11a9e2483"))
                .ToListAsync();

            var tourTypeList = await _systemOptionLanguageItems
               .Where(typeLanguage => typeLanguage.LanguageID == languageId)
               .ToListAsync();



            List<TourListDto> tourlist = (from product in productList
                                          join productLanguage in productLanguageList
                                          on product.Id equals productLanguage.ProductID
                                          join tour in tourList
                                        on product.Id equals tour.ProductID
                                          where tour.TourTypeID == new Guid("ad813663-6348-45f9-a2bb-00e11a9e2483") //TODO: Sabit Guid
                                          orderby product.Order
                                          select new TourListDto()
                                          {
                                              ProductID = product.Id,
                                              CardImagePath = product.CardImagePath,
                                              Name = productLanguage.DisplayName,
                                              AskForPrice = tour.AskForPrice,
                                              Price = 0,
                                              Rate = 5,
                                              TourType = tourTypeList.Where(typeLanguage => typeLanguage.SystemOptionId == tour.TourTypeID)
                                                                     .Select(typeLanguage => new TourTypeDto { TypeName = typeLanguage.Name })
                                                                     .FirstOrDefault()!,
                                              TourId = tour.Id,
                                              Order = product.Order,
                                              Slug = productLanguage.Slug
                                          }).ToList();
            return tourlist;
        }

        public GetTourFilterDto TourFilters(int languageId)
        {

            GetTourFilterDto value = new GetTourFilterDto()
            {
                ConstantValues = (from item in _constantValues.ToList()
                                  join page in _pages.Where(x => x.PageName == "Tour Filter").ToList()
                                  on item.PageID equals page.Id
                                  join languageItem in _constantValueLanguageItems.Where(x => x.LanguageID == languageId)
                                  on item.Id equals languageItem.ConstantValueID
                                  select new ConstantValueDto()
                                  {
                                      Key = item.Key,
                                      Value = languageItem.Value
                                  }).ToList(),
                Categories = (from category in _tourCategories.ToList()
                              where !category.IsDeleted && category.Status
                              join categoryLanguageItem in _tourCategoryLanguageItems.ToList()
                              on category.Id equals categoryLanguageItem.TourCategoryID
                              where categoryLanguageItem.LanguageID == languageId
                              select new SelectListOptionDto()
                              {
                                  OptionID = category.Id,
                                  Option = categoryLanguageItem.CategoryName,
                              }).ToList(),
                Destinations = (from destination in _context.Destinations.ToList()
                                where !destination.IsDeleted && destination.Status
                                join destinationLanguageItem in _destinationLanguageItems.ToList()
                                on destination.Id equals destinationLanguageItem.DestinationID
                                where destinationLanguageItem.LangaugeID == languageId
                                orderby destination.Order
                                select new SelectListOptionDto()
                                {
                                    OptionID = destination.Id,
                                    Option = destinationLanguageItem.DisplayName,
                                }).ToList(),
                Types = (from segment in _systemOptions.ToList()
                         where !segment.IsDeleted && segment.Status && segment.SystemOptionCategoryID == 1
                         join segmentLanguageItem in _systemOptionLanguageItems.ToList()
                         on segment.Id equals segmentLanguageItem.SystemOptionId
                         where segmentLanguageItem.LanguageID == languageId
                         select new SelectListOptionDto()
                         {
                             OptionID = segment.Id,
                             Option = segmentLanguageItem.Name
                         }).ToList(),
                Specials = (from segment in _systemOptions.ToList()
                            where !segment.IsDeleted && segment.Status && segment.SystemOptionCategoryID == 3
                            join segmentLanguageItem in _systemOptionLanguageItems.ToList()
                            on segment.Id equals segmentLanguageItem.SystemOptionId
                            where segmentLanguageItem.LanguageID == languageId
                            select new SelectListOptionDto()
                            {
                                OptionID = segment.Id,
                                Option = segmentLanguageItem.Name
                            }).ToList(),
                Page = (from page in _pages.ToList()
                        where page.PageName == "Tour Filter"
                        join pageLanguageItem in _context.PageLanguageItems.ToList()
                        on page.Id equals pageLanguageItem.PageID
                        where pageLanguageItem.LanguageId == languageId
                        select new PageDto()
                        {
                            BannerImagePath = page.ImagePath,
                            Title = pageLanguageItem.Title,
                            Subtitle = pageLanguageItem.Subtitle
                        }).FirstOrDefault(),
                Months = (from systemOption in _context.SystemOptions.ToList()
                          where systemOption.SystemOptionCategoryID == 7 && !systemOption.IsDeleted && systemOption.Status
                          join languageItem in _context.SystemOptionLanguageItems.ToList()
                              on systemOption.Id equals languageItem.SystemOptionId
                          where languageItem.LanguageID == languageId
                          orderby systemOption.Order
                          select new SelectListOptionDto()
                          {
                              OptionID = systemOption.Id,
                              Option = languageItem.Name
                          }).ToList(),
                /*Tours = (from product in _products.ToList()
                         where !product.IsDeleted && product.Status
                         join productLanguageItem in _productLanguageItem.ToList()
                         on product.Id equals productLanguageItem.ProductID
                         where productLanguageItem.LanguageID == languageId
                         join tour in _tours.ToList()
                         on product.Id equals tour.ProductID
                         join tourLanguageItem in _tourLanguageItems.ToList()
                         on tour.Id equals tourLanguageItem.TourID
                         where tourLanguageItem.LanguageID == languageId
                         select new FilterTourDto()
                         {
                             ProductId = product.Id,
                             Price = 500,
                             Rate = 5,
                             TourName = productLanguageItem.DisplayName,
                             IsPerPerson = tour.IsPerPerson,
                             CategoryNames = (from many in _many_Tour_TourCategories.ToList()
                                              where many.TourID == tour.Id
                                              join categoryLanguageItem in _tourCategoryLanguageItems.ToList()
                                              on many.TourCategoryID equals categoryLanguageItem.TourCategoryID
                                              where categoryLanguageItem.LanguageID == languageId
                                              select categoryLanguageItem.CategoryName).ToList(),
                             TagNames = (from many in _many_Product_Tags.ToList()
                                         where many.ProductID == product.Id
                                         join tagLanguageItem in _tagLanguageItems.ToList()
                                         on many.TagID equals tagLanguageItem.TagID
                                         where tagLanguageItem.LangaugeID == languageId
                                         select tagLanguageItem.DisplayName).ToList(),
                             CategoryIDs = (from many in _many_Tour_TourCategories.ToList()
                                            where many.TourID == tour.Id
                                            select many.TourCategoryID).ToList(),
                             DurationIDs = (from many in _many_Tour_Destinations.ToList()
                                            where many.TourID == tour.Id
                                            select many.DestinationID).ToList(),
                             InclusionIDs = (from many in _many_Tour_Inclusions.ToList()
                                             where many.TourID == tour.Id
                                             select many.SystemOptionID).ToList(),
                             TourTypeId = new Guid(tour.TourTypeID.ToString()),
                             CardImagePath = product.CardImagePath
                             
                         }).ToList(),*/
            };

            return value;
        }

        public async Task<List<TourListDto>> TurkeyTourListAsync(int languageId)
        {
            try
            {
                var productList = await _products
            .Where(product => !product.IsDeleted && product.Status && product.IsTour && product.ShowOnHomePage)
            .ToListAsync();

                var productLanguageList = await _productLanguageItem
                    .Where(productLanguage => productLanguage.LanguageID == languageId)
                    .ToListAsync();

                var tourList = await _tours
                    .Where(tour => tour.CustomTourRequest == null)
                    .ToListAsync();

                var categoryTourList = await _many_Tour_TourCategories
                    .Where(many => tourList.Select(x => x.Id).Contains(many.TourID))
                    .Join(_tourCategories,
                        many => many.TourCategoryID,
                        category => category.Id,
                        (many, category) => new { many, category })
                    .Where(joinResult => joinResult.category.Id == new Guid("e95be291-0f19-4492-b402-d4b0c27e82e7"))
                    .Select(joinResult => joinResult.many)
                    .ToListAsync();

                var tourTypeList = await _systemOptionLanguageItems
                    .Where(typeLanguage => tourList.Select(x => x.TourTypeID).Contains(typeLanguage.SystemOptionId) && typeLanguage.LanguageID == languageId)
                    .ToListAsync();

                var tourlist = (
                    from product in productList
                    join productLanguage in productLanguageList on product.Id equals productLanguage.ProductID
                    join tour in tourList on product.Id equals tour.ProductID
                    join many in categoryTourList on tour.Id equals many.TourID
                    orderby product.Order
                    select new TourListDto
                    {
                        ProductID = product.Id,
                        CardImagePath = product.CardImagePath,
                        Name = productLanguage.DisplayName,
                        Price = 0,
                        Rate = 5,
                        AskForPrice = tour.AskForPrice,
                        TourType = tourTypeList
                            .Where(typeLanguage => typeLanguage.SystemOptionId == tour.TourTypeID)
                            .Select(typeLanguage => new TourTypeDto { TypeName = typeLanguage.Name })
                            .FirstOrDefault()!,
                        TourId = tour.Id,
                        Order = product.Order,
                        Slug = productLanguage.Slug
                    }
                ).ToList();

                return tourlist;
            }
            catch (Exception ex)
            {
                Console.WriteLine("------------");
                Console.WriteLine(ex);
                Console.WriteLine(ex.Message);
                throw;
            }

            /*
            List<TourListDto> tourlist = (from product in _products.ToList()
                                          where !product.IsDeleted && product.Status && product.IsTour && product.ShowOnHomePage

                                          join productLanguage in _productLanguageItem.ToList()
                                          on product.Id equals productLanguage.ProductID
                                          where productLanguage.LanguageID == languageId

                                          join tour in _tours.ToList()
                                          on product.Id equals tour.ProductID

                                          join many in _many_Tour_TourCategories.ToList()
                                          on tour.Id equals many.TourID

                                          join category in _tourCategories.ToList()
                                          on many.TourCategoryID equals category.Id

                                          where category.Id == new Guid("e95be291-0f19-4492-b402-d4b0c27e82e7") && tour.CustomTourRequest == null
                                          select new TourListDto()
                                          {
                                              ProductID = product.Id,
                                              CardImagePath = product.CardImagePath,
                                              Name = productLanguage.DisplayName,
                                              Price = 0,
                                              Rate = 5,
                                              TourType = (from typeLanguage in _systemOptionLanguageItems.ToList()
                                                          where typeLanguage.SystemOptionId == tour.TourTypeID && typeLanguage.LanguageID == languageId
                                                          select new TourTypeDto()
                                                          {
                                                              TypeName = typeLanguage.Name
                                                          }).FirstOrDefault()!,
                                              TourId = tour.Id
                                          }).ToList();
            return tourlist;
            */
        }

        public GetTourFilterDto TourListFiltered(GetTourFilterFormDto filters, int languageId)
        {
            GetTourFilterDto value = new GetTourFilterDto()
            {
                ConstantValues = (from item in _constantValues.ToList()
                                  join page in _pages.Where(x => x.PageName == "Tour Filter").ToList()
                                  on item.PageID equals page.Id
                                  join languageItem in _constantValueLanguageItems.Where(x => x.LanguageID == languageId)
                                  on item.Id equals languageItem.ConstantValueID
                                  select new ConstantValueDto()
                                  {
                                      Key = item.Key,
                                      Value = languageItem.Value
                                  }).ToList(),
                Tours = (from product in _products.ToList()
                         where !product.IsDeleted && product.Status
                         join productLanguageItem in _productLanguageItem.ToList()
                         on product.Id equals productLanguageItem.ProductID
                         where productLanguageItem.LanguageID == languageId
                         join tour in _tours.ToList()
                         on product.Id equals tour.ProductID
                         join tourLanguageItem in _tourLanguageItems.ToList()
                         on tour.Id equals tourLanguageItem.TourID
                         where tourLanguageItem.LanguageID == languageId && tour.CustomTourRequestId == null
                         orderby product.Order
                         select new FilterTourDto()
                         {
                             ProductId = product.Id,
                             Price = 0,
                             Rate = 5,
                             TourName = productLanguageItem.DisplayName,
                             IsPerPerson = tour.IsPerPerson,
                             AskForPrice = tour.AskForPrice,
                             CategoryNames = (from many in _many_Tour_TourCategories.ToList()
                                              where many.TourID == tour.Id
                                              join categoryLanguageItem in _tourCategoryLanguageItems.ToList()
                                              on many.TourCategoryID equals categoryLanguageItem.TourCategoryID
                                              where categoryLanguageItem.LanguageID == languageId
                                              select categoryLanguageItem.CategoryName).ToList(),
                             TagNames = (from many in _many_Product_Tags.ToList()
                                         where many.ProductID == product.Id
                                         join tagLanguageItem in _tagLanguageItems.ToList()
                                         on many.TagID equals tagLanguageItem.TagID
                                         where tagLanguageItem.LangaugeID == languageId
                                         select tagLanguageItem.DisplayName).ToList(),
                             CategoryIDs = (from many in _many_Tour_TourCategories.ToList()
                                            where many.TourID == tour.Id
                                            select many.TourCategoryID).ToList(),
                             DurationIDs = (from many in _many_Tour_Destinations.ToList()
                                            where many.TourID == tour.Id
                                            select many.DestinationID).ToList(),
                             InclusionIDs = (from many in _many_Tour_Inclusions.ToList()
                                             where many.TourID == tour.Id
                                             select many.SystemOptionID).ToList(),
                             TourTypeId = tour.TourTypeID == Guid.Empty ? null : tour.TourTypeID,
                             CardImagePath = product.CardImagePath,
                             TourId = tour.Id,
                             Order = product.Order

                         }).ToList(),
            };

            return value;
        }

        public void EditTour(EditTourDto editTourDto)
        {
            Product product = _context.Products.Find(editTourDto.ProductID);
            Tour tour = _context.Tours.Where(x => x.ProductID == product.Id).FirstOrDefault();

            product.YoutubeLink = editTourDto.YoutubeLink;
            product.BannerImagePath = editTourDto.BannerImagePath;
            product.CardImagePath = editTourDto.CardImagePath;
            product.CancellationPolicyID = editTourDto.CancellationPolicyID;
            product.CutoffHour = editTourDto.CutOffHour + editTourDto.CutOffDay * 24;
            product.CustomerDeposito = editTourDto.CustomerDepositoAmount;
            product.AgentDeposito = editTourDto.AgentDepositoAmount;
            product.PaymentDate = editTourDto.DayOfPayment;
            product.MinimumPayoutPercent = editTourDto.MinimumPayoutPercent;
            product.IsChildPolicyActive = editTourDto.IsChildPolicyActive;
            product.Order = editTourDto.Order;
            product.PersonLimit = editTourDto.PersonLimit;
            product.IsPersonLimited = editTourDto.IsPersonLimited;

            _context.Products.Update(product);


            tour.TourTypeID = new Guid(editTourDto.TourTypeID.ToString());
            tour.SegmentID = new Guid(editTourDto.SegmentID.ToString());
            tour.StartCityID = editTourDto.StartingCityID;
            tour.StartTimeIDs = JsonSerializer.Serialize(editTourDto.StartTimeIDs);
            tour.SuggestedStartTimeID = editTourDto.SuggestedStartTimeID;

            tour.SelectableDurations = "[" + string.Join(",", editTourDto.SelectableDurations.Select(x => x.ToString())) + "]";
            if (tour.Duration != editTourDto.Duration)
            {

                if (tour.Duration < editTourDto.Duration)
                {
                    for (int i = tour.Duration + 1; i <= editTourDto.Duration; i++)
                    {
                        TourProgram tourProgram = new TourProgram()
                        {
                            Day = i,
                            TourID = tour.Id
                        };
                        _context.TourPrograms.Add(tourProgram);

                        foreach (var language in LanguageList.AllLanguages)
                        {
                            TourProgramLanguageItem tourProgramLanguageItem = new TourProgramLanguageItem()
                            {
                                TourProgramID = tourProgram.Id,
                                LanguageID = language.Id,
                                Title = "",
                                Content = ""
                            };

                            _context.TourProgramLanguageItems.Add(tourProgramLanguageItem);
                        }

                    }
                }
                else
                {
                    List<TourProgram> programs = _context.TourPrograms.Where(x => x.TourID == tour.Id).ToList();
                    for (int i = tour.Duration; i > editTourDto.Duration; i--)
                    {
                        TourProgram tourProgram = programs.FirstOrDefault(x => x.Day == i);
                        if (tourProgram is not null)
                        {
                            _context.TourPrograms.Remove(tourProgram);
                        }
                    }
                }
            }
            tour.Duration = editTourDto.SelectableDurations.Max();
            //tour.Duration = editTourDto.Duration;
            tour.IsPerPerson = editTourDto.IsPerPerson;
            tour.IsPerDay = editTourDto.IsPerDay;

            _context.Tours.Update(tour);


            var oldTags = _context.Many_Product_Tags.Where(x => x.ProductID == product.Id).ToList();

            foreach (var tag in oldTags)
            {
                _context.Many_Product_Tags.Remove(tag);
            }

            foreach (var tagID in editTourDto.TagIDs)
            {
                Many_Product_Tag many = new Many_Product_Tag()
                {
                    ProductID = product.Id,
                    TagID = tagID
                };
                _context.Many_Product_Tags.Add(many);
            }

            var oldCategories = _context.Many_Tour_TourCategories.Where(x => x.TourID == tour.Id).ToList();

            foreach (var category in oldCategories)
            {
                _context.Many_Tour_TourCategories.Remove(category);
            }

            foreach (var categoryID in editTourDto.TourCategoryIDs)
            {
                Many_Tour_TourCategory many = new Many_Tour_TourCategory()
                {
                    TourID = tour.Id,
                    TourCategoryID = categoryID
                };
                _context.Many_Tour_TourCategories.Add(many);
            }
            //https://localhost:7177/Tour/EditTour/ce32d433-973a-4ec4-accd-73f42204af35
            var oldDestinations = _context.Many_Tour_Destinations.Where(x => x.TourID == tour.Id).ToList();

            foreach (var destination in oldDestinations)
            {
                _context.Many_Tour_Destinations.Remove(destination);
            }

            foreach (var destinationID in editTourDto.DestinationIDs)
            {
                Many_Tour_Destination many = new Many_Tour_Destination()
                {
                    TourID = tour.Id,
                    DestinationID = destinationID
                };
                _context.Many_Tour_Destinations.Add(many);
            }

            var oldInclusions = _context.Many_Tour_Inclusions.Where(x => x.TourID == tour.Id).ToList();

            foreach (var inclusion in oldInclusions)
            {
                _context.Many_Tour_Inclusions.Remove(inclusion);
            }

            foreach (var inclusionID in editTourDto.InclusionIDs)
            {
                Many_Tour_Inclusion many = new Many_Tour_Inclusion()
                {
                    TourID = tour.Id,
                    SystemOptionID = inclusionID
                };
                _context.Many_Tour_Inclusions.Add(many);
            }

            var oldExclusions = _context.Many_Tour_Exclusions.Where(x => x.TourID == tour.Id).ToList();

            foreach (var exclusion in oldExclusions)
            {
                _context.Many_Tour_Exclusions.Remove(exclusion);
            }

            foreach (var exclusionID in editTourDto.ExclusionIDs)
            {
                Many_Tour_Exclusion many = new Many_Tour_Exclusion()
                {
                    TourID = tour.Id,
                    SystemOptionID = exclusionID
                };
                _context.Many_Tour_Exclusions.Add(many);
            }

            var oldSightToSee = _context.Many_Tour_SightToSees.Where(x => x.TourID == tour.Id).ToList();

            foreach (var sightToSee in oldSightToSee)
            {
                _context.Many_Tour_SightToSees.Remove(sightToSee);
            }

            foreach (var sightToSeeID in editTourDto.SightToSeeIDs)
            {
                Many_Tour_SightToSee many = new Many_Tour_SightToSee()
                {
                    TourID = tour.Id,
                    SystemOptionID = sightToSeeID
                };
                _context.Many_Tour_SightToSees.Add(many);
            }
        }




        public async Task<WebTourDetailDto> GetTourDetailAdditionals(Guid productId, int languageId)
        {
            WebTourDetailDto value = (new WebTourDetailDto()
            {
                AdditionalServices = await (from manyService in _many_Product_AdditionalServiceItems
                                            where manyService.ProductID == productId
                                            && manyService.Status && !manyService.IsDeleted
                                            join service in _additionalServices
                                            on manyService.AdditionalServiceID equals service.Id
                                            join serviceLanguage in _additionalServiceLanguageItems
                                            on service.Id equals serviceLanguage.AdditionalServiceID
                                            where serviceLanguage.LanguageID == languageId
                                            && service.Status && !service.IsDeleted
                                            orderby manyService.Order
                                            select new AdditionalServiceDto()
                                            {
                                                AdditionalServiceID = service.Id,
                                                AdditionalServiceName = serviceLanguage.DisplayName,
                                                IsPerPerson = service.IsPerPerson,
                                                IsPerDay = service.IsPerDay, // isperday de yoktu ben ekliyorum neden yok acaba
                                                Isrequired = manyService.IsRequired,
                                                IsSpecialNumber = service.IsSpecialNumber,
                                                Options = (from manyOption in _many_Product_AdditionalServiceOptions
                                                           where manyOption.Many_Product_AdditionalServiceID == manyService.Id
                                                           join option in _additionalServiceOptions
                                                           on manyOption.AdditionalServiceOptionID equals option.Id
                                                           join optionLanguage in _additionalServiceOptionLanguageItems
                                                           on option.Id equals optionLanguage.AdditionalServiceOptionID
                                                           where optionLanguage.LanguageID == languageId
                                                           && option.Status && !option.IsDeleted
                                                           orderby option.Order
                                                           select new AdditionalServiceOptionDto()
                                                           {
                                                               OptionID = option.Id,
                                                               OptionName = optionLanguage.DisplayName,
                                                               OptionPrice = 0, //TODO: burada kaldık addtionallerın fiyatlarınıda json gibi yapmak lazım
                                                               Order = option.Order,
                                                               Inputs = (from many in _many_Option_Input
                                                                         where many.AdditionalServiceOptionID == option.Id
                                                                         join input in _additionalServiceInputs
                                                                         on many.AdditionalServiceInputID equals input.Id
                                                                         join inputLanguage in _additionalServiceInputLanguageItems
                                                                         on input.Id equals inputLanguage.AdditionalServiceInputID
                                                                         where inputLanguage.LanguageID == languageId
                                                                         && input.Status && !input.IsDeleted
                                                                         select new AdditionalServiceOptionInputDto()
                                                                         {
                                                                             InputID = input.Id,
                                                                             InputName = inputLanguage.DisplayName,
                                                                             Order = input.Order,
                                                                             InputType = input.InputTypeID
                                                                         }).ToList(),
                                                           }).ToList(),

                                            }).ToListAsync(),
            });

            return value;
        }


    }
}
