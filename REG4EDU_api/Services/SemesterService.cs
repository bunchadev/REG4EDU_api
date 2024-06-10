using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto;
using REG4EDU_api.Repositories;

namespace REG4EDU_api.Services
{
    public class SemesterService : ISemesterService
    {
        private readonly ISemesterRepository semesterRepository;

        public SemesterService(ISemesterRepository semesterRepository)
        {
            this.semesterRepository = semesterRepository;
        }

        public async Task<string> CreateSemesterSubject(SemesterSubjectDto semesterSubject)
        {
            return await semesterRepository.CreateSemesterSubject(semesterSubject);
        }

        public async Task<Semester> GetSemester()
        {
            return await semesterRepository.GetSemester();
        }

        public async Task<List<Semester>> GetSemesterListAsync()
        {
            return await semesterRepository.GetAllSemester();
        }

        public async Task<List<SemesterSubjectDto_1>> GetSemesterSubjectMajors(SemesterSubjectDto_2 semesterSubject)
        {
            return await semesterRepository.GetSemesterSubjectMajors(semesterSubject);
        }

        public async Task<Semester> GetSemesterWithCode(string semesterName)
        {
            return await semesterRepository.GetSemesterWithCode(semesterName);
        }

        public async Task<string> UpdateSemester(string semesterName)
        {
            return await semesterRepository.UpdateSemester(semesterName);
        }
    }
}
