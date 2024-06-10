using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto;

namespace REG4EDU_api.Services
{
    public interface ISemesterService
    {
        Task<List<Semester>> GetSemesterListAsync();
        Task<string> CreateSemesterSubject(SemesterSubjectDto semesterSubject);
        Task<Semester> GetSemester();
        Task<List<SemesterSubjectDto_1>> GetSemesterSubjectMajors(SemesterSubjectDto_2 semesterSubject);
        Task<Semester> GetSemesterWithCode(string semesterName);
        Task<string> UpdateSemester(string semesterName);
    }
}
