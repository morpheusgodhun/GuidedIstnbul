using Dto.ApiPanelDtos.CommentDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.CommentManagement)]
    public class CommentController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;
        public CommentController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload) : base(apiHandler, configuration)
        {
            _fileUpload = fileUpload;
        }

        public IActionResult Index()
        {
            string url = _url + "PanelComment/CommentList";
            CustomResponseDto<List<CommentListDto>> commentListDto = _apiHandler.GetApi<CustomResponseDto<List<CommentListDto>>>(url);

            if (commentListDto is null)
            {
                return View();
            }
            else
            {
                return View(commentListDto.Data);
            }
        }

        [HttpGet]
        public IActionResult AddComment()
        {
            string countryUrl = _url + "PanelOtherOption/CountrySelectList";
            CustomResponseDto<List<SelectListOption>> countrySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(countryUrl);

            ViewBag.CountryList = countrySelectList.Data;

            return View();
        }

        public IActionResult AddComment(AddCommentDto addCommentDto, IFormFile Image)
        {
            if (Image != null)
            {
                addCommentDto.ImagePath = _fileUpload.FileUploads(Image);
            }
            string url = _url + "PanelComment/AddComment";
            _apiHandler.PostApi<CustomResponseNullDto>(addCommentDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditComment(string id)
        {
            string url = _url + "PanelComment/EditComment/" + id;
            CustomResponseDto<EditCommentDto> editCommentDto = _apiHandler.GetApi<CustomResponseDto<EditCommentDto>>(url);

            string countryUrl = _url + "PanelOtherOption/CountrySelectList";
            CustomResponseDto<List<SelectListOption>> countrySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(countryUrl);

            ViewBag.CountryList = countrySelectList.Data;

            if (editCommentDto is null)
            {
                return View();
            }
            else
            {
                return View(editCommentDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditComment(EditCommentDto editCommentDto, IFormFile Image)
        {
            if (Image != null)
            {
                editCommentDto.ImagePath = _fileUpload.FileUploads(Image);
            }
            string url = _url + "PanelComment/EditComment";
            _apiHandler.PostApi<CustomResponseNullDto>(editCommentDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleCommentStatus(string id)
        {
            string url = _url + "PanelComment/ToggleCommentStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}
