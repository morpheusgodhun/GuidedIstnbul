using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.ProductDtos.ServiceDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelServiceController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductLanguageService _productLanguageService;
        private readonly IServiceService _serviceService;
        private readonly IServiceLanguageService _serviceLanguageService;
        private readonly IAdditionalServiceLanguageService _additionalServiceLanguageService;
        private readonly IMany_Product_AdditionalServiceService _many_Product_AdditionalServiceService;
        private readonly IMany_Product_TagService _many_Product_TagService;
        private readonly ICancellationPolicyService _cancellationPolicyService;
        private readonly ITagLanguageService _tagLanguageService;

        public PanelServiceController(IProductService productService, IProductLanguageService productLanguageService, IServiceService serviceService, IServiceLanguageService serviceLanguageService, IAdditionalServiceLanguageService additionalServiceLanguageService, IMany_Product_AdditionalServiceService many_Product_AdditionalServiceService, IMany_Product_TagService many_Product_TagService, ICancellationPolicyService cancellationPolicyService, ITagLanguageService tagLanguageService)
        {
            _productService = productService;
            _productLanguageService = productLanguageService;
            _serviceService = serviceService;
            _serviceLanguageService = serviceLanguageService;
            _additionalServiceLanguageService = additionalServiceLanguageService;
            _many_Product_AdditionalServiceService = many_Product_AdditionalServiceService;
            _many_Product_TagService = many_Product_TagService;
            _cancellationPolicyService = cancellationPolicyService;
            _tagLanguageService = tagLanguageService;
        }

        [HttpGet]
        public CustomResponseDto<List<ServiceListDto>> ServiceList()
        {

            List<ServiceListDto> serviceListDtos = (from product in _productService.GetAll(x => !x.IsDeleted && !x.IsTour)
                                                    join productLanguage in _productLanguageService.GetAll(x => !x.IsDeleted)
                                                    on product.Id equals productLanguage.ProductID
                                                    where productLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                    select new ServiceListDto()
                                                    {
                                                        ProductID = product.Id,
                                                        ProductName = productLanguage.DisplayName,
                                                        ShowOnHomepage = product.ShowOnHomePage,
                                                        Status = product.Status,
                                                        AdditionalServices = (from many in _many_Product_AdditionalServiceService.GetAll(x => x.ProductID == product.Id)
                                                                              join additionalServiceLanguage in _additionalServiceLanguageService.GetAll()
                                                                              on many.AdditionalServiceID equals additionalServiceLanguage.AdditionalServiceID
                                                                              where additionalServiceLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                                              select additionalServiceLanguage.DisplayName).ToList()
                                                    }).ToList();


            return CustomResponseDto<List<ServiceListDto>>.Success(200, serviceListDtos);
        }



        [HttpGet("{id}")]
        public CustomResponseDto<EditServiceDto> EditService(string id)
        {
            EditServiceDto editServiceDto = (from product in _productService.GetAll(x => !x.IsDeleted && !x.IsTour)
                                             join productLanguage in _productLanguageService.GetAll(x => !x.IsDeleted)
                                             on product.Id equals productLanguage.ProductID
                                             where productLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                             select new EditServiceDto()
                                             {
                                                 ProductID = product.Id,
                                                 ProductName = productLanguage.DisplayName,
                                                 YoutubeLink = product.YoutubeLink,
                                                 BannerImagePath = product.BannerImagePath,
                                                 CardImagePath = product.CardImagePath,
                                                 CancellationPolicyID = product.CancellationPolicyID,
                                                 TagIDs = (from many in _many_Product_TagService.Where(x => x.ProductID == product.Id)
                                                           select many.TagID).ToList(),
                                                 CutOfHour = product.CutoffHour % 24,
                                                 CutOfDay = product.CutoffHour / 24,
                                                 CustomerDeposito = product.CustomerDeposito,
                                                 AgentDeposito = product.AgentDeposito,
                                                 DayOfPayment = product.PaymentDate,
                                                 MinimumPayoutPercent = product.MinimumPayoutPercent,
                                                 IsChildPolicyActive = product.IsChildPolicyActive
                                             }).FirstOrDefault();


            return CustomResponseDto<EditServiceDto>.Success(200, editServiceDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditService(EditServiceDto editServiceDto)
        {
            Product product = _productService.GetById(editServiceDto.ProductID);

            product.YoutubeLink = editServiceDto.YoutubeLink;
            product.BannerImagePath = editServiceDto.BannerImagePath;
            product.CardImagePath = editServiceDto.CardImagePath;
            product.CancellationPolicyID = editServiceDto.CancellationPolicyID;
            product.CutoffHour = editServiceDto.CutOfHour + editServiceDto.CutOfDay * 24;
            product.CustomerDeposito = editServiceDto.CustomerDeposito;
            product.AgentDeposito = editServiceDto.AgentDeposito;
            product.PaymentDate = editServiceDto.DayOfPayment;
            product.MinimumPayoutPercent = editServiceDto.MinimumPayoutPercent;
            product.IsChildPolicyActive = editServiceDto.IsChildPolicyActive;

            _productService.Update(product);

            List<Many_Product_Tag> oldTag = _many_Product_TagService.GetAll(x => x.ProductID == product.Id).ToList();

            foreach (var tag in oldTag)
            {
                _many_Product_TagService.Remove(tag);
            }

            foreach (var tagID in editServiceDto.TagIDs)
            {
                Many_Product_Tag many = new Many_Product_Tag()
                {
                    ProductID = product.Id,
                    TagID = tagID
                };
                _many_Product_TagService.Add(many);
            }


            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditServiceDto> LanguageEditService(Guid id, int languageId)
        {

            LanguageEditServiceDto languageEditServiceDto = (from product in _productService.Where(x => x.Id == id)
                                                             join productLanguageItem in _productLanguageService.Where(x => x.ProductID == id)
                                                             on product.Id equals productLanguageItem.ProductID
                                                             where productLanguageItem.LanguageID == languageId
                                                             join serviceLanguageItem in _serviceLanguageService.GetAll()
                                                             on product.ServiceID equals serviceLanguageItem.ServiceID
                                                             where serviceLanguageItem.LanguageID == languageId
                                                             select new LanguageEditServiceDto()
                                                             {
                                                                 ProductLanguageID = productLanguageItem.Id,
                                                                 ServiceLanguageID = serviceLanguageItem.Id,
                                                                 ProductName = product.ProductName,
                                                                 DisplayName = productLanguageItem.DisplayName,
                                                                 Slug = productLanguageItem.Slug,
                                                                 Content = serviceLanguageItem.Content,
                                                                 ShortDescription = serviceLanguageItem.ShortDescription,
                                                                 LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name

                                                             }).FirstOrDefault();


            return CustomResponseDto<LanguageEditServiceDto>.Success(200, languageEditServiceDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditService(LanguageEditServiceDto languageEditServiceDto)
        {

            ProductLanguageItem productLanguageItem = _productLanguageService.GetById(languageEditServiceDto.ProductLanguageID);

            productLanguageItem.DisplayName = languageEditServiceDto.DisplayName;
            productLanguageItem.Slug = languageEditServiceDto.Slug;

            _productLanguageService.Update(productLanguageItem);


            ServiceLanguageItem serviceLanguageItem = _serviceLanguageService.GetById(languageEditServiceDto.ServiceLanguageID);

            serviceLanguageItem.ShortDescription = languageEditServiceDto.ShortDescription;
            serviceLanguageItem.Content = languageEditServiceDto.Content;

            _serviceLanguageService.Update(serviceLanguageItem);


            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<ServiceDetailDto> ServiceDetail(Guid id)
        {

            ServiceDetailDto serviceDetailDto = (from product in _productService.GetAll(x => x.Id == id)
                                                 join productLanguageItem in _productLanguageService.GetAll()
                                                 on product.Id equals productLanguageItem.ProductID
                                                 where productLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                 join serviceLanguageItem in _serviceLanguageService.GetAll(x => !x.IsDeleted)
                                                 on product.ServiceID equals serviceLanguageItem.ServiceID
                                                 where serviceLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                 select new ServiceDetailDto()
                                                 {
                                                     ProductName = product.ProductName,
                                                     DisplayName = productLanguageItem.DisplayName,
                                                     YoutubeLink = product.YoutubeLink,
                                                     Slug = productLanguageItem.Slug,
                                                     ShortDescription = serviceLanguageItem.ShortDescription,
                                                     Content = serviceLanguageItem.Content,
                                                     CancellationPolicy = _cancellationPolicyService.GetById(product.CancellationPolicyID).Name,
                                                     Tags = (from many in _many_Product_TagService.GetAll(x => x.ProductID == product.Id)
                                                             join tagLanguageItem in _tagLanguageService.GetAll()
                                                             on many.TagID equals tagLanguageItem.TagID
                                                             where tagLanguageItem.LangaugeID == LanguageList.BaseLanguage.Id
                                                             select tagLanguageItem.DisplayName).ToList(),
                                                     IsChildPolicyActive = product.IsChildPolicyActive,
                                                     CutOffTime = product.CutoffHour / 7 + " Days  - " + product.CutoffHour % 7 + " Hours"

                                                 }).FirstOrDefault();




            return CustomResponseDto<ServiceDetailDto>.Success(200, serviceDetailDto);
        }
    }
}
