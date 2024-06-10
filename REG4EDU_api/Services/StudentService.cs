using REG4EDU_api.Model.Dto;
using REG4EDU_api.Model.Dto.GetStudent;
using REG4EDU_api.Model.Dto.SemesterClass;
using REG4EDU_api.Model.Dto.StudentSubjects;
using REG4EDU_api.Repositories;

namespace REG4EDU_api.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<string> CreateStudent(CreateStudentDto createStudent)
        {
            return await studentRepository.CreateStudent(createStudent);
        }

        public async Task<string> DeleteStudent(Guid studentId)
        {
            return await studentRepository.DeleteStudent(studentId);
        }

        public async Task<List<UserClassDto_1>> GetClassWithStudent(Guid studentId)
        {
            return await studentRepository.GetClassWithStudent(studentId);
        }

        public async Task<List<UserClassDto_1>> GetClassWithStudent_1(SemesterClassDto semesterClass)
        {
            return await studentRepository.GetClassWithStudent_1(semesterClass);
        }

        public async Task<List<UserSubjectDto>> GetStudentSubject(StudentSubjectDto studentSubject)
        {
            return await studentRepository.GetStudentSubject(studentSubject);
        }

        public async Task<GetStudentResDto?> GetStudentWithId(string studentName)
        {
            return await studentRepository.GetStudentWithId(studentName);
        }

        public async Task<List<GetStudentDto>> GetStudentWithPagination(int current, int pageSize, string? order, string? field, string? email, string? userName)
        {
            return await studentRepository.GetStudentWithPagination(current, pageSize, order, field, email, userName?.Replace("?", ""));
        }

        public async Task<List<StudentSubjectDto_1>> GetSubjectWithStudent(StudentSubjectDto_2 studentSubject)
        {
            return await studentRepository.GetSubjectWithStudent(studentSubject);
        }

        public async Task<List<GroupClassDto>> GroupClassStudent(StudentClassDto studentClass)
        {
            return await studentRepository.GroupClassStudent(studentClass);
        }

        public async Task<StudentToken?> LoginStudent(UserLogin userLogin)
        {
            return await studentRepository.LoginStudent(userLogin);
        }

        public async Task<string> UpdateStudent(UpdateStudentDto updateStudent)
        {
            return await studentRepository.UpdateStudent(updateStudent);
        }

        public async Task<MessageStatus> UpdateStudentClass(UpdateClassDto updateClass)
        {
            return await studentRepository.UpdateStudentClass(updateClass);
        }

        public async Task<string> UpdateStudentSubject(StudentSubjectsDto subjectsDto)
        {
            return await studentRepository.UpdateStudentSubject(subjectsDto);
        }
    }
}
