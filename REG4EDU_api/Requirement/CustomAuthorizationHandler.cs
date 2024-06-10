using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using REG4EDU_api.Data;
using System.Security.Claims;

namespace REG4EDU_api.Requirement
{
    public class CustomAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly DevDbContext dbContext;

        public CustomAuthorizationHandler(DevDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return;
            var user = await dbContext.Users
                .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId));
            if (user is null) return;
            var httpContext = context.Resource as HttpContext;
            var apiEndpoint = httpContext?.Request.Path + httpContext?.Request.QueryString;
            if (apiEndpoint is null) return;
            var rolePermissions = await dbContext.RolePermissions
                .Where(x => x.Role_Id == user.RoleId)
                .Select(x => x.Permission)
                .ToListAsync();
            var hasPermission = rolePermissions
                .Any(x => apiEndpoint.Contains(x!.ApiEndpoint ?? "") && x.PermissionName == requirement.Permission);
            if (hasPermission)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail(new AuthorizationFailureReason(this, "Permission denied"));
            }
        }
    }
}
