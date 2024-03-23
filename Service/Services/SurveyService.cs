using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;


namespace Service.Services
{
    public class SurveyService : GenericService<Survey>, ISurveyService
    {
        public SurveyService(IGenericRepository<Survey> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
