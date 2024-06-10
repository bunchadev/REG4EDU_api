using REG4EDU_api.Model.Dto.Permission;
using REG4EDU_api.Model.Dto.Role;
using REG4EDU_api.Model.Dto.RolePermission;
using REG4EDU_api.Repositories;

namespace REG4EDU_api.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<string> CreateRole(CreateRoleDto createRole)
        {
            return await roleRepository.CreateRole(createRole);
        }

        public async Task<string> CreateRolePermission(List<RolePermissionDto> rolePermissionDto)
        {
            return await roleRepository.CreateRolePermission(rolePermissionDto);
        }

        public async Task<string> DeleteRole(Guid roleId)
        {
            return await roleRepository.DeleteRole(roleId);
        }

        public async Task<string> DeleteRolePermission(RolePermissionDto rolePermission)
        {
            return await roleRepository.DeleteRolePermission(rolePermission);
        }

        public async Task<List<RoleDto>> GetAllRoles()
        {
            return await roleRepository.GetAllRoles();
        }

        public async Task<List<PermissionDto>> GetPermissionNotYetRole(Guid roleId)
        {
            return await roleRepository.GetPermissionNotYetRole(roleId);
        }

        public async Task<List<PermissionDto>> GetPermissionWithRole(Guid roleId)
        {
            return await roleRepository.GetPermissionWithRole(roleId);
        }

        public async Task<List<RoleDto>> GetRoleWithPagination(int current, int pageSize, string? order, string? field, string? roleName, string? creatorName)
        {
            return await roleRepository.GetRoleWithPagination(current, pageSize, order, field, roleName, creatorName?.Replace("?", ""));
        }

        public async Task<string> UpdateRole(UpdateRoleDto updateRole)
        {
            return await roleRepository.UpdateRole(updateRole);
        }
    }
}
