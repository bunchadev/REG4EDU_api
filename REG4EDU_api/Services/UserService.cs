using REG4EDU_api.Model.Dto;
using REG4EDU_api.Model.Dto.SemesterClass;
using REG4EDU_api.Repositories;

namespace REG4EDU_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<string> AddRangeUserSubject(List<DeleteUserSubject> subjects)
        {
            return await userRepository.AddRangeUserSubject(subjects);
        }

        public async Task<string> DeleteUsers(Guid userId)
        {
            return await userRepository.DeleteUsers(userId);
        }

        public async Task<string> DeleteUserSubject(DeleteUserSubject subject)
        {
            return await userRepository.DeleteUserSubject(subject);
        }

        public async Task<List<RegisterUserDto>> GetAllUser()
        {
            return await userRepository.GetAllUser();
        }

        public async Task<List<UserClassDto_1>> GetClassWithUser(Guid userId)
        {
            return await userRepository.GetClassWithUser(userId);
        }

        public async Task<List<UserClassDto_2>> GetClassWithUser_1(Guid userId, string semesterName)
        {
            return await userRepository.GetClassWithUser_1(userId, semesterName);
        }

        public async Task<List<UserClassDto_1>> GetClassWithUser_2(SemesterClassDto semesterClass)
        {
            return await userRepository.GetClassWithUser_2(semesterClass);
        }

        public async Task<List<GroupClassDto>> GetSemesterClassWithSubject(SemesterSubjectDto_4 semesterSubject)
        {
            return await userRepository.GetSemesterClassWithSubject(semesterSubject);
        }

        public async Task<List<UserSubjectDto>> GetSubjectNoUser(Guid id)
        {
            return await userRepository.GetSubjectNoUser(id);
        }

        public async Task<List<UserDto_1>> GetUsersPagination(int current, int pageSize, string? order, string? field, string? name, string? userName)
        {
            return await userRepository.GetUsersPagination(current, pageSize, order, field, name, userName?.Replace("?", ""));
        }

        public async Task<List<UserSubjectDto>> GetUserSubjects(Guid id)
        {
            return await userRepository.GetUserSubjects(id);
        }

        public async Task<UserToken?> LoginUser(UserLogin userLogin)
        {
            return await userRepository.LoginUser(userLogin);
        }

        public async Task<string?> RegisterUser(UserDto userDto)
        {
            return await userRepository.RegisterUser(userDto);
        }

        public async Task<MessageStatus> UpdateClassWithSubject(UpdateClassDto updateClass)
        {
            return await userRepository.UpdateClassWithSubject(updateClass);
        }

        public async Task<string> UpdateUser(UserUpdateDto updateDto)
        {
            return await userRepository.UpdateUser(updateDto);
        }
    }
}
