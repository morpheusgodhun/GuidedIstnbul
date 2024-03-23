using Dto.ApiPanelDtos.CertificateManagementDtos;
using Dto.ApiPanelDtos.ImageManagementDtos;
using DTO;
using FluentValidation.Results;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.FluentValidation;
using GuidePanel.FluentValidation.Certificate;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.CertificateManagement)]
    public class CertificateController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;
        public CertificateController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload) : base(apiHandler, configuration)
        {
            _fileUpload = fileUpload;
        }


        public IActionResult Index()
        {
            string url = _url + "PanelCertificate/CertificateList";
            CustomResponseDto<List<CertificateListDto>> certificateListDto = _apiHandler.GetApi<CustomResponseDto<List<CertificateListDto>>>(url);


            if (certificateListDto is null)
            {
                return View();
            }
            else
            {
                return View(certificateListDto.Data);
            }
        }


        [HttpGet]
        public IActionResult AddCertificate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCertificate(AddCertificateDto addCertificateDto, IFormFile Image)
        {
            var isValid = FluentValidator<AddCertificateDto>.Validator(new AddCertificateValidator(), addCertificateDto, ModelState);

            if (isValid)
            {
                string url = _url + "PanelCertificate/AddCertificate";
                if (Image != null)
                {
                    addCertificateDto.ImagePath = _fileUpload.FileUploads(Image);
                }
                _apiHandler.PostApi<AddCertificateDto>(addCertificateDto, url);
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult DeleteCertificate(string id)
        {
            string url = _url + "PanelCertificate/DeleteCertificate/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleCertificateStatus(string id)
        {
            string url = _url + "PanelCertificate/ToggleCertificateStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }

        [HttpGet("/Certificate/OrderUpCertificate/{id}/{order}")]
        public IActionResult OrderUpCertificate(string id, int order)
        {
            string url = _url + "PanelCertificate/OrderUpCertificate/" + id + "/" + order;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}
