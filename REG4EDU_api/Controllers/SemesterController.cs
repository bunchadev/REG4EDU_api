using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REG4EDU_api.Model.Dto;
using REG4EDU_api.Services;

namespace REG4EDU_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterService semesterService;

        public SemesterController(ISemesterService semesterService)
        {
            this.semesterService = semesterService;
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllSemester()
        {
            var result = await semesterService.GetSemesterListAsync();
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("subject")]
        public async Task<IActionResult> GetSemesterSubjectMajors([FromBody] SemesterSubjectDto_2 semesterSubject)
        {
            var result = await semesterService.GetSemesterSubjectMajors(semesterSubject);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSemesterSubject([FromBody] SemesterSubjectDto semesterSubject)
        {
            var result = await semesterService.CreateSemesterSubject(semesterSubject);
            if (result == "200") return Ok(new { statusCode = result });
            return BadRequest(result);
        }
        [HttpGet]
        [Route("one")]
        public async Task<IActionResult> GetSemester()
        {
            var semester = await semesterService.GetSemester();
            if (semester is not null) return Ok(new { statusCode = "200", data = semester });
            return BadRequest(semester);
        }
        [HttpGet]
        [Route("{code}")]
        public async Task<IActionResult> GetSemesterWithCode([FromRoute] string code)
        {
            var result = await semesterService.GetSemesterWithCode(code);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpGet]
        [Route("update/{semesterName}")]
        public async Task<IActionResult> UpdateSemester([FromRoute] string semesterName)
        {
            var result = await semesterService.UpdateSemester(semesterName);
            return Ok(new { statusCode = result });
        }
    }
}
