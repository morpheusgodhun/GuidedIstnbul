using Core.Entities;
using Dto.ApiPanelDtos.CertificateManagementDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface ICertificateService : IGenericService<Certificate>
    {
        List<CertificateListDto> GetCertificateListDtos();
        void AddCertificate(AddCertificateDto addCertificateDto);
    }
}
