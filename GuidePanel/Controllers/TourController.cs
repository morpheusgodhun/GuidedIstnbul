using Core.StaticValues;
using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiPanelDtos.PersonPolicyDtos;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using Dto.ApiPanelDtos.ProductDtos.TourDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.TourManagement)]

    public class TourController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;
        public TourController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload) : base(apiHandler, configuration)
        {
            _fileUpload = fileUpload;
        }

        public IActionResult Index()
        {
            string url = _url + "PanelTour/TourList";
            CustomResponseDto<List<TourListDto2>> tourListDto = _apiHandler.GetApi<CustomResponseDto<List<TourListDto2>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (tourListDto is null)
            {
                return View();
            }
            else
            {
                var model = (tourListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult EditTour(string id)
        {
            string agentUrl = _url + "PanelAgent/AgentSelectList";
            CustomResponseDto<List<SelectListOptionDto>> agentSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(agentUrl);

            string cancellationUrl = _url + "PanelCancellationPolicy/CancellationPolicySelectList";
            CustomResponseDto<List<SelectListOptionDto>> cancellationPolicySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(cancellationUrl);

            string tagUrl = _url + "PanelTag/TagSelectList";
            CustomResponseDto<List<SelectListOptionDto>> tagSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tagUrl);

            string additionalServiceUrl = _url + "PanelAdditionalService/AdditionalServiceSelectList";
            CustomResponseDto<List<SelectListOptionDto>> additionalServiceSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(additionalServiceUrl);

            string tourTypeUrl = _url + "PanelSystemOption/TourTypeSelectList";
            CustomResponseDto<List<SelectListOptionDto>> tourTypeSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tourTypeUrl);

            string segmentUrl = _url + "PanelSystemOption/SegmentSelectList";
            CustomResponseDto<List<SelectListOptionDto>> segmentSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(segmentUrl);

            string cityUrl = _url + "PanelOtherOption/CitySelectList";
            CustomResponseDto<List<SelectListOption>> citySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(cityUrl);

            string destinationUrl = _url + "PanelDestination/DestinationSelectList";
            CustomResponseDto<List<SelectListOptionDto>> destinationSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(destinationUrl);

            string tourCategoryUrl = _url + "PanelTourCategory/TourCategorySelectList";
            CustomResponseDto<List<SelectListOptionDto>> tourCategorySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tourCategoryUrl);

            string startTimeUrl = _url + "PanelOtherOption/StartTimeSelectList";
            CustomResponseDto<List<SelectListOption>> startTimeSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(startTimeUrl);

            string inclusionExclusionUrl = _url + "PanelSystemOption/InclusionExclusionSelectList";
            CustomResponseDto<List<SelectListOptionDto>> inclusionExclusionSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(inclusionExclusionUrl);

            string sightToSeeUrl = _url + "PanelSystemOption/SightToSeeSelectList";
            CustomResponseDto<List<SelectListOptionDto>> sightToSeeSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(sightToSeeUrl);

            string url5 = _url + "PanelProduct/GetProductName/" + id;
            CustomResponseDto<string> productNameResponse = _apiHandler.GetApi<CustomResponseDto<string>>(url5);

            ViewBag.ProductName = productNameResponse.Data;

            ViewBag.AgentSelectList = agentSelectList.Data;
            ViewBag.CancellationPolicySelectList = cancellationPolicySelectList.Data;
            ViewBag.TagSelectList = tagSelectList.Data;
            ViewBag.TourTypeSelectList = tourTypeSelectList.Data;
            ViewBag.SegmentSelectList = segmentSelectList.Data;
            ViewBag.CitySelectList = citySelectList.Data;
            ViewBag.DestinationSelectList = destinationSelectList.Data;
            ViewBag.TourCategorySelectList = tourCategorySelectList.Data;
            ViewBag.StartTimeSelectList = startTimeSelectList.Data;
            ViewBag.InclusionExclusionSelectList = inclusionExclusionSelectList.Data;
            ViewBag.SightToSeeSelectList = sightToSeeSelectList.Data;




            string url = _url + "PanelTour/EditTour/" + id;
            CustomResponseDto<EditTourDto> editTourDto = _apiHandler.GetApi<CustomResponseDto<EditTourDto>>(url);
            if (editTourDto is null)
            {
                return View();
            }
            else
            {
                return View(editTourDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditTour(EditTourDto editTourDto, IFormFile BannerImage, IFormFile CardImage)
        {
            if (CardImage != null)
            {
                editTourDto.CardImagePath = _fileUpload.FileUploads(CardImage);
            }
            if (BannerImage != null)
            {
                editTourDto.BannerImagePath = _fileUpload.FileUploads(BannerImage);
            }
            if (editTourDto.TagIDs == null)
            {
                editTourDto.TagIDs = new List<Guid>();
            }
            if (editTourDto.DestinationIDs == null)
            {
                editTourDto.DestinationIDs = new List<Guid>();
            }
            if (editTourDto.TourCategoryIDs == null)
            {
                editTourDto.TourCategoryIDs = new List<Guid>();
            }
            if (editTourDto.StartTimeIDs == null)
            {
                editTourDto.StartTimeIDs = new List<int>();
            }
            if (editTourDto.InclusionIDs == null)
            {
                editTourDto.InclusionIDs = new List<Guid>();
            }
            if (editTourDto.ExclusionIDs == null)
            {
                editTourDto.ExclusionIDs = new List<Guid>();
            }
            if (editTourDto.SightToSeeIDs == null)
            {
                editTourDto.SightToSeeIDs = new List<Guid>();
            }

            //https://localhost:7177/Tour/EditTour/2447c0ec-21e5-44d8-8351-045103d1999e

            var selectableDurations = $"[{string.Join(",", editTourDto.SelectableDurations.Select(x => x.ToString()))}]";
            //editTourDto.SelectableDurations = selectableDurations;
            editTourDto.Duration = editTourDto.SelectableDurations.Max();


            string url = _url + "PanelTour/EditTour";
            _apiHandler.PostApi<CustomResponseNullDto>(editTourDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditTour(string id, string languageId)
        {
            string url = _url + "PanelTour/LanguageEditTour/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditTourDto> languageEditTourDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditTourDto>>(url);
            if (languageEditTourDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditTourDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditTour(LanguageEditTourDto languageEditTourDto)
        {
            string url = _url + "PanelTour/LanguageEditTour";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditTourDto, url);
            return RedirectToAction("Index");
        }

        //Done
        public IActionResult ToggleTourStatus(string id)
        {
            string url = _url + "PanelProduct/ToggleProductStatus/" + id;
            _apiHandler.GetApi<CustomResponseDto<LanguageEditTourFaqDto>>(url);
            return RedirectToAction("Index");
        }
        //Done
        public IActionResult ToggleShowOnHomepage(string id)
        {
            string url = _url + "PanelProduct/ToggleShowOnHomepage/" + id;
            _apiHandler.GetApi<CustomResponseDto<LanguageEditTourFaqDto>>(url);
            return RedirectToAction("Index");
        }

        //Tour Faq

        public IActionResult TourFaqList(string id)
        {
            ViewBag.TourID = id;
            string url = _url + "PanelTour/TourFaqList/" + id;
            CustomResponseDto<List<TourFaqListDto>> tourFaqListDto = _apiHandler.GetApi<CustomResponseDto<List<TourFaqListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (tourFaqListDto is null)
            {
                return View();
            }
            else
            {
                var model = (tourFaqListDto.Data, languageListDto.Data);
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult AddTourFaq(string id)
        {
            ViewBag.TourID = id;

            return View();
        }

        [HttpPost]
        public IActionResult AddTourFaq(AddTourFaqDto addTourFaqDto)
        {
            string url = _url + "PanelTour/AddTourFaq";
            _apiHandler.PostApi<CustomResponseNullDto>(addTourFaqDto, url);
            return RedirectToAction("TourFaqList", new { id = addTourFaqDto.ProductID });
        }

        [HttpGet]
        public IActionResult EditTourFaq(string id)
        {

            string url = _url + "PanelTour/EditTourFaq/" + id;
            CustomResponseDto<EditTourFaqDto> editTourFaqDto = _apiHandler.GetApi<CustomResponseDto<EditTourFaqDto>>(url);
            if (editTourFaqDto is null)
            {
                return View();
            }
            else
            {
                return View(editTourFaqDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditTourFaq(EditTourFaqDto editTourFaqDto)
        {
            string url = _url + "PanelTour/EditTourFaq";
            CustomResponseDto<Guid> productID = _apiHandler.PostApi<CustomResponseDto<Guid>>(editTourFaqDto, url);
            return RedirectToAction("TourFaqList", new { id = productID.Data });
        }

        [HttpGet]
        public IActionResult LanguageEditTourFaq(string id, string languageId)
        {

            string url = _url + "PanelTour/LanguageEditTourFaq/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditTourFaqDto> languageEditTourFaqDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditTourFaqDto>>(url);
            if (languageEditTourFaqDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditTourFaqDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditTourFaq(LanguageEditTourFaqDto languageEditTourFaqDto)
        {
            string url = _url + "PanelTour/LanguageEditTourFaq";
            CustomResponseDto<Guid> productID = _apiHandler.PostApi<CustomResponseDto<Guid>>(languageEditTourFaqDto, url);
            return RedirectToAction("TourFaqList", new { id = productID.Data });
        }

        public IActionResult ToggleTourFaqStatus(string id)
        {
            string url = _url + "PanelTour/ToggleTourFaqStatus/" + id;
            CustomResponseDto<Guid> productID = _apiHandler.GetApi<CustomResponseDto<Guid>>(url);
            return RedirectToAction("TourFaqList", new { id = productID.Data });
        }

        //Tour Faq End

        //Tour Detail

        //Program

        [HttpGet]
        public IActionResult TourLanguageDetail(string id, string languageId)
        {
            string url = _url + "PanelTour/LanguageEditTourProgram/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditTourProgramDto> languageEditTourProgramDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditTourProgramDto>>(url);
            if (languageEditTourProgramDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditTourProgramDto.Data);
            }
        }

        [HttpPost]
        public IActionResult TourLanguageDetail(LanguageEditTourProgramDto languageEditTourProgramDto)
        {
            string url = _url + "PanelTour/LanguageEditTourProgram";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditTourProgramDto, url);
            return RedirectToAction("Index");
        }

        //Program End

        //General Info

        [HttpGet]
        public IActionResult TourDetail(string id)
        {
            ViewBag.ProductID = id;
            string url = _url + "PanelTour/TourDetail/" + id;
            CustomResponseDto<TourDetailDto> tourDetailDto = _apiHandler.GetApi<CustomResponseDto<TourDetailDto>>(url);

            string url2 = _url + "PanelOtherOption/RoleTemplateSelectList";
            CustomResponseDto<List<SelectListOptionDto>> roleTemplateSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url2);


            ViewBag.RoleTemplateSelectList = roleTemplateSelectList.Data;


            if (tourDetailDto is null)
            {
                return View();
            }
            else
            {
                return View(tourDetailDto.Data);
            }
        }

        [HttpPost]
        public IActionResult PostTour(PostProductDto postProductDto)
        {
            string url = _url + "PanelProduct/PostProduct";
            _apiHandler.PostApi<CustomResponseNullDto>(postProductDto, url);
            return RedirectToAction("TourDetail", new { id = postProductDto.ProductID });
        }

        [HttpPost]
        public IActionResult CloneTour(CloneProductDto cloneTourDto)
        {
            string url = _url + "PanelProduct/CloneProduct";
            _apiHandler.PostApi<CustomResponseNullDto>(cloneTourDto, url);
            return RedirectToAction("TourDetail", new { id = cloneTourDto.ProductID });
        }

        //General Info End

        //Image

        [HttpGet]
        public IActionResult TourDetailImage(string id)
        {
            ViewBag.ProductID = id;
            string url = _url + "PanelProduct/TourDetailProductImageList/" + id;
            CustomResponseDto<TourDetailProductImageListDto> productImageListDtos = _apiHandler.GetApi<CustomResponseDto<TourDetailProductImageListDto>>(url);

            if (productImageListDtos is null)
            {
                return View();
            }
            else
            {
                return View(productImageListDtos.Data);
            }
        }
        public IActionResult AddTourImage(AddProductImageDto addProductImageDto, IFormFile Image)
        {
            if (Image != null)
            {
                addProductImageDto.ImagePath = _fileUpload.FileUploads(Image);
            }
            string url = _url + "PanelProduct/AddProductImage";
            _apiHandler.PostApi<CustomResponseNullDto>(addProductImageDto, url);
            return RedirectToAction("TourDetailImage", new { id = addProductImageDto.ProductID });
        }

        public PartialViewResult _EditTourImagePartial(string id)
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
        public IActionResult EditTourImage(EditProductImageDto editProductImageDto, IFormFile Image)
        {
            if (Image != null)
            {
                editProductImageDto.ImagePath = _fileUpload.FileUploads(Image);
            }
            string url = _url + "PanelProduct/EditProductImage";
            _apiHandler.PostApi<CustomResponseNullDto>(editProductImageDto, url);
            return RedirectToAction("TourDetailImage", new { id = editProductImageDto.ProductID });
        }
        [Route("[controller]/[action]/{id}/{productId}")]
        public IActionResult ToggleProductImageStatus(string id, string productId)
        {
            string url = _url + "PanelProduct/ToggleProductImageStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("TourDetailImage", new { id = productId });
        }

        //Image End

        //Pricing

        [HttpGet]
        public IActionResult TourDetailPrice(string id)
        {
            ViewBag.ProductID = id;

            string url = _url + "PanelTour/TourPriceList/" + id;
            CustomResponseDto<List<TourPriceListDto>> tourPriceListDto = _apiHandler.GetApi<CustomResponseDto<List<TourPriceListDto>>>(url);

            string urlTourID = _url + "PanelTour/TourIdByProductId/" + id;
            CustomResponseDto<Guid> tourId = _apiHandler.GetApi<CustomResponseDto<Guid>>(urlTourID);

            ViewBag.TourID = tourId.Data;

            string url2 = _url + "PanelTour/IsTourPerPerson/" + id;
            CustomResponseDto<bool> isPerPerson = _apiHandler.GetApi<CustomResponseDto<bool>>(url2);
            string url3 = _url + "PanelTour/IsAskForPriceActive/" + id;
            CustomResponseDto<bool> isAskForPriceActive = _apiHandler.GetApi<CustomResponseDto<bool>>(url3);

            string url4 = _url + "PanelPersonPolicy/PersonPolicyDtoList";
            CustomResponseDto<List<PersonPolicyDto>> personPolicyList = _apiHandler.GetApi<CustomResponseDto<List<PersonPolicyDto>>>(url4);

            string url5 = _url + "PanelProduct/GetProductName/" + id;
            CustomResponseDto<string> productNameResponse = _apiHandler.GetApi<CustomResponseDto<string>>(url5);

            ViewBag.ProductName = productNameResponse.Data;
            ViewBag.PersonPolicyList = personPolicyList.Data;
            ViewBag.IsPerPerson = isPerPerson.Data;
            ViewBag.IsAskForPriceActive = isAskForPriceActive.Data;

            if (tourPriceListDto is null)
            {
                return View();
            }
            else
            {
                return View(tourPriceListDto.Data);
            }
        }
        [Route("[controller]/[action]/{productId}")]
        public IActionResult ToggleAskForPrice(string productId)
        {
            string url = _url + "PanelTour/ToggleAskForPrice/" + productId;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("TourDetailPrice", new { id = productId });
        }
        [Route("[controller]/[action]/{id}/{productId}")]
        public IActionResult DeleteTourPrice(string id, string productId)
        {
            string url = _url + "PanelTour/DeleteTourPrice/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("TourDetailPrice", new { id = productId });
        }

        [HttpPost]
        public IActionResult AddTourPrice(AddTourPriceDto addTourPriceDto)
        {

            string url = _url + "PanelTour/AddTourPrice";
            CustomResponseDto<Guid> ProductID = _apiHandler.PostApi<CustomResponseDto<Guid>>(addTourPriceDto, url);
            return RedirectToAction("TourDetailPrice", new { id = ProductID.Data });
        }

        //Pricing End

        //Additional Service

        [HttpGet]
        public IActionResult TourDetailAdditionalService(string id)
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
            return RedirectToAction("TourDetailAdditionalService", new { id = addProductAdditionalServiceDto.ProductID });
        }
        public PartialViewResult _EditTourAdditionalServicePartial(string id)
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



            return RedirectToAction("TourDetailAdditionalService", new { id = ProductID.Data });



        }

        [Route("[controller]/[action]/{id}/{productId}")]
        public IActionResult ToggleProductAdditionalServiceStatus(string id, string productId)
        {
            string url = _url + "PanelProduct/ToggleProductAdditionalServiceStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("TourDetailAdditionalService", new { id = productId });
        }

        //Additional Service End

        //Blog

        [HttpGet]
        public IActionResult TourDetailBlog(string id)
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
        public IActionResult AddTourBlog(AddProductBlogDto addProductBlogDto)
        {
            string url = _url + "PanelProduct/AddProductBlog";
            _apiHandler.PostApi<CustomResponseNullDto>(addProductBlogDto, url);
            return RedirectToAction("TourDetailBlog", new { id = addProductBlogDto.ProductID });
        }

        [Route("[controller]/[action]/{id}/{productId}")]
        public IActionResult DeleteTourBlog(string id, string productId)
        {
            string url = _url + "PanelProduct/DeleteProductBlog/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("TourDetailBlog", new { id = productId });
        }

        //Blog End 

        //Sell Limit

        [HttpGet]
        public IActionResult TourDetailSellLimit(string id)
        {
            ViewBag.ProductID = id;
            string url = _url + "PanelTour/SellLimitList/" + id;
            CustomResponseDto<List<TourSellLimitListDto>> tourSellLimitListDtos = _apiHandler.GetApi<CustomResponseDto<List<TourSellLimitListDto>>>(url);

            string url1 = _url + "PanelProduct/GetProductName/" + id;
            CustomResponseDto<string> productNameResponse = _apiHandler.GetApi<CustomResponseDto<string>>(url1);

            ViewBag.ProductName = productNameResponse.Data;

            if (tourSellLimitListDtos is null)
            {
                return View();
            }
            else
            {
                return View(tourSellLimitListDtos.Data);
            }
        }
        [HttpPost]
        public IActionResult AddTourSellLimit(AddTourSellLimitDto addTourSellLimitDto)
        {
            string url = _url + "PanelTour/AddSellLimit";
            _apiHandler.PostApi<CustomResponseNullDto>(addTourSellLimitDto, url);
            return RedirectToAction("TourDetailSellLimit", new { id = addTourSellLimitDto.ProductID });
        }

        [HttpPost]
        public IActionResult EditTourSellLimit(EditTourSellLimitDto editTourSellLimitDto)
        {
            string url = _url + "PanelTour/EditSellLimit";
            CustomResponseDto<Guid> productID = _apiHandler.PostApi<CustomResponseDto<Guid>>(editTourSellLimitDto, url);
            return RedirectToAction("TourDetailSellLimit", new { id = productID.Data });
        }

        public IActionResult DeleteTourSellLimit(string id)
        {
            string url = _url + "PanelTour/DeleteSellLimit/" + id;
            CustomResponseDto<Guid> productID = _apiHandler.GetApi<CustomResponseDto<Guid>>(url);
            return RedirectToAction("TourDetailSellLimit", new { id = productID.Data });
        }

        //Sell Limit End

        //Tour Detail End


        [Route("[controller]/[action]/{id}/")]
        public IActionResult Delete(string id)
        {
            string url = _url + "PanelTour/Delete/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }

    }
}
