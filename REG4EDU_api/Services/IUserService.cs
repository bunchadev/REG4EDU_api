using REG4EDU_api.Model.Dto;
using REG4EDU_api.Model.Dto.SemesterClass;

namespace REG4EDU_api.Services
{
    public interface IUserService
    {
        Task<string?> RegisterUser(UserDto userDto);
        Task<UserToken?> LoginUser(UserLogin userLogin);
        Task<List<UserSubjectDto>> GetUserSubjects(Guid id);
        Task<List<RegisterUserDto>> GetAllUser();
        Task<List<GroupClassDto>> GetSemesterClassWithSubject(SemesterSubjectDto_4 semesterSubject);
        Task<MessageStatus> UpdateClassWithSubject(UpdateClassDto updateClass);
        Task<List<UserClassDto_1>> GetClassWithUser(Guid userId);
        Task<List<UserClassDto_2>> GetClassWithUser_1(Guid userId, string semesterName);
        Task<List<UserDto_1>> GetUsersPagination(int current, int pageSize, string? order, string? field, string? name, string? userName);
        Task<string> DeleteUsers(Guid userId);
        Task<string> UpdateUser(UserUpdateDto updateDto);
        Task<string> DeleteUserSubject(DeleteUserSubject subject);
        Task<List<UserSubjectDto>> GetSubjectNoUser(Guid id);
        Task<string> AddRangeUserSubject(List<DeleteUserSubject> subjects);
        Task<List<UserClassDto_1>> GetClassWithUser_2(SemesterClassDto semesterClass);
    }
}
