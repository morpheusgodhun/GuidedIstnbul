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
    public class SurveyLanguageService : GenericService<SurveyLanguageItem>, ISurveyLanguageService
    {
        private readonly ISurveyLanguageRepository _surveyLanguageRepository;
        public SurveyLanguageService(IGenericRepository<SurveyLanguageItem> repository, IUnitOfWork unitOfWork, ISurveyLanguageRepository surveyLanguageRepository) : base(repository, unitOfWork)
        {
            _surveyLanguageRepository = surveyLanguageRepository;
        }

        public SurveyLanguageItem GetBySurveyId(Guid surveyId)
        {
            SurveyLanguageItem surveyLanguageItem = _surveyLanguageRepository.Where(x => x.SurveyID == surveyId).FirstOrDefault();
            return surveyLanguageItem;
        }
    }
}
