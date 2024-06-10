using Microsoft.Extensions.Caching.Memory;
using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto;
using REG4EDU_api.Model.Dto.GetStudent;
using REG4EDU_api.Model.Dto.GroupClass;
using REG4EDU_api.Repositories;

namespace REG4EDU_api.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository subjectRepository;
        private readonly IMemoryCache memoryCache;

        public SubjectService(ISubjectRepository subjectRepository, IMemoryCache memoryCache)
        {
            this.subjectRepository = subjectRepository;
            this.memoryCache = memoryCache;
        }

        public async Task<Subject?> CreateSubject(CreateSubject createSubject)
        {
            var subject = await subjectRepository.CreateSubject(createSubject);
            if (subject is not null) memoryCache.Remove("subjectmajors");
            return subject;
        }

        public async Task<string> DeleteSubject(List<Guid> id)
        {
            var status = await subjectRepository.DeleteSubject(id);
            if (status == "200") memoryCache.Remove("subjectmajors");
            return status;
        }

        public async Task<string> DeleteSubjectSemester(Guid id)
        {
            return await subjectRepository.DeleteSubjectSemester(id);
        }

        public async Task<List<Subject>> GetAllSubject()
        {
            return await subjectRepository.GetAllSubject();
        }

        public async Task<List<SubjectMajorDto>> GetAllSubjectWithMajor() =>
        await memoryCache.GetOrCreateAsync("subjectmajors", async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                return await subjectRepository.GetAllSubjectWithMajor();
            }) ?? [];

        public async Task<List<GetStudentResDto>> GetStudentWithClass(GetStudentWithClassDto getStudent)
        {
            return await subjectRepository.GetStudentWithClass(getStudent);
        }

        public async Task<List<SubjectMajorDto>> GetSubjectPagination(int current, int pageSize, string? order, string? field, string? subjectName, string? majorName, int numberOfCredits, string? subjectCode, string? category)
        {
            return await subjectRepository.GetSubjectPagination(current, pageSize, order, field, subjectName, majorName, numberOfCredits, subjectCode, category?.Replace("?", ""));
        }

        public async Task<List<SubjectSemesterDto>> GetSubjectSemesters(string? semesterName, string? majorsCode, string? category, string? order, string? field, string? subjectCode)
        {
            return await subjectRepository.GetSubjectSemesters(semesterName, majorsCode, category, order, field, subjectCode?.Replace("?", ""));
        }

        public async Task<List<GroupClassResDto>> GroupClass(GroupClassDto_1 groupClass)
        {
            return await subjectRepository.GroupClass(groupClass);
        }

        public async Task<Subject?> UpdateSubject(UpdateSubjectDto subjectDto)
        {
            var subject = await subjectRepository.UpdateSubject(subjectDto);
            if (subject is not null) memoryCache.Remove("subjectmajors");
            return subject;
        }
    }
}
