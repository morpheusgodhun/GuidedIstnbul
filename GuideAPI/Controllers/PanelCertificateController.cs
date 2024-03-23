using Core.Entities;
using Core.IService;
using Dto.ApiPanelDtos.CertificateManagementDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelCertificateController : ControllerBase
    {
        private readonly ICertificateService _certificateService;

        public PanelCertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [HttpGet]
        public CustomResponseDto<List<CertificateListDto>> CertificateList()
        {

            List<CertificateListDto> value = _certificateService.GetCertificateListDtos();
            return CustomResponseDto<List<CertificateListDto>>.Success(200, value);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto DeleteCertificate(Guid id)
        {
            var certificate = _certificateService.GetById(id);
            certificate.IsDeleted = true;
            _certificateService.Update(certificate);

            return CustomResponseNullDto.Success(205);
        }


        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleCertificateStatus(Guid id)
        {
            _certificateService.ToggleStatus(id);
            return CustomResponseNullDto.Success(204);
        }

        [HttpPost]
        public CustomResponseNullDto AddCertificate(AddCertificateDto addCertificateDto) 
        {

            _certificateService.AddCertificate(addCertificateDto);
            return CustomResponseNullDto.Success(200);
        }


        [HttpGet("{id}/{order}")]
        public CustomResponseNullDto OrderUpCertificate(Guid id , int order)
        {
            var certificate = _certificateService.GetById(id);
            certificate.Order = order;
            _certificateService.Update(certificate);

            return CustomResponseNullDto.Success(205);
        }
    }
}
