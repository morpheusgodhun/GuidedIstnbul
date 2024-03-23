using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.CertificateManagementDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CertificateService : GenericService<Certificate>, ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;

        public CertificateService(IGenericRepository<Certificate> repository, IUnitOfWork unitOfWork, ICertificateRepository certificateRepository) : base(repository, unitOfWork)
        {
            _certificateRepository = certificateRepository;
        }

        public void AddCertificate(AddCertificateDto addCertificateDto)
        {
            _certificateRepository.AddCertificate(addCertificateDto);
            _unitOfWork.saveChanges();
        }

        public List<CertificateListDto> GetCertificateListDtos()
        {
            return _certificateRepository.GetCertificateListDtos();
        }
    }
}
