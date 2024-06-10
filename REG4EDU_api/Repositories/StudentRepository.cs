using Microsoft.EntityFrameworkCore;
using REG4EDU_api.Authentication;
using REG4EDU_api.Data;
using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto;
using REG4EDU_api.Model.Dto.GetStudent;
using REG4EDU_api.Model.Dto.SemesterClass;
using REG4EDU_api.Model.Dto.StudentSubjects;

namespace REG4EDU_api.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DevDbContext dbContext;
        private readonly IJwtToken jwtToken;

        public StudentRepository(DevDbContext dbContext, IJwtToken jwtToken)
        {
            this.dbContext = dbContext;
            this.jwtToken = jwtToken;
        }

        public async Task<List<UserSubjectDto>> GetStudentSubject(StudentSubjectDto studentSubject)
        {
            var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.Status);
            if (semester == null) return [];
            var studentSubjects = await dbContext.SemesterSubjects
                .Where(x => x.SemesterId == semester.SemesterId)
                .Join(
                  dbContext.MajorsSubjects,
                  x => x.FK_SubjectId,
                  y => y.FK_SubjectId,
                  (x, y) => y)
                .Where(x => x.MajorsId == studentSubject.MajorsId)
                .OrderBy(s => s.Category)
                .Where(s => (s.Subject!.ParentId == null || dbContext.StudentsSubjects.Any(ss => ss.StudentId == studentSubject.StudentId && ss.SubjectId == s.Subject.ParentId && ss.Complete == true))
                 && !dbContext.StudentsSubjects.Any(ss => ss.StudentId == studentSubject.StudentId && ss.SubjectId == s.Subject!.SubjectId && ss.Complete == true))
                .Select(x =>
                  new UserSubjectDto
                  {
                      SubjectId = x.FK_SubjectId,
                      SubjectName = x.Subject!.SubjectName,
                  })
                .ToListAsync();
            return studentSubjects ?? [];
        }

        public async Task<List<GroupClassDto>> GroupClassStudent(StudentClassDto studentClass)
        {
            var semesterClassSubject = await dbContext.SemestersClass
             .Where(x => x.FK_SubjectId == studentClass.SubjectId && x.SemesterId == studentClass.SemesterId)
             .OrderBy(x => x.ClassNumber).ThenBy(x => x.WeekDay).ThenBy(x => x.OnShift)
             .ToListAsync();
            var semesterClassIds = semesterClassSubject.Select(x => x.SemesterClass_Id).ToList();
            var studentClasses = await dbContext.StudentClasses
                .Where(x => semesterClassIds.Contains(x.SemesterClass_Id))
                .Include(x => x.SemesterClass)
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
                    if (studentClasses.Any(x => x.StudentId == studentClass.StudentId && x.SemesterClass!.ClassNumber == group.Key)) data.IsChecked = true;
                    var myNum = await GetNumberStudent(myClass.SemesterClass_Id);
                    data.Name += $"{myClass.Name}" + $"{(myClass.Describe != null ? $"_{myClass.Describe}" : "")}-" +
                    $"Thứ_{myClass.WeekDay}-" + $"Ca({myClass.OnShift}-{myClass.EndShift})-" + $"{myNum}/{myClass.NumberStudent} ";
                }
                groupClass.Add(data);
            }
            return groupClass ?? [];
        }

        public async Task<StudentToken?> LoginStudent(UserLogin userLogin)
        {
            Student? student = await dbContext.Students.FirstOrDefaultAsync(x => x.UserName == userLogin.Username);
            if (student is null) return null;
            if (student.Password != userLogin.Password) return null;
            var studentToken = new StudentToken()
            {
                Access_token = jwtToken.GenerateToken(student.StudentId, "student", 1),
                Refresh_token = jwtToken.GenerateToken(student.StudentId, "student", 10),
                User = student
            };
            return studentToken;
        }

        public async Task<MessageStatus> UpdateStudentClass(UpdateClassDto updateClass)
        {
            var studentSubjects = await dbContext.StudentsSubjects
                .FirstOrDefaultAsync(x => x.StudentId == updateClass.UserId && x.SubjectId == updateClass.SubjectId
                );
            var studentClasses = await dbContext.StudentClasses
              .Where(x => x.StudentId == updateClass.UserId
                && x.SemesterClass!.SemesterId == updateClass.SemesterId
                && x.SemesterClass.FK_SubjectId == updateClass.SubjectId)
              .Include(x => x.SemesterClass)
              .ToListAsync();
            var semesterClasses = await dbContext.SemestersClass
              .Where(x => x.FK_SubjectId == updateClass.SubjectId
                  && x.ClassNumber == updateClass.ClassNumber
                  && x.SemesterId == updateClass.SemesterId)
               .ToListAsync();

            if (updateClass.IsChecked == true)
            {
                foreach (var myClass in semesterClasses)
                {
                    var myNum = await GetNumberStudent(myClass.SemesterClass_Id);
                    if (myNum >= myClass.NumberStudent)
                    {
                        return new MessageStatus
                        {
                            Message = $"Lớp đã đầy",
                            StatusCode = "400",
                        };
                    }
                }
                var studentClass = await dbContext.StudentClasses
                 .Where(x => x.StudentId == updateClass.UserId
                   && x.SemesterClass!.SemesterId == updateClass.SemesterId
                   && x.SemesterClass.FK_SubjectId != updateClass.SubjectId)
                 .Include(x => x.SemesterClass)
                 .ToListAsync();
                foreach (var newClass in semesterClasses)
                {
                    foreach (var existingClass in studentClass)
                    {
                        if (newClass.WeekDay == existingClass.SemesterClass!.WeekDay &&
                            ((newClass.OnShift >= existingClass.SemesterClass.OnShift && newClass.OnShift <= existingClass.SemesterClass.EndShift) ||
                            (newClass.EndShift >= existingClass.SemesterClass.OnShift && newClass.EndShift <= existingClass.SemesterClass.EndShift)))
                        {
                            return new MessageStatus
                            {
                                Message = "Đã bị trùng môn",
                                StatusCode = "401",
                                SubjectName = $"{existingClass.SemesterClass.Name}-Lớp {existingClass.SemesterClass.ClassNumber}-Thứ {existingClass.SemesterClass.WeekDay}-Ca({existingClass.SemesterClass.OnShift}->{existingClass.SemesterClass.EndShift})"
                            };
                        }
                    }
                }
                if (studentClasses.Count == 0 && studentSubjects is null)
                {
                    var studentSubject = new StudentSubject
                    {
                        StudentId = updateClass.UserId,
                        SubjectId = updateClass.SubjectId,
                    };
                    await dbContext.StudentsSubjects.AddAsync(studentSubject);
                }
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        dbContext.StudentClasses.RemoveRange(studentClasses);
                        await dbContext.SaveChangesAsync();
                        var newStudentClasses = semesterClasses.Select(x => new StudentClass
                        {
                            StudentId = updateClass.UserId,
                            SemesterClass_Id = x.SemesterClass_Id
                        });
                        await dbContext.AddRangeAsync(newStudentClasses);
                        await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return new MessageStatus
                        {
                            Message = "Đăng ký môn thành công !!!",
                            StatusCode = "200"
                        };
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            else if (updateClass.IsChecked == false)
            {
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (studentSubjects is not null) dbContext.StudentsSubjects.Remove(studentSubjects);
                        dbContext.StudentClasses.RemoveRange(studentClasses);
                        await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return new MessageStatus
                        {
                            Message = "Hủy môn thành công !!!",
                            StatusCode = "201"
                        };
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }

            }
            return new MessageStatus();
        }
        public async Task<int> GetNumberStudent(Guid id)
        {
            return await dbContext.StudentClasses.CountAsync(sc => sc.SemesterClass_Id == id);
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
        public async Task<List<UserClassDto_1>> GetClassWithStudent(Guid studentId)
        {
            var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.Status);
            if (semester is not null)
            {
                var studentClass = await dbContext.StudentClasses
                    .Where(x => x.StudentId == studentId && x.SemesterClass!.SemesterId == semester.SemesterId)
                    .Include(x => x.SemesterClass)
                    .ThenInclude(x => x!.User)
                    .Select(x =>
                       new UserClassDto_1
                       {
                           SubjectId = x.SemesterClass!.FK_SubjectId,
                           Name = x.SemesterClass!.Name,
                           ClassNumber = x.SemesterClass!.ClassNumber,
                           Classroom = x.SemesterClass!.Classroom,
                           WeekDay = x.SemesterClass!.WeekDay,
                           OnShift = x.SemesterClass!.OnShift,
                           EndShift = x.SemesterClass!.EndShift,
                           Describe = x.SemesterClass!.Describe,
                           UserName = x.SemesterClass!.User!.UserName,
                           Hours = SoGioTuongUng(x.SemesterClass.OnShift),
                           Hours_1 = SoGioTuongUng(x.SemesterClass.EndShift)
                       }
                    )
                    .ToListAsync();
                return studentClass;
            }
            return [];
        }

        public async Task<List<GetStudentDto>> GetStudentWithPagination(int current, int pageSize, string? order, string? field, string? email, string? userName)
        {
            var query = dbContext.Students
               .Where(x =>
                  (string.IsNullOrWhiteSpace(userName) || x.UserName == userName) &&
                  (string.IsNullOrWhiteSpace(email) || x.Email == email)
               ).OrderBy(x => x.StudentId);
            var skipResults = (current - 1) * pageSize;
            var students = await query.Skip(skipResults).Take(pageSize).Include(x => x.Majors).ToListAsync();
            if (!string.IsNullOrWhiteSpace(field) && !string.IsNullOrWhiteSpace(order))
            {
                students = field.ToLower() switch
                {
                    "email" => order.ToLower() == "ascend" ? students.OrderBy(x => x.Email).ToList() : students.OrderByDescending(x => x.Email).ToList(),
                    "userName" => order.ToLower() == "ascend" ? students.OrderBy(x => x.UserName).ToList() : students.OrderByDescending(x => x.UserName).ToList(),
                    _ => students
                };
            }
            return students.Select(x =>
                   new GetStudentDto
                   {
                       StudentId = x.StudentId,
                       NumberOfCredits = x.NumberOfCredits,
                       UserName = x.UserName,
                       Email = x.Email,
                       Password = x.Password,
                       Status = x.Status,
                       MajorsCode = x.Majors?.MajorsCode
                   }
            ).ToList();
        }

        public async Task<string> UpdateStudent(UpdateStudentDto updateStudent)
        {
            var major = await dbContext.Majors
                .FirstOrDefaultAsync(x => x.MajorsCode == updateStudent.MajorsCode);
            var student = await dbContext.Students
                .FirstOrDefaultAsync(x => x.StudentId == updateStudent.StudentId);
            if (student is not null)
            {
                student.UserName = updateStudent.UserName;
                student.Email = updateStudent.Email;
                student.Password = updateStudent.Password;
                student.Status = updateStudent.Status;
                student.MajorsId = major!.MajorsId;
                dbContext.Update(student);
                await dbContext.SaveChangesAsync();
                return "200";
            }
            return "400";
        }

        public async Task<string> DeleteStudent(Guid studentId)
        {
            var student = await dbContext.Students
                .FirstOrDefaultAsync(x => x.StudentId == studentId);
            if (student is not null)
            {
                dbContext.Remove(student);
                await dbContext.SaveChangesAsync();
                return "200";
            }
            return "400";
        }

        public async Task<string> CreateStudent(CreateStudentDto createStudent)
        {
            var major = await dbContext.Majors
                .FirstOrDefaultAsync(x => x.MajorsCode == createStudent.MajorsCode);
            var student = new Student
            {
                UserName = createStudent.UserName,
                Email = createStudent.Email,
                Password = createStudent.Password,
                MajorsId = major!.MajorsId
            };
            await dbContext.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return "200";
        }

        public async Task<List<StudentSubjectDto_1>> GetSubjectWithStudent(StudentSubjectDto_2 studentSubject)
        {
            var semester = await dbContext.Semesters
                .FirstOrDefaultAsync(x => x.Name == studentSubject.SemesterName);
            var studentClasses = await dbContext.StudentClasses
                .Where(x => x.StudentId == studentSubject.StudentId &&
                   x.SemesterClass!.SemesterId == semester!.SemesterId
                )
                .Include(x => x.SemesterClass)
                .ThenInclude(y => y!.Subject)
                .ToListAsync();
            var distinctList = studentClasses.GroupBy(x => x.SemesterClass!.FK_SubjectId)
                .Select(g => g.First())
                .ToList();
            return distinctList.Select(x =>
               new StudentSubjectDto_1
               {
                   SubjectId = x.SemesterClass?.FK_SubjectId,
                   SubjectName = x.SemesterClass!.Subject!.SubjectName,
                   ClassNumber = x.SemesterClass!.ClassNumber,
               }
            ).ToList();
        }

        public async Task<GetStudentResDto?> GetStudentWithId(string studentName)
        {
            var student = await dbContext.Students
                .FirstOrDefaultAsync(x => x.UserName == studentName);
            if (student is not null)
            {
                var result = new GetStudentResDto()
                {
                    StudentId = student.StudentId,
                    UserName = student.UserName,
                };
                return result;
            }
            return null;
        }

        public async Task<List<UserClassDto_1>> GetClassWithStudent_1(SemesterClassDto semesterClass)
        {
            var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.Name == semesterClass.SemesterName);
            if (semester is not null)
            {
                var studentClass = await dbContext.StudentClasses
                    .Where(x => x.StudentId == semesterClass.UserId && x.SemesterClass!.SemesterId == semester.SemesterId)
                    .Include(x => x.SemesterClass)
                    .ThenInclude(x => x!.User)
                    .Select(x =>
                       new UserClassDto_1
                       {
                           SubjectId = x.SemesterClass!.FK_SubjectId,
                           Name = x.SemesterClass!.Name,
                           ClassNumber = x.SemesterClass!.ClassNumber,
                           Classroom = x.SemesterClass!.Classroom,
                           WeekDay = x.SemesterClass!.WeekDay,
                           OnShift = x.SemesterClass!.OnShift,
                           EndShift = x.SemesterClass!.EndShift,
                           Describe = x.SemesterClass!.Describe,
                           UserName = x.SemesterClass!.User!.UserName,
                           Hours = SoGioTuongUng(x.SemesterClass.OnShift),
                           Hours_1 = SoGioTuongUng(x.SemesterClass.EndShift)
                       }
                    )
                    .ToListAsync();
                return studentClass;
            }
            return [];
        }

        public async Task<string> UpdateStudentSubject(StudentSubjectsDto subjectsDto)
        {
            var studentSubject = await dbContext.StudentsSubjects
                .FirstOrDefaultAsync(x => x.SubjectId == subjectsDto.SubjectId
                && x.StudentId == subjectsDto.StudentId
                );
            if (studentSubject is not null)
            {
                studentSubject.Complete = true;
                dbContext.StudentsSubjects.Update(studentSubject);
                await dbContext.SaveChangesAsync();
                return "200";
            }
            return "400";
        }
    }
}