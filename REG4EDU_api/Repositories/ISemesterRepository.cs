using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto;

namespace REG4EDU_api.Repositories
{
    public interface ISemesterRepository
    {
        Task<List<Semester>> GetAllSemester();
        Task<string> CreateSemesterSubject(SemesterSubjectDto semesterSubject);
        Task<List<SemesterSubjectDto_1>> GetSemesterSubjectMajors(SemesterSubjectDto_2 semesterSubject);
        Task<Semester> GetSemester();
        Task<Semester> GetSemesterWithCode(string semesterName);
        Task<string> UpdateSemester(string semesterName);
    }
}
