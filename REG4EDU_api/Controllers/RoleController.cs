using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REG4EDU_api.Model.Dto.Role;
using REG4EDU_api.Model.Dto.RolePermission;
using REG4EDU_api.Services;

namespace REG4EDU_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await roleService.GetAllRoles();
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpGet]
        [Route("pagination")]
        public async Task<IActionResult> GetRoleWithPagination([FromQuery] int current, [FromQuery] int pageSize, [FromQuery] string? order, [FromQuery] string? field, [FromQuery] string? roleName, [FromQuery] string? creatorName)
        {
            var result = await roleService.GetRoleWithPagination(current, pageSize, order, field, roleName, creatorName);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRole)
        {
            var result = await roleService.CreateRole(createRole);
            return Ok(new { statusCode = result });
        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDto updateRole)
        {
            var result = await roleService.UpdateRole(updateRole);
            return Ok(new { statusCode = result });
        }
        [HttpGet]
        [Route("delete/{id:guid}")]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid id)
        {
            var result = await roleService.DeleteRole(id);
            return Ok(new { statusCode = result });
        }
        [HttpGet]
        [Route("permission/{id:guid}")]
        public async Task<IActionResult> GetPermissionWithRole([FromRoute] Guid id)
        {
            var result = await roleService.GetPermissionWithRole(id);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("permission/delete")]
        public async Task<IActionResult> DeleteRolePermission([FromBody] RolePermissionDto rolePermission)
        {
            var result = await roleService.DeleteRolePermission(rolePermission);
            return Ok(new { statusCode = result });
        }
        [HttpPost]
        [Route("permission/create")]
        public async Task<IActionResult> CreateRolePermission([FromBody] List<RolePermissionDto> rolePermission)
        {
            var result = await roleService.CreateRolePermission(rolePermission);
            return Ok(new { statusCode = result });
        }
        [HttpGet]
        [Route("permission/not/yet/{id:guid}")]
        public async Task<IActionResult> GetPermissionNotYetRole([FromRoute] Guid id)
        {
            var result = await roleService.GetPermissionNotYetRole(id);
            return Ok(new { statusCode = "200", data = result });
        }
    }
}
