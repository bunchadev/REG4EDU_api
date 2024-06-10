using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto.Permission;

namespace REG4EDU_api.Repositories
{
    public interface IPermissionRepository
    {
        Task<List<PermissionDto>> GetAllPermission();
        Task<string> UpdatePermission(UpdatePermissionDto updatePermission);
        Task<string> CreatePermission(CreatePermissionDto createPermission);
        Task<string> DeletePermission(Guid id);
    }
}
