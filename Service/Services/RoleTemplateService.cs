using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class RoleTemplateService : GenericService<RoleTemplate>, IRoleTemplateService
    {
        private readonly IMany_Role_RoleTemplateRepository _manyRoleService;
        public RoleTemplateService(IGenericRepository<RoleTemplate> repository, IUnitOfWork unitOfWork, IMany_Role_RoleTemplateRepository manyRoleService) : base(repository, unitOfWork)
        {
            _manyRoleService = manyRoleService;
        }

        public string GetRoleTemplateName(string roleTemplateId)
        {
            var roleTemplate = _repository.GetById(new Guid(roleTemplateId));
            return roleTemplate.Name;
        }
        public Dictionary<string, List<string>> GetRoleTemplatePermissions()
        {
            var rolePermissionList = _manyRoleService.GetRoleTemplatePermissions();
            Dictionary<string, List<string>> rolePermissionsDict = new();

            rolePermissionList.ForEach(x => rolePermissionsDict[x.RoleTemplateName] = x.Permissions);
            return rolePermissionsDict;
        }

    }
}
