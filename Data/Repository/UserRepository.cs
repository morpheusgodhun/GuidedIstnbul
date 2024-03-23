using Core.Entities;
using Core.IRepository;
using Core.StaticValues;
using Dto.ApiWebDtos.WebToApiDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        DbSet<User> _users;
        public UserRepository(Context context) : base(context)
        {
            _users = _context.Users;
        }

        public User GetUserByEmail(string email)
        {
            var query = from user in _users
                        join roleTemplate in _context.RoleTemplates on user.RoleTemplateId equals roleTemplate.Id
                        where user.Email == email && user.RoleTemplateId != ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Customer ).OptionID
                        select new User
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Surname = user.Surname,
                            RoleTemplate = roleTemplate,
                            RoleTemplateId = roleTemplate.Id,
                            IsDeleted = user.IsDeleted,
                            PasswordHash = user.PasswordHash,
                            PasswordSalt = user.PasswordSalt,
                            Status = user.Status,
                            Phone = user.Phone,
                            Email = user.Email,
                            AgentId = user.AgentId,
                            GuideId = user.GuideId,
                        };
            return query.FirstOrDefault();
        }

        public List<User> GetUserByRoleTemplate(Guid roleTemplateId)
        {
            var query = from user in _users
                        join roleTemplate in _context.RoleTemplates on user.RoleTemplateId equals roleTemplate.Id
                        where user.RoleTemplateId == roleTemplateId
                        select new User
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Surname = user.Surname,
                            RoleTemplate = roleTemplate,
                            RoleTemplateId = roleTemplate.Id,
                            IsDeleted = user.IsDeleted,
                            Status = user.Status,
                            Phone = user.Phone,
                            Email = user.Email,
                        };
            return query.ToList();
        }

        public List<User> GetUserList()
        {
            var query = from user in _users
                        join roleTemplate in _context.RoleTemplates on user.RoleTemplateId equals roleTemplate.Id
                        select new User
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Surname = user.Surname,
                            RoleTemplate = roleTemplate,
                            RoleTemplateId = roleTemplate.Id,
                            IsDeleted = user.IsDeleted,
                            Status = user.Status,
                            Phone = user.Phone,
                            Email = user.Email,
                        };
            return query.ToList();
        }

    }
}
