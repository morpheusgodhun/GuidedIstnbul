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
    public class FaqService : GenericService<Faq>, IFaqService
    {
        private readonly IFaqRepository _faqRepository;
        public FaqService(IGenericRepository<Faq> repository, IUnitOfWork unitOfWork, IFaqRepository faqRepository) : base(repository, unitOfWork)
        {
            _faqRepository = faqRepository;
        }

    }
}
