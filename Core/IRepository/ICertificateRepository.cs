using Core.Entities;
using Dto.ApiPanelDtos.CertificateManagementDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface ICertificateRepository : IGenericRepository<Certificate>
    {
        List<CertificateListDto> GetCertificateListDtos();
        void AddCertificate(AddCertificateDto addCertificateDto);
    }
}
