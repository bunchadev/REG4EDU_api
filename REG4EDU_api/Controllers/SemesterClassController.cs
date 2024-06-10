using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REG4EDU_api.Model.Dto;
using REG4EDU_api.Services;

namespace REG4EDU_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterClassController : ControllerBase
    {
        private readonly ISemesterClassService semesterClassService;

        public SemesterClassController(ISemesterClassService semesterClassService)
        {
            this.semesterClassService = semesterClassService;
        }
        [HttpPost]
        public async Task<IActionResult> GetSemesterClass([FromBody] SemesterSubjectDto_3 semesterClass)
        {
            var result = await semesterClassService.GetSemesterClass(semesterClass);
            return Ok(new { statusCode = "200", data = result });
        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateSemesterClass([FromBody] SemesterClassSubject semesterClass)
        {
            var result = await semesterClassService.UpdateSemesterClass(semesterClass);
            return Ok(result);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassDto createClass)
        {
            var result = await semesterClassService.CreateSemesterClass(createClass);
            return Ok(result);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteSemesterClass([FromRoute] Guid id)
        {
            var result = await semesterClassService.DeleteSemesterClass(id);
            return Ok(new { statusCode = result });
        }
    }
}
