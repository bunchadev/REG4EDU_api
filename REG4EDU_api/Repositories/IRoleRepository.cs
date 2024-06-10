using REG4EDU_api.Model.Dto.Permission;
using REG4EDU_api.Model.Dto.Role;
using REG4EDU_api.Model.Dto.RolePermission;

namespace REG4EDU_api.Repositories
{
    public interface IRoleRepository
    {
        Task<List<RoleDto>> GetAllRoles();
        Task<List<RoleDto>> GetRoleWithPagination(int current, int pageSize, string? order, string? field, string? roleName, string? creatorName);
        Task<string> UpdateRole(UpdateRoleDto updateRole);
        Task<string> CreateRole(CreateRoleDto createRole);
        Task<string> DeleteRole(Guid roleId);
        Task<List<PermissionDto>> GetPermissionWithRole(Guid roleId);
        Task<string> CreateRolePermission(List<RolePermissionDto> rolePermissionDto);
        Task<string> DeleteRolePermission(RolePermissionDto rolePermission);
        Task<List<PermissionDto>> GetPermissionNotYetRole(Guid roleId);
    }
}
