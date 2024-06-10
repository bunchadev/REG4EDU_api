using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REG4EDU_api.Model.Dto;
using REG4EDU_api.Model.Dto.SemesterClass;
using REG4EDU_api.Model.Dto.StudentSubjects;
using REG4EDU_api.Services;

namespace REG4EDU_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> LoginStudent([FromBody] UserLogin userLogin)
        {
            var result = await studentService.LoginStudent(userLogin);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("subject")]
        public async Task<IActionResult> GetSubjectStudent([FromBody] StudentSubjectDto studentSubject)
        {
            var result = await studentService.GetStudentSubject(studentSubject);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("group/class")]
        public async Task<IActionResult> GroupClassStudent([FromBody] StudentClassDto studentClass)
        {
            var result = await studentService.GroupClassStudent(studentClass);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("update/class")]
        [Authorize(Policy = "AdminOrStudentPolicy")]
        public async Task<IActionResult> UpdateClassStudent([FromBody] UpdateClassDto updateClass)
        {
            var result = await studentService.UpdateStudentClass(updateClass);
            return Ok(result);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetClassWithStudent([FromRoute] Guid id)
        {
            var result = await studentService.GetClassWithStudent(id);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpGet]
        [Route("pagination")]
        [Authorize(Policy = "ViewStudentPolicy")]
        public async Task<IActionResult> GetStudentsPagination([FromQuery] int current, [FromQuery] int pageSize, [FromQuery] string? order, [FromQuery] string? field, [FromQuery] string? email, [FromQuery] string? userName)
        {
            var result = await studentService.GetStudentWithPagination(current, pageSize, order, field, email, userName);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("create")]
        [Authorize(Policy = "CreateStudentPolicy")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto createStudent)
        {
            var result = await studentService.CreateStudent(createStudent);
            return Ok(new { statusCode = result });
        }
        [HttpGet]
        [Route("delete/{id:guid}")]
        [Authorize(Policy = "DeleteStudentPolicy")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid id)
        {
            var result = await studentService.DeleteStudent(id);
            return Ok(new { statusCode = result });
        }
        [HttpPost]
        [Route("update")]
        [Authorize(Policy = "UpdateStudentPolicy")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentDto updateStudent)
        {
            var result = await studentService.UpdateStudent(updateStudent);
            return Ok(new { statusCode = result });
        }
        [HttpPost]
        [Route("subject_1")]
        [Authorize(Policy = "ViewStudentWithSubjectPolicy")]
        public async Task<IActionResult> GetSubjectWithStudent([FromBody] StudentSubjectDto_2 studentSubject)
        {
            var result = await studentService.GetSubjectWithStudent(studentSubject);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpGet]
        [Route("one/{studentName}")]
        public async Task<IActionResult> GetStudentWithId([FromRoute] string studentName)
        {
            var result = await studentService.GetStudentWithId(studentName);
            if (result is not null) return Ok(new { statusCode = "200", data = result });
            return Ok(new { statusCode = "400" });
        }
        [HttpPost]
        [Route("classes")]
        public async Task<IActionResult> GetClassWithStudent_1([FromBody] SemesterClassDto semesterClass)
        {
            var result = await studentService.GetClassWithStudent_1(semesterClass);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("subject/update")]
        public async Task<IActionResult> UpdateStudentSubject([FromBody] StudentSubjectsDto subjectsDto)
        {
            var result = await studentService.UpdateStudentSubject(subjectsDto);
            return Ok(new { statusCode = result });
        }
    }
}
