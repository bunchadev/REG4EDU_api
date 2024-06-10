using REG4EDU_api.Model.Dto;
using REG4EDU_api.Model.Dto.GetStudent;
using REG4EDU_api.Model.Dto.SemesterClass;
using REG4EDU_api.Model.Dto.StudentSubjects;

namespace REG4EDU_api.Services
{
    public interface IStudentService
    {
        Task<GetStudentResDto?> GetStudentWithId(string studentName);
        Task<StudentToken?> LoginStudent(UserLogin userLogin);
        Task<List<GroupClassDto>> GroupClassStudent(StudentClassDto studentClass);
        Task<List<UserSubjectDto>> GetStudentSubject(StudentSubjectDto studentSubject);
        Task<MessageStatus> UpdateStudentClass(UpdateClassDto updateClass);
        Task<List<UserClassDto_1>> GetClassWithStudent(Guid studentId);
        Task<List<GetStudentDto>> GetStudentWithPagination(int current, int pageSize, string? order, string? field, string? email, string? userName);
        Task<string> UpdateStudent(UpdateStudentDto updateStudent);
        Task<string> DeleteStudent(Guid studentId);
        Task<string> CreateStudent(CreateStudentDto createStudent);
        Task<List<StudentSubjectDto_1>> GetSubjectWithStudent(StudentSubjectDto_2 studentSubject);
        Task<List<UserClassDto_1>> GetClassWithStudent_1(SemesterClassDto semesterClass);
        Task<string> UpdateStudentSubject(StudentSubjectsDto subjectsDto);
    }
}
