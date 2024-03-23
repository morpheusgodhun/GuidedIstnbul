
using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.FaqManagementDtos;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using Dto.ApiPanelDtos.ProductDtos.TourDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Services;
using System.IO.Pipelines;
using System.Text.Json;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelTourController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductLanguageService _productLanguageService;
        private readonly ITourService _tourService;
        private readonly ITourLanguageService _tourLanguageService;
        private readonly ISystemOptionLanguageService _systemOptionLanguageService;
        private readonly IMany_Product_TagService _many_Product_TagService;
        private readonly IMany_Tour_DestinationService _many_Tour_DestinationService;
        private readonly IMany_Tour_TourCategoryService _many_Tour_TourCategoryService;
        private readonly IMany_Tour_InclusionService _many_Tour_InclusionService;
        private readonly IMany_Tour_ExclusionService _many_Tour_ExclusionService;
        private readonly IMany_Tour_SightToSeeService _many_Tour_SightToSeeService;
        private readonly IMany_Product_RoleTemplateService _many_Product_RoleTemplateService;
        private readonly ITourProgramService _tourProgramService;
        private readonly ITourProgramLanguageService _tourProgramLanguageService;
        private readonly IDestinationLanguageService _destinationLanguageService;
        private readonly ITourCategoryLanguageService _tourCategoryLanguageService;
        private readonly ICancellationPolicyService _cancellationPolicyService;
        private readonly ITagLanguageService _tagLanguageService;
        private readonly IProductSellLimitService _productSellLimitService;
        private readonly IProductFaqService _productFaqService;
        private readonly IProductFaqLanguageService _productFaqLanguageService;
        private readonly ITourPriceService _tourPriceService;
        private readonly ITourPriceItemService _tourPriceItemService;
        private readonly IRouteService _routeService;

        public PanelTourController(IProductService productService, IProductLanguageService productLanguageService, ITourService tourService, ITourLanguageService tourLanguageService, ISystemOptionLanguageService systemOptionLanguageService, IMany_Product_TagService many_Product_TagService, IMany_Tour_DestinationService many_Tour_DestinationService, IMany_Tour_TourCategoryService many_Tour_TourCategoryService, IMany_Tour_InclusionService many_Tour_InclusionService, IMany_Tour_ExclusionService many_Tour_ExclusionService, IMany_Tour_SightToSeeService many_Tour_SightToSeeService, ITourProgramService tourProgramService, ITourProgramLanguageService tourProgramLanguageService, IDestinationLanguageService destinationLanguageService, ITourCategoryLanguageService tourCategoryLanguageService, ICancellationPolicyService cancellationPolicyService, ITagLanguageService tagLanguageService, IProductSellLimitService productSellLimitService, IProductFaqService productFaqService, IProductFaqLanguageService productFaqLanguageService, ITourPriceService tourPriceService, ITourPriceItemService tourPriceItemService, IMany_Product_RoleTemplateService many_Product_RoleTemplateService, IRouteService routeService)
        {
            _productService = productService;
            _productLanguageService = productLanguageService;
            _tourService = tourService;
            _tourLanguageService = tourLanguageService;
            _systemOptionLanguageService = systemOptionLanguageService;
            _many_Product_TagService = many_Product_TagService;
            _many_Tour_DestinationService = many_Tour_DestinationService;
            _many_Tour_TourCategoryService = many_Tour_TourCategoryService;
            _many_Tour_InclusionService = many_Tour_InclusionService;
            _many_Tour_ExclusionService = many_Tour_ExclusionService;
            _many_Tour_SightToSeeService = many_Tour_SightToSeeService;
            _tourProgramService = tourProgramService;
            _tourProgramLanguageService = tourProgramLanguageService;
            _destinationLanguageService = destinationLanguageService;
            _tourCategoryLanguageService = tourCategoryLanguageService;
            _cancellationPolicyService = cancellationPolicyService;
            _tagLanguageService = tagLanguageService;
            _productSellLimitService = productSellLimitService;
            _productFaqService = productFaqService;
            _productFaqLanguageService = productFaqLanguageService;
            _tourPriceService = tourPriceService;
            _tourPriceItemService = tourPriceItemService;
            _many_Product_RoleTemplateService = many_Product_RoleTemplateService;
            _routeService = routeService;
        }

        [HttpGet]
        public CustomResponseDto<List<TourListDto2>> TourList()
        {
            List<TourListDto2> tourListDtos = (from product in _productService.GetAll(x => !x.IsDeleted && x.IsTour)
                                               join productLanguage in _productLanguageService.GetAll(x => !x.IsDeleted)
                                               on product.Id equals productLanguage.ProductID
                                               where productLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                               join tour in _tourService.GetAll(x => !x.IsDeleted)
                                               on product.Id equals tour.ProductID
                                               select new TourListDto2()
                                               {
                                                   ProductID = product.Id,
                                                   Status = product.Status,
                                                   Order = product.Order,
                                                   ShowOnHomepage = product.ShowOnHomePage,
                                                   TourTitle = productLanguage.DisplayName,
                                                   TourType = tour.TourTypeID is not null ? _systemOptionLanguageService.Where(x => x.LanguageID == LanguageList.BaseLanguage.Id && x.SystemOptionId == tour.TourTypeID).FirstOrDefault().Name : "",
                                                   Uncompleted = tour.StartCityID is not null && tour.StartCityID != 0 ? false : true,
                                                   TourID = tour.Id
                                               }).ToList();

            return CustomResponseDto<List<TourListDto2>>.Success(200, tourListDtos);

        }



        [HttpGet("{id}")]
        public CustomResponseDto<EditTourDto> EditTour(Guid id)
        {

            GIT_ID agentId = new GIT_ID();

            EditTourDto editTourDto = (from product in _productService.GetAll(x => !x.IsDeleted && x.IsTour)
                                       where product.Id == id
                                       join productLanguage in _productLanguageService.GetAll(x => !x.IsDeleted)
                                       on product.Id equals productLanguage.ProductID
                                       where productLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                       join tour in _tourService.GetAll(x => !x.IsDeleted)
                                       on product.Id equals tour.ProductID
                                       select new EditTourDto()
                                       {

                                           ProductID = id,
                                           ProductName = productLanguage.DisplayName,
                                           //OperatorAgentID = agentId.ID, bunu sonradan kapattım gerek yok gibi kullanmaya şimidlik
                                           YoutubeLink = product.YoutubeLink,
                                           BannerImagePath = product.BannerImagePath,
                                           CardImagePath = product.CardImagePath,
                                           CancellationPolicyID = product.CancellationPolicyID,
                                           TagIDs = (from many in _many_Product_TagService.GetAll()
                                                     where many.ProductID == id
                                                     select many.TagID).ToList(),
                                           CutOffDay = product.CutoffHour / 24,
                                           CutOffHour = product.CutoffHour % 24,
                                           CustomerDepositoAmount = product.CustomerDeposito,
                                           AgentDepositoAmount = product.AgentDeposito,
                                           DayOfPayment = product.PaymentDate,
                                           MinimumPayoutPercent = (int)product.MinimumPayoutPercent,
                                           IsChildPolicyActive = product.IsChildPolicyActive,
                                           TourTypeID = tour.TourTypeID is not null ? new Guid(tour.TourTypeID.ToString()) : Guid.Empty,
                                           SegmentID = tour.SegmentID is not null ? new Guid(tour.SegmentID.ToString()) : Guid.Empty,
                                           StartingCityID = tour.StartCityID is not null ? (int)tour.StartCityID : 0,
                                           DestinationIDs = (from many in _many_Tour_DestinationService.GetAll()
                                                             where many.TourID == tour.Id
                                                             select many.DestinationID).ToList(),
                                           TourCategoryIDs = (from many in _many_Tour_TourCategoryService.GetAll()
                                                              where many.TourID == tour.Id
                                                              select many.TourCategoryID).ToList(),
                                           StartTimeIDs = JsonSerializer.Deserialize<List<int>>(!string.IsNullOrEmpty(tour.StartTimeIDs) ? tour.StartTimeIDs : "[]"),
                                           SuggestedStartTimeID = tour.SuggestedStartTimeID is not null ? (int)tour.SuggestedStartTimeID : 1,
                                           Duration = tour.Duration,
                                           //SelectableDurations = tour.SelectableDurations.Length > 0 ? tour.SelectableDurations.Split(',').Select(x => Convert.ToInt32(x)).ToList() : new List<int>(),
                                           SelectableDurations = JsonSerializer.Deserialize<List<int>>(string.IsNullOrWhiteSpace(tour.SelectableDurations) ? "[" + tour.Duration.ToString() + "]" : tour.SelectableDurations),
                                           InclusionIDs = (from many in _many_Tour_InclusionService.GetAll()
                                                           where many.TourID == tour.Id
                                                           select many.SystemOptionID).ToList(),
                                           ExclusionIDs = (from many in _many_Tour_ExclusionService.GetAll()
                                                           where many.TourID == tour.Id
                                                           select many.SystemOptionID).ToList(),
                                           SightToSeeIDs = (from many in _many_Tour_SightToSeeService.GetAll()
                                                            where many.TourID == tour.Id
                                                            select many.SystemOptionID).ToList(),
                                           IsPerPerson = tour.IsPerPerson,
                                           IsPerDay = tour.IsPerDay,
                                           Order = product.Order,
                                           PersonLimit = product.PersonLimit,
                                           IsPersonLimited = product.IsPersonLimited,

                                       }).SingleOrDefault();


            return CustomResponseDto<EditTourDto>.Success(200, editTourDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditTour(EditTourDto editTourDto)
        {
            _tourService.EditTour(editTourDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditTourDto> LanguageEditTour(Guid id, int languageId)
        {
            Core.Entities.Route? route = _routeService.GetAll(x => x.EntityId == id).FirstOrDefault();
            LanguageEditTourDto languageEditTourDto = (from product in _productService.GetAll()
                                                       where product.Id == id
                                                       join productLanguage in _productLanguageService.GetAll()
                                                       on product.Id equals productLanguage.ProductID
                                                       where productLanguage.LanguageID == languageId
                                                       join tour in _tourService.GetAll()
                                                       on product.Id equals tour.ProductID
                                                       join tourLanguage in _tourLanguageService.GetAll()
                                                       on tour.Id equals tourLanguage.TourID
                                                       //join route in _routeService.GetAll(x=>x.EntityId==id)
                                                       //on productLanguage.ProductID equals route.EntityId
                                                       where tourLanguage.LanguageID == languageId
                                                       select new LanguageEditTourDto()
                                                       {
                                                           ProductLanguageID = productLanguage.Id,
                                                           ProductName = product.ProductName,
                                                           DisplayName = productLanguage.DisplayName,
                                                           TourLanguageID = tourLanguage.Id,
                                                           LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name,
                                                           Slug = productLanguage.Slug,
                                                           DurationText = tourLanguage.DurationText,
                                                           StartPoint = tourLanguage.TourStartPoint,
                                                           EndPoint = tourLanguage.TourEndPoint,
                                                           Content = tourLanguage.Content,
                                                           SitemapInclude = route is not null ? route.SitemapInclude : false
                                                       }).FirstOrDefault();


            return CustomResponseDto<LanguageEditTourDto>.Success(200, languageEditTourDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditTour(LanguageEditTourDto languageEditTour)
        {
            ProductLanguageItem productLanguageItem = _productLanguageService.GetById(languageEditTour.ProductLanguageID);
            TourLanguageItem tourLanguageItem = _tourLanguageService.GetById(languageEditTour.TourLanguageID);

            productLanguageItem.DisplayName = languageEditTour.DisplayName;
            productLanguageItem.Slug = languageEditTour.Slug;
            productLanguageItem.SitemapInclude = languageEditTour.SitemapInclude;
            _productLanguageService.Update(productLanguageItem);

            tourLanguageItem.DurationText = languageEditTour.DurationText;
            tourLanguageItem.TourStartPoint = languageEditTour.StartPoint;
            tourLanguageItem.TourEndPoint = languageEditTour.EndPoint;
            tourLanguageItem.Content = languageEditTour.Content;
            _tourLanguageService.Update(tourLanguageItem);

            return CustomResponseNullDto.Success(204);
        }



        //GeneralInfo

        [HttpGet("{id}")]
        public async Task<CustomResponseDto<TourDetailDto>> TourDetail(Guid id)
        {
            var postedRoleTemplateIds = await _many_Product_RoleTemplateService.GetAttachedRolesByProductIdAsync(id);

            TourDetailDto tourDetailDto = (from product in _productService.GetAll()
                                           where product.Id == id
                                           join productLanguage in _productLanguageService.GetAll()
                                           on product.Id equals productLanguage.ProductID
                                           where productLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                           join tour in _tourService.GetAll()
                                           on product.Id equals tour.ProductID
                                           join tourLanguage in _tourLanguageService.GetAll()
                                           on tour.Id equals tourLanguage.TourID
                                           select new TourDetailDto()
                                           {
                                               ProductID = product.Id,
                                               ProductName = product.ProductName,
                                               OperatorAgent = "Guided İstanbul Tour", //TODO: Nurası neden statik
                                               Status = product.Status,
                                               IsPerPerson = tour.IsPerPerson,
                                               IsPerDay = tour.IsPerDay,
                                               PostedRoleTemplateIds = postedRoleTemplateIds,
                                               Type = _systemOptionLanguageService.GetAll(x => x.SystemOptionId == tour.TourTypeID).FirstOrDefault().Name,
                                               Segment = _systemOptionLanguageService.GetAll(x => x.SystemOptionId == tour.SegmentID).FirstOrDefault().Name,
                                               SelectableDurations = tour.SelectableDurations,
                                               StartCity = new CityList().Cities.FirstOrDefault(x => x.ID == tour.StartCityID).Value,
                                               SuggestedStartTime = StartTimeList.StartTimes.FirstOrDefault(x => x.ID == tour.SuggestedStartTimeID).Value,
                                               Destinations = (from many in _many_Tour_DestinationService.GetAll()
                                                               where many.TourID == tour.Id
                                                               join destinationLanguage in _destinationLanguageService.GetAll()
                                                               on many.DestinationID equals destinationLanguage.DestinationID
                                                               where destinationLanguage.LangaugeID == LanguageList.BaseLanguage.Id
                                                               select destinationLanguage.DisplayName).ToList(),
                                               Categories = (from many in _many_Tour_TourCategoryService.GetAll()
                                                             where many.TourID == tour.Id
                                                             join tourCategoryLanguage in _tourCategoryLanguageService.GetAll()
                                                             on many.TourCategoryID equals tourCategoryLanguage.TourCategoryID
                                                             where tourCategoryLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                             select tourCategoryLanguage.CategoryName).ToList(),
                                               Inclusions = (from many in _many_Tour_InclusionService.GetAll()
                                                             where many.TourID == tour.Id
                                                             join systemOptionLanguage in _systemOptionLanguageService.GetAll()
                                                             on many.SystemOptionID equals systemOptionLanguage.SystemOptionId
                                                             where systemOptionLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                             select systemOptionLanguage.Name).ToList(),
                                               Exclusions = (from many in _many_Tour_ExclusionService.GetAll()
                                                             where many.TourID == tour.Id
                                                             join systemOptionLanguage in _systemOptionLanguageService.GetAll()
                                                             on many.SystemOptionID equals systemOptionLanguage.SystemOptionId
                                                             where systemOptionLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                             select systemOptionLanguage.Name).ToList(),
                                               SightsToSee = (from many in _many_Tour_SightToSeeService.GetAll()
                                                              where many.TourID == tour.Id
                                                              join systemOptionLanguage in _systemOptionLanguageService.GetAll()
                                                              on many.SystemOptionID equals systemOptionLanguage.SystemOptionId
                                                              where systemOptionLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                              select systemOptionLanguage.Name).ToList(),
                                               Duration = tourLanguage.DurationText,
                                               StartPoint = tourLanguage.TourStartPoint,
                                               EndPoint = tourLanguage.TourEndPoint,
                                               CancellationPolicy = _cancellationPolicyService.GetById(product.CancellationPolicyID).Name,
                                               CutOffTime = product.CutoffHour / 24 + " Day " + product.CutoffHour % 24 + " Hour",
                                               Tags = (from many in _many_Product_TagService.GetAll()
                                                       where many.ProductID == product.Id
                                                       join tag in _tagLanguageService.GetAll()
                                                       on many.TagID equals tag.TagID
                                                       where tag.LangaugeID == LanguageList.BaseLanguage.Id
                                                       select tag.DisplayName).ToList()
                                           }).FirstOrDefault();


            return CustomResponseDto<TourDetailDto>.Success(200, tourDetailDto);
        }

        //Price

        [HttpGet("{id}")]
        public CustomResponseDto<List<TourPriceListDto>> TourPriceList(Guid id)
        {
            List<TourPriceListDto> tourPriceListDtos = (from tour in _tourService.GetAll(x => x.ProductID == id)
                                                        join price in _tourPriceService.GetAll(x => !x.IsDeleted)
                                                        on tour.Id equals price.TourID
                                                        select new TourPriceListDto()
                                                        {
                                                            PriceID = price.Id,
                                                            Priority = price.Priority,
                                                            FromDate = Convert.ToDateTime(price.FromDate),
                                                            ToDate = Convert.ToDateTime(price.ToDate),
                                                            Prices = (from priceItem in _tourPriceItemService.GetAll(x => x.TourPriceID == price.Id)
                                                                      select new Dto.ApiPanelDtos.ProductDtos.TourDtos.TourPriceItem()
                                                                      {
                                                                          Price = priceItem.Price,
                                                                          PersonPolicyID = priceItem.PersonPolicyID
                                                                      }).ToList()
                                                        }
                         ).ToList();


            return CustomResponseDto<List<TourPriceListDto>>.Success(200, tourPriceListDtos);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<bool> IsTourPerPerson(Guid id)
        {
            bool isPerPerson = _tourService.GetAll(x => x.ProductID == id).FirstOrDefault().IsPerPerson;

            return CustomResponseDto<bool>.Success(200, isPerPerson);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<bool> IsAskForPriceActive(Guid id)
        {
            bool askForPrice = _tourService.GetAll(x => x.ProductID == id).FirstOrDefault().AskForPrice;

            return CustomResponseDto<bool>.Success(200, askForPrice);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<Guid> TourIdByProductId(Guid id)
        {
            Guid tourID = new Guid(_productService.GetById(id).TourID.ToString());

            return CustomResponseDto<Guid>.Success(200, tourID);
        }

        [HttpPost]
        public CustomResponseDto<Guid> AddTourPrice(AddTourPriceDto addTourPriceDto)
        {
            TourPrice tourPrice = new TourPrice()
            {
                Priority = addTourPriceDto.Priority,
                ToDate = addTourPriceDto.ToDate,
                FromDate = addTourPriceDto.FromDate,
                TourID = addTourPriceDto.TourID
            };
            _tourPriceService.Add(tourPrice);

            foreach (var price in addTourPriceDto.Prices)
            {
                if (price.Price > 0)
                {
                    Core.Entities.TourPriceItem tourPriceItem = new Core.Entities.TourPriceItem()
                    {
                        TourPriceID = tourPrice.Id,
                        Price = price.Price,
                        PersonPolicyID = price.PersonPolicyID
                    };

                    _tourPriceItemService.Add(tourPriceItem);
                }
            }
            Tour tour = _tourService.GetById(tourPrice.TourID);

            return CustomResponseDto<Guid>.Success(204, tour.ProductID);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto DeleteTourPrice(Guid id)
        {
            _tourPriceService.Remove(_tourPriceService.GetById(id));

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleAskForPrice(Guid id)
        {
            Tour tour = _tourService.GetAll(x => x.ProductID == id).FirstOrDefault();
            tour.AskForPrice = !tour.AskForPrice;
            _tourService.Update(tour);

            return CustomResponseNullDto.Success(204);
        }

        //Price End



        //Sell Limit

        [HttpGet("{id}")]
        public CustomResponseDto<List<TourSellLimitListDto>> SellLimitList(Guid id)
        {
            List<TourSellLimitListDto> tourSellLimitListDtos = (from sellLimit in _productSellLimitService.GetAll()
                                                                where sellLimit.ProductID == id
                                                                select new TourSellLimitListDto()
                                                                {
                                                                    SellLimitID = sellLimit.Id,
                                                                    Date = sellLimit.Date,
                                                                    SellLimit = sellLimit.SellLimit
                                                                }).ToList();



            return CustomResponseDto<List<TourSellLimitListDto>>.Success(200, tourSellLimitListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddSellLimit(AddTourSellLimitDto addTourSellLimitDto)
        {
            ProductSellLimit sellLimit = new ProductSellLimit()
            {
                ProductID = addTourSellLimitDto.ProductID,
                Date = addTourSellLimitDto.Date,
                SellLimit = addTourSellLimitDto.SellLimit
            };
            _productSellLimitService.Add(sellLimit);

            return CustomResponseNullDto.Success(204);
        }

        [HttpPost]
        public CustomResponseDto<Guid> EditSellLimit(EditTourSellLimitDto editTourSellLimitDto)
        {
            ProductSellLimit sellLimit = _productSellLimitService.GetById(editTourSellLimitDto.SellLimitID);
            sellLimit.SellLimit = editTourSellLimitDto.SellLimit;
            _productSellLimitService.Update(sellLimit);

            return CustomResponseDto<Guid>.Success(204, sellLimit.ProductID);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<Guid> DeleteSellLimit(Guid id)
        {
            ProductSellLimit sellLimit = _productSellLimitService.GetById(id);
            _productSellLimitService.Remove(sellLimit);

            return CustomResponseDto<Guid>.Success(204, sellLimit.ProductID);
        }

        //Sell Limit End



        //Tour Program

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditTourProgramDto> LanguageEditTourProgram(Guid id, int languageId)
        {

            LanguageEditTourProgramDto languageEditTourProgramDto = (from product in _productService.GetAll()
                                                                     where product.Id == id
                                                                     join tour in _tourService.GetAll()
                                                                     on product.Id equals tour.ProductID
                                                                     select new LanguageEditTourProgramDto()
                                                                     {
                                                                         TourID = tour.Id,
                                                                         LanguageID = languageId,
                                                                         LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name,
                                                                         Programs = (from program in _tourProgramService.GetAll()
                                                                                     where program.TourID == tour.Id
                                                                                     join programLanguage in _tourProgramLanguageService.GetAll()
                                                                                     on program.Id equals programLanguage.TourProgramID
                                                                                     where programLanguage.LanguageID == languageId
                                                                                     select new LanguageEditTourProgramItemDto()
                                                                                     {
                                                                                         TourProgramLanguageItemID = programLanguage.Id,
                                                                                         Day = program.Day,
                                                                                         Title = programLanguage.Title,
                                                                                         Content = programLanguage.Content
                                                                                     }).OrderBy(x => x.Day).ToList()
                                                                     }).FirstOrDefault();



            return CustomResponseDto<LanguageEditTourProgramDto>.Success(200, languageEditTourProgramDto);
        }

        [HttpPost]

        public CustomResponseNullDto LanguageEditTourProgram(LanguageEditTourProgramDto languageEditTourProgramDto)
        {
            foreach (var programDto in languageEditTourProgramDto.Programs)
            {
                TourProgramLanguageItem tourProgramLanguageItem = _tourProgramLanguageService.GetById(programDto.TourProgramLanguageItemID);
                tourProgramLanguageItem.Title = programDto.Title;
                tourProgramLanguageItem.Content = programDto.Content;
                _tourProgramLanguageService.Update(tourProgramLanguageItem);
            }
            return CustomResponseNullDto.Success(204);
        }

        //Tour Program End

        // Tour Faq

        [HttpGet("{id}")]
        public CustomResponseDto<List<TourFaqListDto>> TourFaqList(Guid id)
        {
            List<TourFaqListDto> tourFaqListDtos = (from faq in _productFaqService.GetAll()
                                                    where faq.ProductID == id
                                                    join faqLanguageItem in _productFaqLanguageService.GetAll()
                                                    on faq.Id equals faqLanguageItem.ProductFaqID
                                                    where faqLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                    select new TourFaqListDto()
                                                    {
                                                        FaqID = faq.Id,
                                                        Status = faq.Status,
                                                        Order = faq.Order,
                                                        Answer = faqLanguageItem.Answer,
                                                        Question = faqLanguageItem.Question,
                                                    }).ToList();


            return CustomResponseDto<List<TourFaqListDto>>.Success(200, tourFaqListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddTourFaq(AddTourFaqDto addTourFaqDto)
        {
            ProductFaq faq = new ProductFaq()
            {
                ProductID = addTourFaqDto.ProductID,
                Order = addTourFaqDto.Order,

            };
            _productFaqService.Add(faq);

            foreach (var language in LanguageList.AllLanguages)
            {
                ProductFaqLanguageItem productFaqLanguageItem = new ProductFaqLanguageItem()
                {
                    LanguageID = language.Id,
                    ProductFaqID = faq.Id,
                    Answer = addTourFaqDto.Answer,
                    Question = addTourFaqDto.Question
                };
                _productFaqLanguageService.Add(productFaqLanguageItem);
            }

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditTourFaqDto> EditTourFaq(Guid id)
        {
            ProductFaq faq = _productFaqService.GetById(id);

            EditTourFaqDto editTourFaqDto = new EditTourFaqDto()
            {
                FaqID = faq.Id,
                Order = faq.Order
            };

            return CustomResponseDto<EditTourFaqDto>.Success(200, editTourFaqDto);
        }

        [HttpPost]
        public CustomResponseDto<Guid> EditTourFaq(EditTourFaqDto editTourFaqDto)
        {
            ProductFaq faq = _productFaqService.GetById(editTourFaqDto.FaqID);
            faq.Order = editTourFaqDto.Order;
            _productFaqService.Update(faq);

            return CustomResponseDto<Guid>.Success(204, faq.ProductID);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditTourFaqDto> LanguageEditTourFaq(Guid id, int languageId)
        {
            ProductFaqLanguageItem productFaqLanguageItem = _productFaqLanguageService.GetAll(x => x.ProductFaqID == id && x.LanguageID == languageId).FirstOrDefault();

            LanguageEditTourFaqDto languageEditTourFaqDto = new LanguageEditTourFaqDto()
            {
                FaqLanguageID = productFaqLanguageItem.Id,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name,
                Question = productFaqLanguageItem.Question,
                Answer = productFaqLanguageItem.Answer
            };


            return CustomResponseDto<LanguageEditTourFaqDto>.Success(200, languageEditTourFaqDto);

        }

        [HttpPost]
        public CustomResponseDto<Guid> LanguageEditTourFaq(LanguageEditTourFaqDto languageEditTourFaqDto)
        {

            ProductFaqLanguageItem productFaqLanguageItem = _productFaqLanguageService.GetById(languageEditTourFaqDto.FaqLanguageID);

            productFaqLanguageItem.Answer = languageEditTourFaqDto.Answer;
            productFaqLanguageItem.Question = languageEditTourFaqDto.Question;
            _productFaqLanguageService.Update(productFaqLanguageItem);




            return CustomResponseDto<Guid>.Success(204, _productFaqService.GetById(productFaqLanguageItem.ProductFaqID).ProductID);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<Guid> ToggleTourFaqStatus(Guid id)
        {
            ProductFaq faq = _productFaqService.GetById(id);
            faq.Status = !faq.Status;
            _productFaqService.Update(faq);

            return CustomResponseDto<Guid>.Success(204, faq.ProductID);
        }

        // Tour Faq End


        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> TourSelectList()
        {
            List<SelectListOptionDto> tourSelectList = (from product in _productService.GetAll(x => !x.IsDeleted && x.IsTour)
                                                        select new SelectListOptionDto()
                                                        {
                                                            OptionID = product.Id,
                                                            Option = product.ProductName
                                                        }).ToList();

            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, tourSelectList);
        }


        [HttpGet("{id}")]
        public CustomResponseNullDto Delete(Guid id)
        {
            //buraya gelen id turun idsi ama lakinki onunla akalaı productıda silelim
            var tour = _tourService.GetById(id);

            _tourService.Delete(tour.Id);
            _productService.Delete(tour.ProductID);

            return CustomResponseNullDto.Success(204);
        }
    }
}
