using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;


namespace Service.Services
{
    public class Many_AdditionalServiceOption_AdditionalServiceInputService : GenericService<Many_AdditionalServiceOption_AdditionalServiceInput>, IMany_AdditionalServiceOption_AdditionalServiceInputService
    {
        public Many_AdditionalServiceOption_AdditionalServiceInputService(IGenericRepository<Many_AdditionalServiceOption_AdditionalServiceInput> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
