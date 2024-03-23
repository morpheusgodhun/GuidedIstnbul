using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AdditionalServiceInputLanguageService : GenericService<AdditionalServiceInputLanguageItem>, IAdditionalServiceInputLanguageService
    {
        public AdditionalServiceInputLanguageService(IGenericRepository<AdditionalServiceInputLanguageItem> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
