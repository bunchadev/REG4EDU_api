using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto;

namespace REG4EDU_api.Services
{
    public interface ISemesterClassService
    {
        Task<List<SemesterClassSubject>> GetSemesterClass(SemesterSubjectDto_3 semesterClass);
        Task<MessageStatus> UpdateSemesterClass(SemesterClassSubject semesterClass);
        Task<MessageStatus> CreateSemesterClass(CreateClassDto semesterClass);
        Task<string> DeleteSemesterClass(Guid semesterClassId);
    }
}
