using Microsoft.AspNetCore.Authorization;

namespace cibus.infrastructure.Authentication;
public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission) 
        : base(policy: permission.ToString())
    {

    }
}
