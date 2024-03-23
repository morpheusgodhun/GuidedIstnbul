using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiWebDtos.ApiToWebDtos.AdditionalService;
using Dto.ApiWebDtos.ApiToWebDtos.Service;
using Dto.ApiWebDtos.GeneralDtos;

namespace Service.Services
{
    public class ServiceService : GenericService<Core.Entities.Service>, IServiceService
    {
        private readonly IProductService _productService;
        private readonly IProductLanguageService _productLanguageService;

        private readonly IServiceLanguageService _serviceLanguageService;
        private readonly IConstantValueService _constantValueService;
        private readonly IMany_Product_TagService _many_Product_TagService;
        private readonly ITagLanguageService _tagLanguageService;
        private readonly IProductImageService _productImageService;
        private readonly ISystemOptionService _systemOptionService;
        private readonly ISystemOptionLanguageService _systemOptionLanguageService;
        private readonly IMany_Product_AdditionalServiceService _many_Product_AdditionalServiceService;
        private readonly IMany_Product_AdditionalServiceOptionService _many_Product_AdditionalServiceOptionService;
        private readonly IAdditionalServiceLanguageService _additionalServiceLanguageService;
        private readonly IAdditionalServiceService _additionalServiceService;
        private readonly IAdditionalServiceOptionService _additionalServiceOptionService;
        private readonly IAdditionalServiceOptionLanguageService _additionalServiceOptionLanguageService;
        private readonly IAdditionalServiceInputService _additionalServiceInputService;
        private readonly IAdditionalServiceInputLanguageService _additionalServiceInputLanguageService;
        private readonly IMany_AdditionalServiceOption_AdditionalServiceInputService _many_AdditionalServiceOption_AdditionalServiceInputService;
        private readonly IAdditionalServiceOptionPriceService _additionalServiceOptionPriceService;


        public ServiceService(IGenericRepository<Core.Entities.Service> repository, IUnitOfWork unitOfWork, IProductService productService, IProductLanguageService productLanguageService, IServiceLanguageService serviceLanguageService, IConstantValueService constantValueService, IMany_Product_TagService many_Product_TagService, ITagLanguageService tagLanguageService, IProductImageService productImageService, ISystemOptionService systemOptionService, ISystemOptionLanguageService systemOptionLanguageService, IMany_Product_AdditionalServiceService many_Product_AdditionalServiceService, IMany_Product_AdditionalServiceOptionService many_Product_AdditionalServiceOptionService, IAdditionalServiceLanguageService additionalServiceLanguageService, IAdditionalServiceService additionalServiceService, IAdditionalServiceOptionService additionalServiceOptionService, IAdditionalServiceOptionLanguageService additionalServiceOptionLanguageService, IAdditionalServiceInputService additionalServiceInputService, IAdditionalServiceInputLanguageService additionalServiceInputLanguageService, IMany_AdditionalServiceOption_AdditionalServiceInputService many_AdditionalServiceOption_AdditionalServiceInputService, IAdditionalServiceOptionPriceService additionalServiceOptionPriceService) : base(repository, unitOfWork)
        {
            _productService = productService;
            _productLanguageService = productLanguageService;
            _serviceLanguageService = serviceLanguageService;
            _constantValueService = constantValueService;
            _many_Product_TagService = many_Product_TagService;
            _tagLanguageService = tagLanguageService;
            _productImageService = productImageService;
            _systemOptionService = systemOptionService;
            _systemOptionLanguageService = systemOptionLanguageService;
            _many_Product_AdditionalServiceService = many_Product_AdditionalServiceService;
            _many_Product_AdditionalServiceOptionService = many_Product_AdditionalServiceOptionService;
            _additionalServiceLanguageService = additionalServiceLanguageService;
            _additionalServiceService = additionalServiceService;
            _additionalServiceOptionService = additionalServiceOptionService;
            _additionalServiceOptionLanguageService = additionalServiceOptionLanguageService;
            _additionalServiceInputService = additionalServiceInputService;
            _additionalServiceInputLanguageService = additionalServiceInputLanguageService;
            _many_AdditionalServiceOption_AdditionalServiceInputService = many_AdditionalServiceOption_AdditionalServiceInputService;
            _additionalServiceOptionPriceService = additionalServiceOptionPriceService;
        }

