using Core.Entities;
using Core.IRepository;
using Core.IService;
using Dto.ApiPanelDtos.CertificateManagementDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CertificateRepository : GenericRepository<Certificate>, ICertificateRepository
    {
        Context _context;
        DbSet<Certificate> _certificates;

        public CertificateRepository(Context context) : base(context)
        {
            _context = context;
            _certificates = _context.Certificates;
        }

        public void AddCertificate(AddCertificateDto addCertificateDto)
        {
            Certificate certificate = new Certificate()
            {
                ImagePath = addCertificateDto.ImagePath,
                Title = addCertificateDto.Title,
                Order = addCertificateDto.Order
            };

            _certificates.Add(certificate);
        }

        public List<CertificateListDto> GetCertificateListDtos()
        {
            List<CertificateListDto> certificateListDtos = (from certificate in _certificates
                                                            where certificate.IsDeleted == false
                                                            select new CertificateListDto()
                                                            {
                                                                CertificateID = certificate.Id,
                                                                ImagePath = certificate.ImagePath,
                                                                Status = certificate.Status,
                                                                Title = certificate.Title,
                                                                Order = certificate.Order
                                                            }).ToList();
            return certificateListDtos;
        }
    }
}
