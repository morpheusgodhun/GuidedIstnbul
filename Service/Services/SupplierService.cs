using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SupplierService : GenericService<Supplier>, ISupplierService
    {
        public SupplierService(IGenericRepository<Supplier> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public List<SelectListOptionDto> GetSupplierSelectList()
        {
            var supplierSelectList = _repository.GetAll().Select(x => new SelectListOptionDto
            {
                Option = x.Name,
                OptionID = x.Id
            }).ToList();
            return supplierSelectList;
        }
    }
}
