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
    public class PaymentMethodService : GenericService<PaymentMethod>, IPaymentMethodService
    {
        public PaymentMethodService(IGenericRepository<PaymentMethod> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
