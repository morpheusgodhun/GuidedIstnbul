using Core.Entities;
using Core.IRepository;
using Dto.ApiPanelDtos.RoleDtos;
using Dto.ApiWebDtos.GeneralDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
    {
        Context _context;
        DbSet<RolePermission> _roles;
        DbSet<RoleTemplate> _templates;
        DbSet<Many_Role_RoleTemplate> _many;

        public RolePermissionRepository(Context context) : base(context)
        {
            _context = context;
            _roles = _context.RolePermissions;
            _templates = _context.RoleTemplates;
            _many = _context.Many_Role_RoleTemplates;
        }

        public void AddRole(AddRoleDto addRoleDto)
        {
            RoleTemplate roleTemplate = new RoleTemplate()
            {
                Name = addRoleDto.RoleTemplateName
            };
            _templates.Add(roleTemplate);

            foreach (var roleId in addRoleDto.RoleIDs)
            {
                Many_Role_RoleTemplate many = new Many_Role_RoleTemplate()
                {
                    RoleTemplateId = roleTemplate.Id,
                    RolePermissionId = roleId
                };
                _many.Add(many);
            }
        }

        public List<SelectListOptionDto> AllowSelectList()
        {
            List<SelectListOptionDto> allowSelectList = (from role in _roles.ToList()
                                                         where !role.IsDeleted
                                                         //where role.Id != new Guid("57458bff-dece-4a0f-90f4-0921454b78ca")
                                                         //where role.Id != new Guid("01b01966-fbc7-440e-b500-b26e7c220f46")
                                                         select new SelectListOptionDto()
                                                         {
                                                             OptionID = role.Id,
                                                             Option = role.Name
                                                         }).ToList();
            return allowSelectList;
        }

        public void EditRole(EditRoleDto editRoleDto)
        {
            RoleTemplate template = _templates.Find(editRoleDto.RoleTemplateID);
            template.Name = editRoleDto.RoleTemplateName;
            _templates.Update(template);

            List<Many_Role_RoleTemplate> manies = _many.Where(x => x.RoleTemplateId == template.Id).ToList();
            foreach (var many in manies)
            {
                _many.Remove(many);
            }

            foreach (var roleId in editRoleDto.RoleIDs)
            {
                Many_Role_RoleTemplate many = new Many_Role_RoleTemplate()
                {
                    RoleTemplateId = template.Id,
                    RolePermissionId = roleId
                };
                _many.Add(many);
            }
        }

        public EditRoleDto GetEditRole(Guid id)
        {
            EditRoleDto editRoleDto = (from template in _templates.ToList()
                                       where template.Id == id
                                       where !template.IsDeleted && template.Status
                                       //where template.Id == new Guid("f38ce8d4-0a6b-4fca-8da5-fbe5b0390efa")
                                       //where template.Id == new Guid("449b055c-c1e1-4abe-bdf2-a36a4686caf7")
                                       select new EditRoleDto()
                                       {
                                           RoleTemplateID = id,
                                           RoleTemplateName = template.Name,
                                           RoleIDs = (from many in _many.ToList()
                                                      where many.RoleTemplateId == template.Id
                                                      join role in _roles.ToList()
                                                      on many.RolePermissionId equals role.Id
                                                      where !role.IsDeleted
                                                      select role.Id).ToList()
                                       }).FirstOrDefault();
            return editRoleDto;
        }

        public List<RoleListDto> GetRoleListDtos() // aslında bu role çekmiyor rol templateleri seçiyor
        {
            List<RoleListDto> roleList = (from template in _templates.ToList()
                                          where !template.IsDeleted
                                          //where template.Id != new Guid("89f6763a-0852-4e87-ba6e-b6a51ef00c6a")
                                          //where template.Id != new Guid("415cb898-18dd-4fae-93fb-fa7a43a61a12")
                                          select new RoleListDto()
                                          {
                                              RoleTemplateId = template.Id,
                                              Status = template.Status,
                                              RoleTemplateName = template.Name,
                                              Roles = (from many in _many.ToList()
                                                       where many.RoleTemplateId == template.Id
                                                       join role in _roles.ToList()
                                                       on many.RolePermissionId equals role.Id
                                                       where !role.IsDeleted
                                                       select role.Name).ToList()

                                          }).ToList();
            return roleList;
        }

        public List<SelectListOptionDto> RoleSelectList()
        {
            List<SelectListOptionDto> roleSelectList = (from template in _templates.ToList()
                                                        where !template.IsDeleted && template.Status
                                                        //where template.Id != new Guid("89f6763a-0852-4e87-ba6e-b6a51ef00c6a")
                                                        //where template.Id != new Guid("415cb898-18dd-4fae-93fb-fa7a43a61a12")
                                                        select new SelectListOptionDto()
                                                        {
                                                            OptionID = template.Id,
                                                            Option = template.Name
                                                        }).ToList();
            return roleSelectList;
        }


    }
}
