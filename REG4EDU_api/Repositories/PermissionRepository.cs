using Microsoft.EntityFrameworkCore;
using REG4EDU_api.Data;
using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto.Permission;

namespace REG4EDU_api.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly DevDbContext dbContext;

        public PermissionRepository(DevDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<string> CreatePermission(CreatePermissionDto createPermission)
        {
            if (dbContext.Permissions.Any(x => x.PermissionName == createPermission.PermissionName)) return "400";
            var permission = new Permission()
            {
                PermissionName = createPermission.PermissionName,
                ApiEndpoint = createPermission.ApiEndpoint,
                Description = createPermission.Description,
            };
            await dbContext.Permissions.AddAsync(permission);
            await dbContext.SaveChangesAsync();
            return "200";
        }

        public async Task<string> DeletePermission(Guid id)
        {
            var permission = await dbContext.Permissions
                .FirstOrDefaultAsync(x => x.Id == id);
            if (permission is null) return "400";
            dbContext.Permissions.Remove(permission);
            await dbContext.SaveChangesAsync();
            return "200";
        }

        public async Task<List<PermissionDto>> GetAllPermission()
        {
            var permissions = await dbContext.Permissions
                .Select(x =>
                  new PermissionDto
                  {
                      Id = x.Id,
                      PermissionName = x.PermissionName,
                      ApiEndpoint = x.ApiEndpoint,
                      UpdatedAt = x.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                  }
                )
                .ToListAsync();
            return permissions;
        }

        public async Task<string> UpdatePermission(UpdatePermissionDto updatePermission)
        {
            var permission = await dbContext.Permissions
                .FirstOrDefaultAsync(x => x.Id == updatePermission.Id);
            if (permission is null) return "400";
            else
            {
                permission.PermissionName = updatePermission.PermissionName;
                permission.ApiEndpoint = updatePermission.ApiEndpoint;
                permission.Description = updatePermission.Description;
                dbContext.Permissions.Update(permission);
                await dbContext.SaveChangesAsync();
                return "200";
            }
        }
    }
}
