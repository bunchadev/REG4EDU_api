using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto;
using REG4EDU_api.Model.Dto.GetStudent;
using REG4EDU_api.Model.Dto.GroupClass;

namespace REG4EDU_api.Repositories
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllSubject();
        Task<List<SubjectMajorDto>> GetAllSubjectWithMajor();
        Task<Subject?> UpdateSubject(UpdateSubjectDto subjectDto);
        Task<Subject?> CreateSubject(CreateSubject createSubject);
        Task<string> DeleteSubject(List<Guid> id);
        Task<List<SubjectMajorDto>> GetSubjectPagination(int current, int pageSize, string? order, string? field, string? subjectName, string? majorName, int numberOfCredits, string? subjectCode, string? category);
        Task<List<SubjectSemesterDto>> GetSubjectSemesters(string? semesterName, string? majorsCode, string? category, string? order, string? field, string? subjectCode);
        Task<string> DeleteSubjectSemester(Guid id);
        Task<List<GroupClassResDto>> GroupClass(GroupClassDto_1 groupClass);
        Task<List<GetStudentResDto>> GetStudentWithClass(GetStudentWithClassDto getStudent);
    }
}
