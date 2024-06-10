using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto.Permission;
using REG4EDU_api.Repositories;

namespace REG4EDU_api.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }
        public async Task<string> CreatePermission(CreatePermissionDto createPermission)
        {
            return await permissionRepository.CreatePermission(createPermission);
        }

        public async Task<string> DeletePermission(Guid id)
        {
            return await permissionRepository.DeletePermission(id);
        }

        public async Task<List<PermissionDto>> GetAllPermission()
        {
            return await permissionRepository.GetAllPermission();
        }

        public async Task<string> UpdatePermission(UpdatePermissionDto updatePermission)
        {
            return await permissionRepository.UpdatePermission(updatePermission);
        }
    }
}
