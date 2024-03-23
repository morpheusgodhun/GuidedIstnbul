using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IMany_Product_RoleTemplateService : IGenericService<Many_Product_RoleTemplate>
    {
        Task<List<Guid>> GetAttachedRolesByProductIdAsync(Guid productId);
    }
}
