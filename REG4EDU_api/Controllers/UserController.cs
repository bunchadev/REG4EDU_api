using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REG4EDU_api.Model.Dto;
using REG4EDU_api.Model.Dto.SemesterClass;
using REG4EDU_api.Services;

namespace REG4EDU_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> LoginUser([FromBody] UserLogin userLogin)
        {
            var result = await userService.LoginUser(userLogin);
            if (result is null) return Ok(new { statusCode = "400", message = "Tài khoản hoặc mật khẩu không đúng" });
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
        {
            var result = await userService.RegisterUser(userDto);
            if (result is null) return Ok(new { statusCode = "400", message = "Đăng ký không thành công" });
            return Ok(new { statusCode = "200", message = "Đăng ký thành công" });
        }
        [HttpGet]
        [Route("subject/{id:guid}")]
        [Authorize(Policy = "ViewUserSubjectPolicy")]
        public async Task<IActionResult> GetUserSubject([FromRoute] Guid id)
        {
            var subjects = await userService.GetUserSubjects(id);
            if (subjects.Any()) return Ok(new { statusCode = "200", data = subjects });
            return Ok(new { statusCode = "400", data = subjects });
        }
        [HttpGet]
        [Route("class/{userId:guid}")]
        [Authorize(Policy = "ViewUserClassPolicy")]
        public async Task<IActionResult> GetClassWithUser([FromRoute] Guid userId)
        {
            var result = await userService.GetClassWithUser(userId);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("class/{userId:guid}")]
        public async Task<IActionResult> GetClassWithUser_1([FromRoute] Guid userId, [FromBody] string semesterName)
        {
            var result = await userService.GetClassWithUser_1(userId, semesterName);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await userService.GetAllUser();
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("subject/class")]
        public async Task<IActionResult> GetClassWithSubject([FromBody] SemesterSubjectDto_4 subject)
        {
            var result = await userService.GetSemesterClassWithSubject(subject);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("update/class")]
        [Authorize(Policy = "ScheduleUserPolicy")]
        public async Task<IActionResult> UpdateClass([FromBody] UpdateClassDto updateClass)
        {
            var result = await userService.UpdateClassWithSubject(updateClass);
            return Ok(result);
        }
        [HttpGet]
        [Route("pagination")]
        public async Task<IActionResult> GetUsersPagination([FromQuery] int current, [FromQuery] int pageSize, [FromQuery] string? order, [FromQuery] string? field, [FromQuery] string? name, [FromQuery] string? userName)
        {
            var result = await userService.GetUsersPagination(current, pageSize, order, field, name, userName);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpGet]
        [Route("delete/{userId:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            var result = await userService.DeleteUsers(userId);
            return Ok(new { statusCode = result });
        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto updateDto)
        {
            var result = await userService.UpdateUser(updateDto);
            return Ok(new { statusCode = result });
        }
        [HttpPost]
        [Route("/subject/delete")]
        public async Task<IActionResult> DeleteUserSubject([FromBody] DeleteUserSubject subject)
        {
            var result = await userService.DeleteUserSubject(subject);
            return Ok(new { statusCode = result });
        }
        [HttpGet]
        [Route("/no/subject/{id:guid}")]
        public async Task<IActionResult> GetUserNoSubject([FromRoute] Guid id)
        {
            var result = await userService.GetSubjectNoUser(id);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("add/subject")]
        public async Task<IActionResult> AddRangeUserSubject([FromBody] List<DeleteUserSubject> subjects)
        {
            var result = await userService.AddRangeUserSubject(subjects);
            return Ok(new { statusCode = result });
        }
        [HttpPost]
        [Route("classes")]
        public async Task<IActionResult> GetClassWithUser_2([FromBody] SemesterClassDto semesterClass)
        {
            var result = await userService.GetClassWithUser_2(semesterClass);
            return Ok(new { statusCode = "200", data = result });
        }
    }
}
