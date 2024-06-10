using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REG4EDU_api.Model.Dto.Permission;
using REG4EDU_api.Repositories;

namespace REG4EDU_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository permissionRepository;

        public PermissionController(IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPermission()
        {
            var result = await permissionRepository.GetAllPermission();
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdatePermission([FromBody] UpdatePermissionDto permissionDto)
        {
            var result = await permissionRepository.UpdatePermission(permissionDto);
            return Ok(new { statusCode = result });
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionDto permissionDto)
        {
            var result = await permissionRepository.CreatePermission(permissionDto);
            return Ok(new { statusCode = result });
        }
        [HttpGet]
        [Route("delete/{id:guid}")]
        public async Task<IActionResult> DeletePermission([FromRoute] Guid id)
        {
            var result = await permissionRepository.DeletePermission(id);
            return Ok(new { statusCode = result });
        }
    }
}
