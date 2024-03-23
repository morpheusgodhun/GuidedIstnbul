
using Core.Entities;
using DocumentFormat.OpenXml;
using Dto.ApiPanelDtos.BlogDtos;
using Dto.ApiPanelDtos.FaqManagementDtos;
using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiPanelDtos.TagManagementDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Drawing;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.BlogManagement)]
    public class BlogController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;
        public BlogController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload) : base(apiHandler, configuration)
        {
            _fileUpload = fileUpload;
        }

        public IActionResult Index()
        {
            string url = _url + "PanelBlog/BlogList";
            CustomResponseDto<List<BlogListDto>> blogListDto = _apiHandler.GetApi<CustomResponseDto<List<BlogListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (blogListDto is null)
            {
                return View();
            }
            else
            {
                var model = (blogListDto.Data, languageListDto.Data);
                return View(model);
            }
        }


        public IActionResult BlogCommentList(string id)
        {
            string url = _url + "PanelBlog/BlogCommentList/" + id;
            CustomResponseDto<List<BlogCommentListDto>> blogCommentListDto = _apiHandler.GetApi<CustomResponseDto<List<BlogCommentListDto>>>(url);
            if (blogCommentListDto is null)
            {
                return View();
            }
            else
            {
                return View(blogCommentListDto.Data);
            }
        }

        [Route("[controller]/[action]/{id}/{blogId}")]
        public IActionResult ToggleBlogCommentStatus(string id, string blogId)
        {
            string url = _url + "PanelBlog/ToggleBlogCommentStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("BlogCommentList", new { id = blogId });
        }

        [HttpGet]
        public IActionResult AddBlog()
        {

            string blogCategoryUrl = _url + "PanelBlogCategory/BlogCategorySelectList";
            CustomResponseDto<List<SelectListOptionDto>> blogCategorySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(blogCategoryUrl);

            string tagUrl = _url + "PanelTag/TagSelectListForBlog";
            CustomResponseDto<List<SelectListOptionDto>> tagSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tagUrl);

            string tourUrl = _url + "PanelTour/TourSelectList";
            CustomResponseDto<List<SelectListOptionDto>> tourSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tourUrl);

            ViewBag.BlogCategories = blogCategorySelectList.Data;
            ViewBag.Tags = tagSelectList.Data;
            ViewBag.Tours = tourSelectList.Data;

            return View();
        }

        [HttpPost]
        public IActionResult AddBlog(AddBlogDto addBlogDto, IFormFile CardImage, IFormFile BannerImage)
        {
            if (CardImage != null)
            {
                addBlogDto.CardImagePath = _fileUpload.FileUploads(CardImage);
            }
            if (BannerImage != null)
            {
                addBlogDto.BannerImagePath = _fileUpload.FileUploads(BannerImage);
            }
            if (addBlogDto.RecommendedTourIDs == null)
            {
                addBlogDto.RecommendedTourIDs = new List<Guid>();
            }
            if (addBlogDto.TagIDs == null)
            {
                addBlogDto.TagIDs = new List<Guid>();
            }

            if (string.IsNullOrEmpty(addBlogDto.Content1))
                addBlogDto.Content1 = "";

            if (string.IsNullOrEmpty(addBlogDto.Content2))
                addBlogDto.Content2 = "";

            string url = _url + "PanelBlog/AddBlog";
            _apiHandler.PostApi<CustomResponseNullDto>(addBlogDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditBlog(string id)
        {
            string blogCategoryUrl = _url + "PanelBlogCategory/BlogCategorySelectList";
            CustomResponseDto<List<SelectListOptionDto>> blogCategorySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(blogCategoryUrl);

            string tagUrl = _url + "PanelTag/TagSelectListForBlog";
            CustomResponseDto<List<SelectListOptionDto>> tagSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tagUrl);

            string url = _url + "PanelBlog/EditBlog/" + id;
            CustomResponseDto<EditBlogDto> editBlogDto = _apiHandler.GetApi<CustomResponseDto<EditBlogDto>>(url);

            string tourUrl = _url + "PanelTour/TourSelectList";
            CustomResponseDto<List<SelectListOptionDto>> tourSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(tourUrl);


            ViewBag.BlogCategories = blogCategorySelectList.Data;
            ViewBag.Tags = tagSelectList.Data;
            ViewBag.Tours = tourSelectList.Data;
            return View(editBlogDto.Data);
        }

        [HttpPost]
        public IActionResult EditBlog(EditBlogDto editBlogDto, IFormFile CardImage, IFormFile BannerImage)
        {
            if (CardImage != null)
            {
                editBlogDto.CardImagePath = _fileUpload.FileUploads(CardImage);
            }
            if (BannerImage != null)
            {
                editBlogDto.BannerImagePath = _fileUpload.FileUploads(BannerImage);
            }
            if (editBlogDto.RecommendedTourIDs == null)
            {
                editBlogDto.RecommendedTourIDs = new List<Guid>();
            }
            if (editBlogDto.TagIDs == null)
            {
                editBlogDto.TagIDs = new List<Guid>();
            }

            string url = _url + "PanelBlog/EditBlog";
            _apiHandler.PostApi<CustomResponseNullDto>(editBlogDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditBlog(string id, string languageId)
        {
            string url = _url + "PanelBlog/LanguageEditBlog/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditBlogDto> languageEditBlogDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditBlogDto>>(url);

            return View(languageEditBlogDto.Data);
        }

        [HttpPost]
        public IActionResult LanguageEditBlog(LanguageEditBlogDto languageEditBlogDto)
        {

            //if (string.IsNullOrEmpty(languageEditBlogDto.Content1))
            //    languageEditBlogDto.Content1 = "";

            //if (string.IsNullOrEmpty(languageEditBlogDto.Content2))
            //    languageEditBlogDto.Content2 = "";

            languageEditBlogDto.ShortDescription = !String.IsNullOrEmpty(languageEditBlogDto.ShortDescription) ? languageEditBlogDto.ShortDescription : string.Empty;
            languageEditBlogDto.Content1 = !String.IsNullOrEmpty(languageEditBlogDto.Content1) ? languageEditBlogDto.Content1 : string.Empty;
            languageEditBlogDto.Content2 = !String.IsNullOrEmpty(languageEditBlogDto.Content2) ? languageEditBlogDto.Content2 : string.Empty;
          
            string url = _url + "PanelBlog/LanguageEditBlog";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditBlogDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleBlogStatus(string id)
        {
            string url = _url + "PanelBlog/ToggleBlogStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleShowOnFaq(string id)
        {
            string url = _url + "PanelBlog/ToggleShowOnFaq/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleShowOnHomepage(string id)
        {
            string url = _url + "PanelBlog/ToggleShowOnHomepage/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult UploadContentImage1(IFormFile upload)
        {
            var returnPath = "";
            if (upload != null)
            {
                returnPath = _fileUpload.FileUploads(upload);
                returnPath = Path.Combine(Resource.ImagePath, $"{returnPath}");
            }

            /*
            var response = new
            {
                success = true,
                url = returnPath,
                message = "Dosya başarıyla yüklendi."
            };
            return Ok(response);*/
            /*
            var script = "<script> window.parent.CKEDITOR.tools.callFunction(1, '" + returnPath + "');</script>";
            return Content(script, "text/html");*/
            
            var script = "<script> window.parent.cke1.insertHtml('<img src=\"" + returnPath + "\">');" +
                " window.parent.CKEDITOR.dialog.getCurrent().hide();" +
                "</script>";
            return Content(script, "text/html");
        }

        [HttpPost]
        public IActionResult UploadContentImage2(IFormFile upload)
        {
            var returnPath = "";
            if (upload != null)
            {
                returnPath = _fileUpload.FileUploads(upload);
                returnPath = Path.Combine(Resource.ImagePath, $"{returnPath}");
            }
            /*
            var response = new
            {
                success = true,
                url = returnPath,
                message = "Dosya başarıyla yüklendi."
            };
            return Ok(response);*/

            /*
            var script = "<script> window.parent.CKEDITOR.tools.callFunction(1, '" + returnPath + "');</script>";
            return Content(script, "text/html");*/

            var script = "<script> window.parent.cke2.insertHtml('<img src=\"" + returnPath + "\">');" +
                " window.parent.CKEDITOR.dialog.getCurrent().hide();" +
                "</script>";
            return Content(script, "text/html");
        }

    }
}
