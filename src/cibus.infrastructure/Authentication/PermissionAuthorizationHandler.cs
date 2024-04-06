using cibus.application.Interfaces.BusinessLogics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.infrastructure.Authentication
{
    public class PermissionAuthorizationHandler
        : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionBL _permissionBL;
        public PermissionAuthorizationHandler(IPermissionBL permissionBL)
        {
            _permissionBL = permissionBL;
        }

        // TODO:: instead of fetching user permissions from the database, need to cache the user permissions when user get the token
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            PermissionRequirement requirement)
        {
            string userId = context.User.Claims.FirstOrDefault(
                x => x.Type == "UserId")?.Value;

            if (!Int32.TryParse(userId, out Int32 parsedRoleId))
            {
                return;
            }

            IEnumerable<string> permissions = await _permissionBL.GetPermissionsByUserIdAsync(Convert.ToInt32(userId));

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
