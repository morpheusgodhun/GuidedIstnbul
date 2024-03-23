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
    public class Many_Product_RoleTemplateService : GenericService<Many_Product_RoleTemplate>, IMany_Product_RoleTemplateService
    {
        public Many_Product_RoleTemplateService(IGenericRepository<Many_Product_RoleTemplate> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public async Task<List<Guid>> GetAttachedRolesByProductIdAsync(Guid productId)
        {
            var data = await _repository.GetWhereAsync(x => x.ProductId == productId);
            return data.Select(x => x.RoleTemplateId).ToList();
        }
    }
}