        public List<ServiceListItemDto> GetServiceList(int LanguageId)
        {
            var servicelist = (from product in _productService.GetAll(x => !x.IsDeleted && !x.IsTour)
                               join productLanguage in _productLanguageService.GetAll(x => x.LanguageID == LanguageId)
                               on product.Id equals productLanguage.ProductID
                               where product.Status && !product.IsDeleted
                               select new ServiceListItemDto()
                               {
                                   ServiceID = new Guid(product.ServiceID.ToString()),
                                   CardImagePath = product.CardImagePath,
                                   Name = productLanguage.DisplayName,
                                   Slug = productLanguage.Slug,
                                   Price = ServiceAdditionalsMinPriceForList(product.Id, LanguageId), //_additionalServiceOptionPriceService.SetOptionalPricesForTourDetail(tourDetail.AdditionalServices),
                                   Rate = 5
                               }).ToList();

            return servicelist;
        }

        public GetServiceDetailDto GetServiceDetail(Guid serviceID, int LanguageID)
        {
            GetServiceDetailDto getServiceDetailDto = (from product in _productService.GetAll(x => x.ServiceID == serviceID)
                                                       join productLanguage in _productLanguageService.GetAll(x => x.LanguageID == LanguageID)
                                                       on product.Id equals productLanguage.ProductID
                                                       join serviceLanguage in _serviceLanguageService.GetAll(x => x.LanguageID == LanguageID)
                                                       on product.ServiceID equals serviceLanguage.ServiceID
                                                       select new GetServiceDetailDto()
                                                       {
                                                           //ConstantValues = _constantValueService.GetConstantValueByPageName("Service Detail", LanguageID),
                                                           ProductID = product.Id,
                                                           ServiceBannerImagePath = product.BannerImagePath,
                                                           ServiceTitle = productLanguage.DisplayName,
                                                           ServiceDescription = serviceLanguage.Content,
                                                           Tags = (from many in _many_Product_TagService.GetAll(x => x.ProductID == product.Id)
                                                                   join tagLanguage in _tagLanguageService.GetAll(x => x.LangaugeID == LanguageID)
                                                                   on many.TagID equals tagLanguage.TagID
                                                                   select new TagDto()
                                                                   {
                                                                       ID = tagLanguage.TagID,
                                                                       Name = tagLanguage.DisplayName
                                                                   }).ToList(),
                                                           Images = (from image in _productImageService.GetAll(x => x.ProductID == product.Id)
                                                                     select new ProductImageDto()
                                                                     {
                                                                         ImageID = image.Id,
                                                                         ImagePath = image.ImagePath,
                                                                         Order = image.Order
                                                                     }).ToList(),
                                                           FindUsOptions = (from option in _systemOptionService.GetAll(x => !x.IsDeleted && x.SystemOptionCategoryID == 5)
                                                                            join optionLanguage in _systemOptionLanguageService.GetAll(x => x.LanguageID == LanguageID)
                                                                            on option.Id equals optionLanguage.SystemOptionId
                                                                            select new SelectListOptionDto()
                                                                            {
                                                                                OptionID = option.Id,
                                                                                Option = optionLanguage.Name
                                                                            }).ToList(),
                                                           TripadvisorComments = new List<TripadvisorCommentDto>(),
                                                           AdditionalService = GetServiceAdditionals(product.Id, LanguageID),


                                                       }).First();

            return getServiceDetailDto;
        }


