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
    public class FaqCategoryService : GenericService<FaqCategory>, IFaqCategoryService
    {
        readonly IPageRepository _pageRepository;
        readonly IFaqCategoryLanguageRepository _faqCategoryLanguageRepository;
        readonly IFaqCategoryRepository _faqCategoryRepository;
        public FaqCategoryService(IGenericRepository<FaqCategory> repository, IUnitOfWork unitOfWork, IFaqCategoryRepository faqCategoryRepository, IFaqCategoryLanguageRepository faqCategoryLanguageRepository, IPageRepository pageRepository) : base(repository, unitOfWork)
        {
            _faqCategoryRepository = faqCategoryRepository;
            _faqCategoryLanguageRepository = faqCategoryLanguageRepository;
            _pageRepository = pageRepository;
        }

        public string GetPageFaqCategorySlug(string pageId, int languageId)
        {
            return _faqCategoryLanguageRepository.GetPageFaqCategorySlug(new Guid(pageId), languageId);
        }
    }
}
