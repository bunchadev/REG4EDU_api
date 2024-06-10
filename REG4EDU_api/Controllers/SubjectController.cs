using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REG4EDU_api.Model.Dto;
using REG4EDU_api.Model.Dto.GetStudent;
using REG4EDU_api.Model.Dto.GroupClass;
using REG4EDU_api.Services;

namespace REG4EDU_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSubject()
        {
            var result = await subjectService.GetAllSubject();
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpGet]
        [Route("majors")]
        public async Task<IActionResult> GetAllSubjectWithMajor()
        {
            var result = await subjectService.GetAllSubjectWithMajor();
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("update")]
        [Authorize(Policy = "UpdateSubjectPolicy")]
        public async Task<IActionResult> UpdateSubject([FromBody] UpdateSubjectDto subjectDto)
        {
            var result = await subjectService.UpdateSubject(subjectDto);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("create")]
        [Authorize(Policy = "CreateSubjectPolicy")]
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubject createSubject)
        {
            var result = await subjectService.CreateSubject(createSubject);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("delete")]
        [Authorize(Policy = "DeleteSubjectPolicy")]
        public async Task<IActionResult> DeleteSubject([FromBody] List<Guid> subjectIds)
        {
            var result = await subjectService.DeleteSubject(subjectIds);
            return Ok(new { statusCode = result });
        }
        [HttpGet]
        [Route("pagination")]
        [Authorize(Policy = "ViewSubjectPolicy")]
        public async Task<IActionResult> GetAllSubjectWithPagination([FromQuery] int current, [FromQuery] int pageSize, [FromQuery] string? order, [FromQuery] string? field, [FromQuery] string? subjectName, [FromQuery] string? majorName, [FromQuery] int numberOfCredits, [FromQuery] string? subjectCode, [FromQuery] string? category)
        {
            string queryString = string.Format("current={0}&pageSize={1}&order={2}&field={3}&subjectName={4}&majorName={5}&numberOfCredits={6}&subjectCode={7}&category={8}",
                                    current, pageSize, order, field, subjectName, majorName, numberOfCredits, subjectCode, category);
            var result = await subjectService.GetSubjectPagination(current, pageSize, order, field, subjectName, majorName, numberOfCredits, subjectCode, category);
            return Ok(new { statusCode = "200", data = result, query = queryString });
        }
        [HttpGet]
        [Route("semester")]
        public async Task<IActionResult> GetSubjectSemester([FromQuery] string? semesterName, [FromQuery] string? majorsCode, [FromQuery] string? category, [FromQuery] string? order, [FromQuery] string? field, [FromQuery] string? subjectCode)
        {
            var result = await subjectService.GetSubjectSemesters(semesterName, majorsCode, category, order, field, subjectCode);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("semester/delete")]
        public async Task<IActionResult> DeleteSemesterSubject([FromBody] DeleteSubject subject)
        {
            var result = await subjectService.DeleteSubjectSemester(subject.SemesterSubject_Id);
            if (result == "200") return Ok(new { statusCode = result });
            return Ok(new { statusCode = result });
        }
        [HttpPost]
        [Route("group")]
        public async Task<IActionResult> GroupClasses([FromBody] GroupClassDto_1 groupClass)
        {
            var result = await subjectService.GroupClass(groupClass);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("class/student")]
        public async Task<IActionResult> GetStudentWithClass([FromBody] GetStudentWithClassDto getStudent)
        {
            var result = await subjectService.GetStudentWithClass(getStudent);
            return Ok(new { statusCode = "200", data = result });
        }
    }
}
