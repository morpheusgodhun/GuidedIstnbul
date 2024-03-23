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
    public class Many_Blog_RecomendedTourService : GenericService<Many_Blog_RecomendedTour>, IMany_Blog_RecomendedTourService
    {
        public Many_Blog_RecomendedTourService(IGenericRepository<Many_Blog_RecomendedTour> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
