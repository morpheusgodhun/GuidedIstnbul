using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class OfferService : GenericService<Offer>, IOfferService
    {
        public OfferService(IGenericRepository<Offer> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
