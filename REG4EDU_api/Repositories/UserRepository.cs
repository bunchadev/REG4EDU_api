using Microsoft.EntityFrameworkCore;
using REG4EDU_api.Authentication;
using REG4EDU_api.Data;
using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto;
using REG4EDU_api.Model.Dto.SemesterClass;
using REG4EDU_api.Model.Dto.User;
using System.Data;

namespace REG4EDU_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevDbContext dbContext;
        private readonly IJwtToken jwtToken;

        public UserRepository(DevDbContext dbContext, IJwtToken jwtToken)
        {
            this.dbContext = dbContext;
            this.jwtToken = jwtToken;
        }

        public async Task<List<RegisterUserDto>> GetAllUser()
        {
            var users = await dbContext.Users
                .Select(x =>
                   new RegisterUserDto
                   {
                       UserId = x.UserId,
                       UserName = x.UserName,
                   }).ToListAsync();
            return users ?? [];
        }

        public async Task<List<UserDto_1>> GetUsersPagination(int current, int pageSize, string? order, string? field, string? name, string? userName)
        {
            var query = dbContext.Users
                .Where(x =>
                   (string.IsNullOrWhiteSpace(userName) || x.UserName == userName) &&
                   (string.IsNullOrWhiteSpace(name) || x.Name == name)
                ).OrderBy(x => x.UserId);
            var skipResults = (current - 1) * pageSize;
            var users = await query.Skip(skipResults).Take(pageSize).Include(x => x.Department).Include(x => x.Role).ToListAsync();
            if (!string.IsNullOrWhiteSpace(field) && !string.IsNullOrWhiteSpace(order))
            {
                users = field.ToLower() switch
                {
                    "name" => order.ToLower() == "ascend" ? users.OrderBy(x => x.Name).ToList() : users.OrderByDescending(x => x.Name).ToList(),
                    "userName" => order.ToLower() == "ascend" ? users.OrderBy(x => x.UserName).ToList() : users.OrderByDescending(x => x.UserName).ToList(),
                    _ => users
                };
            }
            return users.Select(x =>
                   new UserDto_1
                   {
                       UserName = x.UserName,
                       Name = x.Name ?? "?",
                       UserId = x.UserId,
                       DepartmentName = x.Department is not null ? x.Department.DepartmentCode : "",
                       Email = x.Email,
                       Password = x.Password,
                       Role = x.Role!.RoleName,
                   }
            ).ToList();
        }
        public static int SoGioTuongUng(int ca)
        {
            switch (ca)
            {
                case 1:
                    return 7;
                case 2:
                    return 8;
                case 3:
                    return 9;
                case 4:
                    return 10;
                case 5:
                    return 11;
                case 6:
                    return 13;
                case 7:
                    return 14;
                case 8:
                    return 15;
                case 9:
                    return 16;
                case 10:
                    return 17;
                default:
                    return 0;
            }
        }
        public async Task<List<UserClassDto_1>> GetClassWithUser(Guid userId)
        {
            var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.Status);
            if (semester is not null)
            {
                var semesterClass = await dbContext.SemestersClass
                    .Where(x => x.SemesterId == semester.SemesterId && x.UserId == userId)
                    .Include(x => x.User)
                    .Select(x =>
                       new UserClassDto_1
                       {
                           SubjectId = x.FK_SubjectId,
                           Name = x.Name,
                           ClassNumber = x.ClassNumber,
                           Classroom = x.Classroom,
                           WeekDay = x.WeekDay,
                           OnShift = x.OnShift,
                           EndShift = x.EndShift,
                           UserName = x.User != null ? x.User.UserName : null,
                           Describe = x.Describe,
                           Hours = SoGioTuongUng(x.OnShift),
                           Hours_1 = SoGioTuongUng(x.EndShift)
                       }
                    ).ToListAsync();
                return semesterClass;
            }
            return [];
        }

        public async Task<List<GroupClassDto>> GetSemesterClassWithSubject(SemesterSubjectDto_4 semesterClass)
        {
            var semesterClassSubject = await dbContext.SemestersClass
              .Where(x => x.FK_SubjectId == semesterClass.SubjectId && x.SemesterId == semesterClass.SemesterId)
              .OrderBy(x => x.ClassNumber).ThenBy(x => x.WeekDay).ThenBy(x => x.OnShift)
              .ToListAsync();
            var groupSemesterClass = semesterClassSubject
              .GroupBy(x => x.ClassNumber);
            var groupClass = new List<GroupClassDto>();
            foreach (var group in groupSemesterClass)
            {
                var data = new GroupClassDto()
                {
                    ClassNumber = group.Key,
                    Name = $".{group.Key} ",
                };
                foreach (var myClass in group)
                {
                    if (myClass.UserId == semesterClass.UserId) data.IsChecked = true;
                    data.Name += $"{myClass.Name}" + $"{(myClass.Describe != null ? $"_{myClass.Describe}" : "")}-" +
                    $"Thứ_{myClass.WeekDay}-" + $"Ca({myClass.OnShift}-{myClass.EndShift})-" + $"{(myClass.UserId != null ? "1/1" : "0/1")} ";
                }
                groupClass.Add(data);
            }
            return groupClass;
        }

        public async Task<List<UserSubjectDto>> GetUserSubjects(Guid id)
        {
            Semester? semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.Status);
            if (semester is not null)
            {
                var userSubjects = await dbContext.SemesterSubjects
                    .Where(x => x.SemesterId == semester.SemesterId)
                    .Join(
                       dbContext.UserSubjects,
                       x => x.FK_SubjectId,
                       y => y.FK_SubjectId,
                       (x, y) => y
                    )
                    .Where(a => a.UserId == id)
                    .Select(a =>
                        new UserSubjectDto
                        {
                            SubjectId = a.FK_SubjectId,
                            SubjectName = a.Subject != null ? a.Subject.SubjectName : "",
                        }
                    )
                    .ToListAsync();
                return userSubjects;
            }
            return [];
        }

        public async Task<UserToken?> LoginUser(UserLogin userLogin)
        {
            User? user = await dbContext.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserName == userLogin.Username);
            if (user is null) return null;
            if (user.Password != userLogin.Password) return null;
            var userToken = new UserToken()
            {
                Access_token = jwtToken.GenerateToken(user.UserId, user.Role.RoleName!, 1),
                Refresh_token = jwtToken.GenerateToken(user.UserId, user.Role.RoleName!, 10),
                User = new User_Dto
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role.RoleName ?? ""
                }
            };
            return userToken;
        }
        public async Task<string?> RegisterUser(UserDto userDto)
        {
            var department = await dbContext.Departments.FirstOrDefaultAsync(x => x.DepartmentCode == userDto.DepartmentCode);
            var role = await dbContext.Roles.FirstOrDefaultAsync(x => x.RoleName == userDto.Role);
            if (string.IsNullOrEmpty(userDto.UserName) || string.IsNullOrEmpty(userDto.Password)) return "400";
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    {
                        User user = new User
                        {
                            Name = userDto.Name,
                            UserName = userDto.UserName,
                            Password = userDto.Password,
                            Email = userDto.Email,
                            RoleId = role != null ? role.Id : null,
                            DepartmentId = department!.DepartmentId,
                        };
                        await dbContext.AddAsync(user);
                        await dbContext.SaveChangesAsync();
                        await trans.CommitAsync();
                        return "200";
                    }
                }
                catch (Exception)
                {
                    await trans.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<MessageStatus> UpdateClassWithSubject(UpdateClassDto updateClass)
        {
            var semesterClass = await dbContext.SemestersClass
                .Where(x => x.FK_SubjectId == updateClass.SubjectId && x.SemesterId == updateClass.SemesterId && x.ClassNumber == updateClass.ClassNumber)
                .OrderBy(x => x.ClassNumber).ThenBy(x => x.OnShift)
                .ToListAsync();

            if (updateClass.IsChecked == true)
            {
                var data_0 = semesterClass.Where(x => x.UserId != updateClass.UserId && x.UserId != null).ToList();
                if (data_0.Count > 0)
                {
                    var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == data_0[0].UserId);
                    return new MessageStatus
                    {
                        Message = $"Giảng viên {user?.UserName} đã đăng ký môn",
                        StatusCode = "400",
                    };
                }
                var data_1 = await dbContext.SemestersClass
                    .Where(x => x.SemesterId == updateClass.SemesterId && x.UserId == updateClass.UserId && x.FK_SubjectId != updateClass.SubjectId).OrderBy(x => x.ClassNumber)
                    .ThenBy(x => x.OnShift)
                    .ToListAsync();
                if (data_1.Count > 0)
                {
                    foreach (var item_1 in semesterClass)
                    {
                        foreach (var item_2 in data_1)
                        {
                            if (item_1.WeekDay == item_2.WeekDay)
                            {
                                if ((item_1.OnShift >= item_2.OnShift && item_1.OnShift <= item_2.EndShift)
                                 || (item_1.EndShift >= item_2.OnShift && item_1.EndShift <= item_2.EndShift)
                                )
                                {
                                    return new MessageStatus
                                    {
                                        Message = "Đã bị trùng môn",
                                        StatusCode = "401",
                                        SubjectName = $"{item_2.Name}-Lớp .{item_2.ClassNumber}-Thứ {item_2.WeekDay}-Ca({item_2.OnShift}->{item_2.EndShift})"
                                    };
                                }
                            }
                        }
                    }
                }
                var semesterClass_1 = await dbContext.SemestersClass
                    .Where(x => x.FK_SubjectId == updateClass.SubjectId && x.SemesterId == updateClass.SemesterId)
                    .ToListAsync();
                foreach (var item in semesterClass_1)
                {
                    if (item.ClassNumber == updateClass.ClassNumber)
                    {
                        item.UserId = updateClass.UserId;
                    }
                    else
                    {
                        if (item.UserId == updateClass.UserId) item.UserId = null;
                    }
                }
                dbContext.SemestersClass.UpdateRange(semesterClass);
                await dbContext.SaveChangesAsync();
                return new MessageStatus
                {
                    Message = "Đăng ký môn thành công !!!",
                    StatusCode = "200"
                };
            }
            else if (updateClass.IsChecked == false)
            {
                foreach (var item in semesterClass)
                {
                    if (item.UserId == updateClass.UserId) item.UserId = null;
                }
                dbContext.SemestersClass.UpdateRange(semesterClass);
                await dbContext.SaveChangesAsync();
                return new MessageStatus
                {
                    Message = "Hủy môn thành công !!!",
                    StatusCode = "201"
                };
            }
            return new MessageStatus();
        }

        public async Task<string> DeleteUsers(Guid userId)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user is null) return "400";
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return "200";
        }

        public async Task<string> UpdateUser(UserUpdateDto updateDto)
        {
            var department = await dbContext.Departments.FirstOrDefaultAsync(x => x.DepartmentCode == updateDto.DepartmentCode);
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == updateDto.UserId);
            var role = await dbContext.Roles.FirstOrDefaultAsync(x => x.RoleName == updateDto.Role);
            if (user is not null)
            {
                user.Name = updateDto.Name;
                user.UserName = updateDto.UserName;
                user.Password = updateDto.Password;
                user.Email = updateDto.Email;
                user.RoleId = role!.Id;
                user.DepartmentId = department!.DepartmentId;
                dbContext.Update(user);
                await dbContext.SaveChangesAsync();
                return "200";
            }
            return "400";
        }

        public async Task<string> DeleteUserSubject(DeleteUserSubject subject)
        {
            var usersubject = await dbContext.UserSubjects
                .FirstOrDefaultAsync(x => x.FK_SubjectId == subject.SubjectId && x.UserId == subject.UserId);
            if (usersubject is not null)
            {
                dbContext.Remove(usersubject);
                await dbContext.SaveChangesAsync();
                return "200";
            }
            return "400";
        }

        public async Task<List<UserSubjectDto>> GetSubjectNoUser(Guid id)
        {
            var allSubjects = await dbContext.Subjects.Select(s => s.SubjectId).ToListAsync();
            var userSubjects = await dbContext.UserSubjects.Where(us => us.UserId == id).Select(us => us.FK_SubjectId).ToListAsync();

            var missingSubjects = await dbContext.Subjects
                .Where(s => allSubjects.Contains(s.SubjectId) && !userSubjects.Contains(s.SubjectId))
                .Select(s => new UserSubjectDto
                {
                    SubjectId = s.SubjectId,
                    SubjectName = s.SubjectName,
                })
                .ToListAsync();
            return missingSubjects;
        }

        public async Task<string> AddRangeUserSubject(List<DeleteUserSubject> subjects)
        {
            if (subjects.Count > 0)
            {
                var usersubjects = new List<UserSubject>();
                foreach (var item in subjects)
                {
                    var usersubject = new UserSubject
                    {
                        FK_SubjectId = item.SubjectId,
                        UserId = item.UserId,
                    };
                    usersubjects.Add(usersubject);
                }
                await dbContext.UserSubjects.AddRangeAsync(usersubjects);
                await dbContext.SaveChangesAsync();
                return "200";
            }
            return "400";
        }

        public async Task<List<UserClassDto_2>> GetClassWithUser_1(Guid userId, string semesterName)
        {
            var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.Name == semesterName);
            if (semester is not null)
            {
                var semesterClass = await dbContext.SemestersClass
                    .Where(x => x.SemesterId == semester.SemesterId && x.UserId == userId)
                    .Select(x =>
                       new UserClassDto_2
                       {
                           Id = x.SemesterClass_Id,
                           Name = x.Name,
                           ClassNumber = x.ClassNumber,
                           Classroom = x.Classroom,
                           WeekDay = x.WeekDay,
                           OnShift = x.OnShift,
                           EndShift = x.EndShift,
                           Describe = x.Describe
                       }
                    ).ToListAsync();
                return semesterClass;
            }
            return [];
        }

        public async Task<List<UserClassDto_1>> GetClassWithUser_2(SemesterClassDto semesterClass)
        {
            var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.Name == semesterClass.SemesterName);
            var semesterClasses = await dbContext.SemestersClass
                .Where(x => x.SemesterId == semester!.SemesterId && x.UserId == semesterClass.UserId)
                .Include(x => x.User)
                .Select(x =>
                   new UserClassDto_1
                   {
                       SubjectId = x.FK_SubjectId,
                       Name = x.Name,
                       ClassNumber = x.ClassNumber,
                       Classroom = x.Classroom,
                       WeekDay = x.WeekDay,
                       OnShift = x.OnShift,
                       EndShift = x.EndShift,
                       Describe = x.Describe,
                       UserName = x.User != null ? x.User.UserName : null,
                       Hours = SoGioTuongUng(x.OnShift),
                       Hours_1 = SoGioTuongUng(x.EndShift)
                   }
                ).ToListAsync();
            return semesterClasses;
        }
    }
}
