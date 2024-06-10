using Microsoft.EntityFrameworkCore;
using REG4EDU_api.Data;
using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto.Permission;
using REG4EDU_api.Model.Dto.Role;
using REG4EDU_api.Model.Dto.RolePermission;

namespace REG4EDU_api.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DevDbContext dbContext;
        public RoleRepository(DevDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateRole(CreateRoleDto createRole)
        {
            if (dbContext.Roles.Any(x => x.RoleName == createRole.RoleName)) return "400";
            var role = new Role
            {
                RoleName = createRole.RoleName,
                CreatorName = createRole.CreatorName,
            };
            await dbContext.AddAsync(role);
            await dbContext.SaveChangesAsync();
            return "200";
        }

        public async Task<string> CreateRolePermission(List<RolePermissionDto> rolePermissionDto)
        {
            var listRolePermission = new List<RolePermission>();
            foreach (var item in rolePermissionDto)
            {
                var rolePermission = new RolePermission
                {
                    Role_Id = item.RoleId,
                    Permission_Id = item.PermissionId,
                };
                listRolePermission.Add(rolePermission);
            }
            await dbContext.AddRangeAsync(listRolePermission);
            await dbContext.SaveChangesAsync();
            return "200";
        }

        public async Task<string> DeleteRole(Guid roleId)
        {
            var rolePermissions = await dbContext.RolePermissions
                .Where(x => x.Role_Id == roleId)
                .ToListAsync();
            if (rolePermissions.Count > 0)
            {
                dbContext.RemoveRange(rolePermissions);
                await dbContext.SaveChangesAsync();
            }
            var role = await dbContext.Roles
                .FirstOrDefaultAsync(x => x.Id == roleId);
            if (role == null) return "400";
            dbContext.Roles.Remove(role);
            await dbContext.SaveChangesAsync();
            return "200";
        }

        public async Task<string> DeleteRolePermission(RolePermissionDto rolePermission)
        {
            var rolePermission_0 = await dbContext.RolePermissions
                .FirstOrDefaultAsync(x => x.Role_Id == rolePermission.RoleId
                  && x.Permission_Id == rolePermission.PermissionId
                );
            if (rolePermission_0 is null) return "400";
            dbContext.Remove(rolePermission_0);
            await dbContext.SaveChangesAsync();
            return "200";
        }

        public async Task<List<RoleDto>> GetAllRoles()
        {
            return await dbContext.Roles.Select(x =>
                 new RoleDto
                 {
                     Id = x.Id,
                     RoleName = x.RoleName,
                     CreatorName = x.CreatorName,
                     UpdateHours = x.UpdatedAt.ToString("HH:mm:ss"),
                     UpdateDay = x.UpdatedAt.ToString("yyyy-MM-dd")
                 }
            ).ToListAsync();
        }

        public async Task<List<PermissionDto>> GetPermissionNotYetRole(Guid roleId)
        {
            var rolePermissionId = await dbContext.RolePermissions
                .Where(x => x.Role_Id == roleId)
                .Select(x => x.Permission_Id)
                .ToListAsync();
            var permissionNotYet = await dbContext.Permissions
                .Where(x => !rolePermissionId.Contains(x.Id)).OrderBy(x => x.Group)
                .Select(x =>
                   new PermissionDto
                   {
                       Id = x.Id,
                       PermissionName = x.PermissionName,
                       ApiEndpoint = x.ApiEndpoint,
                       Description = x.Description,
                       UpdatedAt = x.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                   }
                )
                .ToListAsync();
            return permissionNotYet;

        }

        public async Task<List<PermissionDto>> GetPermissionWithRole(Guid roleId)
        {
            var permissions = await dbContext.RolePermissions
                .Where(x => x.Role_Id == roleId)
                .Include(x => x.Permission)
                .OrderBy(x => x.Permission!.Group)
                .Select(x =>
                   new PermissionDto
                   {
                       Id = x.Permission!.Id,
                       PermissionName = x.Permission.PermissionName,
                       ApiEndpoint = x.Permission.ApiEndpoint,
                       Description = x.Permission.Description,
                       UpdatedAt = x.Permission.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                   }
                )
                .ToListAsync();
            return permissions;
        }

        public async Task<List<RoleDto>> GetRoleWithPagination(int current, int pageSize, string? order, string? field, string? roleName, string? creatorName)
        {
            var query = dbContext.Roles
               .Where(x =>
                  (string.IsNullOrWhiteSpace(roleName) || x.RoleName == roleName) &&
                  (string.IsNullOrWhiteSpace(creatorName) || x.RoleName == roleName)
               ).OrderBy(x => x.Id);
            var skipResults = (current - 1) * pageSize;
            var roles = await query.Skip(skipResults).Take(pageSize).ToListAsync();
            if (!string.IsNullOrWhiteSpace(field) && !string.IsNullOrWhiteSpace(order))
            {
                roles = field.ToLower() switch
                {
                    "updateHours" => order.ToLower() == "ascend" ? roles.OrderBy(x => x.UpdatedAt).ToList() : roles.OrderByDescending(x => x.UpdatedAt).ToList(),
                    "roleName" => order.ToLower() == "ascend" ? roles.OrderBy(x => x.RoleName).ToList() : roles.OrderByDescending(x => x.RoleName).ToList(),
                    _ => roles
                };
            }
            return roles.Select(x =>
                   new RoleDto
                   {
                       Id = x.Id,
                       RoleName = x.RoleName,
                       CreatorName = x.CreatorName,
                       UpdateHours = x.UpdatedAt.ToString("HH:mm:ss"),
                       UpdateDay = x.UpdatedAt.ToString("yyyy-MM-dd")
                   }
            ).ToList();
        }

        public async Task<string> UpdateRole(UpdateRoleDto updateRole)
        {
            var role = await dbContext.Roles
                .FirstOrDefaultAsync(x => x.Id == updateRole.Id);
            if (role == null) return "400";
            else
            {
                role.RoleName = updateRole.RoleName;
                role.CreatorName = updateRole.CreatorName;
                dbContext.Roles.Update(role);
                await dbContext.SaveChangesAsync();
                return "200";
            }
        }
    }
}
