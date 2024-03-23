using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticValues;
using Data.Migrations;
using Dto.ApiPanelDtos.RoleDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class RolePermissionService : GenericService<RolePermission>, IRolePermissionService
    {
        private readonly IRolePermissionRepository _roleRepository;
        public RolePermissionService(IGenericRepository<RolePermission> repository, IUnitOfWork unitOfWork, IRolePermissionRepository roleRepository) : base(repository, unitOfWork)
        {
            _roleRepository = roleRepository;
        }

        public void AddRole(AddRoleDto addRoleDto)
        {
            _roleRepository.AddRole(addRoleDto);
            _unitOfWork.saveChanges();
        }

        public List<SelectListOptionDto> AllowSelectList()
        {

            //tüm rolleri çekip
            var AllRoles = _roleRepository.AllowSelectList();
            var constantRoles = ConstantRoles.SpecialRolesGuid;
            // sabit olan rolleri çıkarıyorum
            AllRoles.RemoveAll(role => constantRoles.Any(template => template.OptionID == role.OptionID)); //RoleId

            return AllRoles;

            //return _roleRepository.AllowSelectList();


        }

        public void EditRole(EditRoleDto editRoleDto)
        {
            _roleRepository.EditRole(editRoleDto);
            _unitOfWork.saveChanges();
        }

        public EditRoleDto GetEditRole(Guid id)
        {
            return _roleRepository.GetEditRole(id);
        }

        public List<RoleListDto> GetRoleListDtos()
        {
            //tüm rolleri templateleri çekip
            var AllRoles = _roleRepository.GetRoleListDtos();
            var constantRoles = ConstantRoles.SpecialRoleTemplatesGuid;
            // sabit olan roller templatelerii çıkarıyorum
            AllRoles.RemoveAll(role => constantRoles.Any(template => template.OptionID == role.RoleTemplateId));

            return AllRoles;
        }

        public List<SelectListOptionDto> RoleSelectList()
        {

            //tüm rolleri templateleri çekip
            var AllRoles = _roleRepository.RoleSelectList();
            var constantRoles = ConstantRoles.SpecialRoleTemplatesGuid;
            // sabit olan roller templatelerii çıkarıyorum
            AllRoles.RemoveAll(role => constantRoles.Any(template => template.OptionID == role.OptionID)); //RoleTemplateId

            return AllRoles;
        }
    }
}