        // bir servise ait tüm addtionalları çeken arkadaş // -- GetServiceAdditionals
        public List<AdditionalServiceDto> GetServiceAdditionals(Guid ProductId, int LanguageID)
        {
            var serviceAdditonalList = (from many in _many_Product_AdditionalServiceService.GetAll(x => x.ProductID == ProductId)
                                        join additionalService in _additionalServiceService.GetAll()
                                        on many.AdditionalServiceID equals additionalService.Id
                                        join additionalServiceLanguage in _additionalServiceLanguageService.GetAll(x => x.LanguageID == LanguageID)
                                        on additionalService.Id equals additionalServiceLanguage.AdditionalServiceID
                                        select new AdditionalServiceDto()
                                        {
                                            AdditionalServiceID = additionalService.Id,
                                            AdditionalServiceName = additionalServiceLanguage.DisplayName,
                                            IsPerDay = additionalService.IsPerDay,
                                            IsPerPerson = additionalService.IsPerPerson,
                                            Isrequired = many.IsRequired,
                                            Options = (from manyOption in _many_Product_AdditionalServiceOptionService.GetAll(x => x.Many_Product_AdditionalServiceID == many.Id)
                                                       join option in _additionalServiceOptionService.GetAll()
                                                       on manyOption.AdditionalServiceOptionID equals option.Id
                                                       join optionLanguage in _additionalServiceOptionLanguageService.GetAll(x => x.LanguageID == LanguageID)
                                                       on option.Id equals optionLanguage.AdditionalServiceOptionID
                                                       where option.Status && !option.IsDeleted
                                                       select new AdditionalServiceOptionDto()
                                                       {
                                                           OptionID = option.Id,
                                                           OptionName = optionLanguage.DisplayName,
                                                           OptionPrice = 0, // sonradan hesaplanacak
                                                           Inputs = (from manyInput in _many_AdditionalServiceOption_AdditionalServiceInputService.GetAll(x => x.AdditionalServiceOptionID == option.Id)
                                                                     join input in _additionalServiceInputService.GetAll()
                                                                     on manyInput.AdditionalServiceInputID equals input.Id
                                                                     join inputLanguage in _additionalServiceInputLanguageService.GetAll(x => x.LanguageID == LanguageID)
                                                                     on input.Id equals inputLanguage.AdditionalServiceInputID
                                                                     select new AdditionalServiceOptionInputDto()
                                                                     {
                                                                         InputID = input.Id,
                                                                         InputType = input.InputTypeID,
                                                                         Order = input.Order,
                                                                         InputName = inputLanguage.DisplayName
                                                                     }).ToList()
                                                       }).ToList()
                                        }).ToList();

            return serviceAdditonalList;
        }



        //ilgili productın tüm additionallarını çekiyor
        //onların min pricelarını hesaplayıp
        //min olarak toplama işlemi yapıp döner
        //List<AdditionalServiceDto>
        public decimal ServiceAdditionalsMinPriceForList(Guid ProductId, int LanguageID) //GetServiceAdditionals(Guid ProductId, int LanguageID)
        {

            //tüm additionallerı çektim
            var additionallist = GetServiceAdditionals(ProductId, LanguageID);

            //addtionalların pricelarını hesaplayalım
            additionallist = _additionalServiceOptionPriceService.SetOptionalPricesForTourDetail(additionallist);

            /*
            var minPrices = additionallist
                            .Where(service => service.Isrequired)
                            .SelectMany(service => service.Options)
                            .Select(option => option.OptionPrice);

            // En düşük fiyatları toplamak için Sum fonksiyonunu kullanın.
            decimal totalPrice = minPrices.Sum();
            */

            decimal totalPrice = 0;
            foreach (var service in additionallist.Where(s => s.Isrequired))
            {

                //decimal minimumOptionPrice = service.Options.Where(o => o.OptionAllPrices != null && o.OptionAllPrices.Any()).Min(o => o.OptionAllPrices.Min(p => p.Price));

                decimal minimumOptionPrice = service.Options.Min(o => o.OptionPrice);

                totalPrice += minimumOptionPrice;
            }

            //gelen fiyatları topla çıkar çarp böl yapıp liste için fiyat göstereceğim
            //foreach (var additional in additionallist)
            //{
            //    foreach (var opt in additional.Options)
            //    {

            //        serviceTotalPrice += opt.OptionPrice;
            //    }

            //}




            return totalPrice;
        }
    }
}
