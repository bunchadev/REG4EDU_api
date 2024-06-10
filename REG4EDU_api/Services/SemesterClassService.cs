using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto;
using REG4EDU_api.Repositories;

namespace REG4EDU_api.Services
{
    public class SemesterClassService : ISemesterClassService
    {
        private readonly ISemesterClassRepository semesterClassRepository;

        public SemesterClassService(ISemesterClassRepository semesterClassRepository)
        {
            this.semesterClassRepository = semesterClassRepository;
        }

        public async Task<MessageStatus> CreateSemesterClass(CreateClassDto semesterClass)
        {
            return await semesterClassRepository.CreateSemesterClass(semesterClass);
        }

        public async Task<string> DeleteSemesterClass(Guid semesterClassId)
        {
            return await semesterClassRepository.DeleteSemesterClass(semesterClassId);
        }

        public async Task<List<SemesterClassSubject>> GetSemesterClass(SemesterSubjectDto_3 semesterClass)
        {
            return await semesterClassRepository.GetSemesterClass(semesterClass);
        }

        public async Task<MessageStatus> UpdateSemesterClass(SemesterClassSubject semesterClass)
        {
            return await semesterClassRepository.UpdateSemesterClass(semesterClass);
        }
    }
}
