using Core.Entities;
using Core.IService;
using Dto;
using DTO;
using GuidePanel.APIHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GuidePanel.Authorization
{
    public class RolePermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        public RolePermissionHandler()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            bool hasUserPermissions = context.User.HasClaim(c => c.Type == "Permission");

            if (!hasUserPermissions)
            {
                context.Fail(new(this, "No permission provided"));
                return Task.CompletedTask;
            }

            var userClaims = context.User.Claims;

            if (userClaims.Where(x => x.Type == "Permission").Select(x => x.Value).ToList().Contains(requirement.PermissionName))
                context.Succeed(requirement);
            else
                context.Fail(new(this, "Not authorized!"));

            return Task.CompletedTask;
        }

    }
}
