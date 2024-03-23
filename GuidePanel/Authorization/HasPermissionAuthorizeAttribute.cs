using Microsoft.AspNetCore.Authorization;

namespace GuidePanel.Authorization
{
    public sealed class HasPermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public HasPermissionAuthorizeAttribute(string permission) : base(policy: permission)
        {

        }
    }
}
