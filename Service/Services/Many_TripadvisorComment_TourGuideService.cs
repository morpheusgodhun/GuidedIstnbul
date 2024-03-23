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
    internal class Many_TripadvisorComment_TourGuideService : GenericService<Many_TripadvisorComment_TourGuide>, IMany_TripadvisorComment_TourGuideService
    {
        public Many_TripadvisorComment_TourGuideService(IGenericRepository<Many_TripadvisorComment_TourGuide> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
