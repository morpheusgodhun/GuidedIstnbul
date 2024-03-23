using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using Dto.ApiPanelDtos.ProductDtos.ServiceDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.ServiceManagement)]

    public class ServiceController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;
        public ServiceController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload) : base(apiHandler, configuration)
        {
            _fileUpload = fileUpload;
        }

        public IActionResult Index()
        {
            string url = _url + "PanelService/ServiceList";
            CustomResponseDto<List<ServiceListDto>> serviceListDto = _apiHandler.GetApi<CustomResponseDto<List<ServiceListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);

            if (serviceListDto is null)
            {
                return View();
            }
            else
            {
                var model = (serviceListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        public IActionResult ToggleServiceStatus(string id)
        {
            string url = _url + "PanelProduct/ToggleProductStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleShowOnHomepage(string id)
        {
            string url = _url + "PanelProduct/ToggleShowOnHomepage/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditService(string id)
        {
            string url = _url + "PanelService/EditService/" + id;
            CustomResponseDto<EditServiceDto> editServiceDto = _apiHandler.GetApi<CustomResponseDto<EditServiceDto>>(url);

            string cancellationUrl = _url + "PanelCancellationPolicy/CancellationPolicySelectList";
            CustomResponseDto<List<SelectListOptionDto>> cancellationPolicySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(cancellationUrl);

            string tagUrl = _url + "PanelTag/TagSelectList";
            CustomResponseDto<List<SelectListOptionDto>> tagSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tagUrl);

            ViewBag.CancellationPolicySelectList = cancellationPolicySelectList.Data;
            ViewBag.TagSelectList = tagSelectList.Data;
            string url5 = _url + "PanelProduct/GetProductName/" + id;
            CustomResponseDto<string> productNameResponse = _apiHandler.GetApi<CustomResponseDto<string>>(url5);

            ViewBag.ProductName = productNameResponse.Data;

            if (editServiceDto is null)
            {
                return View();
            }
            else
            {
                return View(editServiceDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditService(EditServiceDto editServiceDto, IFormFile BannerImage, IFormFile CardImage)
        {
            if (CardImage != null)
            {
                editServiceDto.CardImagePath = _fileUpload.FileUploads(CardImage);
            }
            if (BannerImage != null)
            {
                editServiceDto.BannerImagePath = _fileUpload.FileUploads(BannerImage);
            }
            string url = _url + "PanelService/EditService";
            _apiHandler.PostApi<CustomResponseNullDto>(editServiceDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditService(string id, string languageId)
        {
            string url = _url + "PanelService/LanguageEditService/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditServiceDto> languageEditServiceDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditServiceDto>>(url);

            if (languageEditServiceDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditServiceDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditService(LanguageEditServiceDto languageEditServiceDto)
        {
            string url = _url + "PanelService/LanguageEditService";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditServiceDto, url);
            return RedirectToAction("Index");
        }

        //General Info

        [HttpGet]
        public IActionResult ServiceDetail(string id)
        {
            ViewBag.ProductID = id;
            string url = _url + "PanelService/ServiceDetail/" + id;
            CustomResponseDto<ServiceDetailDto> serviceDetailDto = _apiHandler.GetApi<CustomResponseDto<ServiceDetailDto>>(url);

            string url2 = _url + "PanelOtherOption/CustomerTypeSelectList";
            CustomResponseDto<List<SelectListOptionDto>> customerTypeSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url2);

            ViewBag.CustomerTypeSelectList = customerTypeSelectList.Data;

            string url1 = _url + "PanelProduct/GetProductName/" + id;
            CustomResponseDto<string> productNameResponse = _apiHandler.GetApi<CustomResponseDto<string>>(url1);

            ViewBag.ProductName = productNameResponse.Data;

            if (serviceDetailDto is null)
            {
                return View();
            }
            else
            {
                return View(serviceDetailDto.Data);
            }
        }

        public IActionResult PostService(PostProductDto postProductDto)
        {
            string url = _url + "PanelProduct/PostProduct";
            _apiHandler.PostApi<CustomResponseNullDto>(postProductDto, url);
            return RedirectToAction("ServiceDetail", new { id = postProductDto.ProductID });
        }

        public IActionResult CloneService(CloneProductDto cloneProductDto)
        {
            string url = _url + "PanelProduct/CloneProduct";
            _apiHandler.PostApi<CustomResponseNullDto>(cloneProductDto, url);
            return RedirectToAction("ServiceDetail", new { id = cloneProductDto.ProductID });
        }

        //General Info End

        //Image

        [HttpGet]
        public IActionResult ServiceDetailImage(string id)
        {
            ViewBag.ProductID = id;
            string url = _url + "PanelProduct/ProductImageList/" + id;
            CustomResponseDto<List<ProductImageListDto>> productImageListDtos = _apiHandler.GetApi<CustomResponseDto<List<ProductImageListDto>>>(url);

            string url1 = _url + "PanelProduct/GetProductName/" + id;
            CustomResponseDto<string> productNameResponse = _apiHandler.GetApi<CustomResponseDto<string>>(url1);

            ViewBag.ProductName = productNameResponse.Data;

            if (productImageListDtos is null)
            {
                return View();
            }
            else
            {
                return View(productImageListDtos.Data);
            }
        }
        public IActionResult AddServiceImage(AddProductImageDto addProductImageDto, IFormFile Image)
        {
            if (Image != null)
            {
                addProductImageDto.ImagePath = _fileUpload.FileUploads(Image);
            }
            string url = _url + "PanelProduct/AddProductImage";
            _apiHandler.PostApi<CustomResponseNullDto>(addProductImageDto, url);
            return RedirectToAction("ServiceDetailImage", new { id = addProductImageDto.ProductID });
        }

        public PartialViewResult _EditServiceImagePartial(string id)
        {

            string url = _url + "PanelProduct/EditProductImage/" + id;
            CustomResponseDto<EditProductImageDto> editProductImageDto = _apiHandler.GetApi<CustomResponseDto<EditProductImageDto>>(url);

            if (editProductImageDto is null)
            {
                return PartialView();
            }
            else
            {
                return PartialView(editProductImageDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditServiceImage(EditProductImageDto editProductImageDto, IFormFile Image)
        {
            if (Image != null)
            {
                editProductImageDto.ImagePath = _fileUpload.FileUploads(Image);
            }
            string url = _url + "PanelProduct/EditProductImage";
            _apiHandler.PostApi<CustomResponseNullDto>(editProductImageDto, url);
            return RedirectToAction("ServiceDetailImage", new { id = editProductImageDto.ProductID });
        }



        [Route("[controller]/[action]/{id}/{productId}")]
        public IActionResult ToggleProductImageStatus(string id, string productId)
        {
            string url = _url + "PanelProduct/ToggleProductImageStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("ServiceDetailImage", new { id = productId });
        }

        //End  Image 

        // Additional Service

        [HttpGet]
        public IActionResult ServiceDetailAdditionalService(string id)
        {
            ViewBag.ProductID = id;
            string url = _url + "PanelProduct/AdditionalServiceList/" + id;
            CustomResponseDto<List<ProductAdditionalServiceListDto>> productAdditionalServiceListDto = _apiHandler.GetApi<CustomResponseDto<List<ProductAdditionalServiceListDto>>>(url);

            string additionalServiceUrl = _url + "PanelAdditionalService/AdditionalServiceSelectList";
            CustomResponseDto<List<SelectListOptionDto>> additionalServiceSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(additionalServiceUrl);

            string url1 = _url + "PanelProduct/GetProductName/" + id;
            CustomResponseDto<string> productNameResponse = _apiHandler.GetApi<CustomResponseDto<string>>(url1);

            ViewBag.ProductName = productNameResponse.Data;

            ViewBag.AdditionalServiceSelectList = additionalServiceSelectList.Data;
            if (productAdditionalServiceListDto is null)
            {
                return View();
            }
            else
            {
                return View(productAdditionalServiceListDto.Data);
            }
        }

        [HttpPost]
        public IActionResult AddProductAdditionalService(AddProductAdditionalServiceDto addProductAdditionalServiceDto)
        {
            string url = _url + "PanelProduct/AddProductAdditionalService";
            _apiHandler.PostApi<CustomResponseNullDto>(addProductAdditionalServiceDto, url);
            return RedirectToAction("ServiceDetailAdditionalService", new { id = addProductAdditionalServiceDto.ProductID });
        }
        public PartialViewResult _EditServiceAdditionalServicePartial(string id)
        {

            string url = _url + "PanelProduct/EditProductAdditionalService/" + id;
            CustomResponseDto<EditProductAdditionalServiceDto> editProductAdditionalServiceDto = _apiHandler.GetApi<CustomResponseDto<EditProductAdditionalServiceDto>>(url);


            string optionUrl = _url + "PanelAdditionalService/AdditionalServiceOptionSelectList/" + editProductAdditionalServiceDto.Data.AdditionalServiceID;
            CustomResponseDto<List<SelectListOptionDto>> optionSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(optionUrl);

            ViewBag.OptionSelectList = optionSelectList.Data;

            if (editProductAdditionalServiceDto is null)
            {
                return PartialView();
            }
            else
            {
                return PartialView(editProductAdditionalServiceDto.Data);
            }
        }
        [HttpPost]
        public IActionResult EditProductAdditionalService(EditProductAdditionalServiceDto editProductAdditionalServiceDto)
        {
            string url = _url + "PanelProduct/EditProductAdditionalService";
            CustomResponseDto<Guid> ProductID = _apiHandler.PostApi<CustomResponseDto<Guid>>(editProductAdditionalServiceDto, url);
            return RedirectToAction("ServiceDetailAdditionalService", new { id = ProductID.Data });
        }

        [Route("[controller]/[action]/{id}/{productId}")]
        public IActionResult ToggleProductAdditionalServiceStatus(string id, string productId)
        {
            string url = _url + "PanelProduct/ToggleProductAdditionalServiceStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("ServiceDetailAdditionalService", new { id = productId });
        }

        // End Additional Service

        //Blog
        [HttpGet]
        public IActionResult ServiceDetailBlog(string id)
        {
            ViewBag.ProductID = id;
            string url = _url + "PanelProduct/ProductBlogList/" + id;
            CustomResponseDto<List<ProductBlogListDto>> productBlogListDto = _apiHandler.GetApi<CustomResponseDto<List<ProductBlogListDto>>>(url);

            string blogUrl = _url + "PanelBlog/BlogSelectList";
            CustomResponseDto<List<SelectListOptionDto>> blogSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(blogUrl);

            ViewBag.BlogSelectList = blogSelectList.Data;

            string url1 = _url + "PanelProduct/GetProductName/" + id;
            CustomResponseDto<string> productNameResponse = _apiHandler.GetApi<CustomResponseDto<string>>(url1);

            ViewBag.ProductName = productNameResponse.Data;

            if (productBlogListDto is null)
            {
                return View();
            }
            else
            {
                return View(productBlogListDto.Data);
            }
        }
        [HttpPost]
        public IActionResult AddServiceBlog(AddProductBlogDto addProductBlogDto)
        {
            string url = _url + "PanelProduct/AddProductBlog";
            _apiHandler.PostApi<CustomResponseNullDto>(addProductBlogDto, url);
            return RedirectToAction("ServiceDetailBlog", new { id = addProductBlogDto.ProductID });
        }

        [Route("[controller]/[action]/{id}/{productId}")]
        public IActionResult DeleteServiceBlog(string id, string productId)
        {
            string url = _url + "PanelProduct/DeleteProductBlog/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("ServiceDetailBlog", new { id = productId });
        }

        //End Blog
    }
}
