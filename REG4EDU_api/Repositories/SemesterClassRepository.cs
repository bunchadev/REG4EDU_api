using Microsoft.EntityFrameworkCore;
using REG4EDU_api.Data;
using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto;

namespace REG4EDU_api.Repositories
{
    public class SemesterClassRepository : ISemesterClassRepository
    {
        private readonly DevDbContext dbContext;

        public SemesterClassRepository(DevDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<MessageStatus> CreateSemesterClass(CreateClassDto semesterClass)
        {
            var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.Name == semesterClass.SemesterName);
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (semester is not null)
                    {
                        var semesterClass_1 = new SemesterClass()
                        {
                            Name = semesterClass.Name,
                            ClassNumber = semesterClass.ClassNumber,
                            Classroom = semesterClass.Classroom,
                            WeekDay = semesterClass.WeekDay,
                            OnShift = semesterClass.OnShift,
                            EndShift = semesterClass.EndShift,
                            Describe = semesterClass.Describe,
                            SemesterId = semester.SemesterId,
                            FK_SubjectId = semesterClass.SubjectId,
                            NumberStudent = semesterClass.NumberStudent,
                        };
                        await dbContext.AddAsync(semesterClass_1);
                        await dbContext.SaveChangesAsync();
                        await trans.CommitAsync();
                        return new MessageStatus
                        {
                            StatusCode = "200",
                            Message = "Tạo mới thành công"
                        };
                    }
                    return new MessageStatus();
                }
                catch (Exception)
                {
                    await trans.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<string> DeleteSemesterClass(Guid semesterClassId)
        {
            var semesterClass = await dbContext.SemestersClass
                .FirstOrDefaultAsync(x => x.SemesterClass_Id == semesterClassId);
            if (semesterClass is not null)
            {
                dbContext.SemestersClass.Remove(semesterClass);
                await dbContext.SaveChangesAsync();
                return "200";
            }
            return "400";
        }

        public async Task<List<SemesterClassSubject>> GetSemesterClass(SemesterSubjectDto_3 semesterClass)
        {
            var semesterClassSubject = await dbContext.SemestersClass
              .Where(x => x.FK_SubjectId == semesterClass.SubjectId && x.SemesterId == semesterClass.SemesterId)
              .Include(u => u.User)
              .Select(x => new SemesterClassSubject
              {
                  SemesterClass_Id = x.SemesterClass_Id,
                  Name = x.Name,
                  ClassNumber = x.ClassNumber,
                  Classroom = x.Classroom,
                  WeekDay = x.WeekDay,
                  OnShift = x.OnShift,
                  EndShift = x.EndShift,
                  NumberStudent = x.NumberStudent,
                  Number = 0,
                  Describe = x.Describe ?? ".",
                  SemesterId = x.SemesterId,
                  SubjectId = x.FK_SubjectId,
                  UserId = x.UserId,
                  UserName = x.User.UserName
              }).OrderBy(x => x.ClassNumber).ToListAsync();
            var semesterClassIds = semesterClassSubject.Select(x => x.SemesterClass_Id).ToList();
            var studentSubjectCounts = await dbContext.StudentClasses
                .Where(ss => semesterClassIds.Contains(ss.SemesterClass_Id))
                .GroupBy(ss => ss.SemesterClass_Id)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
            foreach (var subject in semesterClassSubject)
            {
                if (studentSubjectCounts.TryGetValue(subject.SemesterClass_Id, out int count))
                {
                    subject.Number = count;
                }
            }
            return semesterClassSubject;
        }

        public async Task<MessageStatus> UpdateSemesterClass(SemesterClassSubject semesterClass)
        {
            var semesterClass_1 = await dbContext.SemestersClass.FirstOrDefaultAsync(x => x.SemesterClass_Id == semesterClass.SemesterClass_Id);
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserName == semesterClass.UserName);
            if (semesterClass.UserId is not null)
            {
                var data_1 = await dbContext.SemestersClass
                    .Where(x => x.SemesterId == semesterClass.SemesterId && x.UserId == semesterClass.UserId && x.SemesterClass_Id != semesterClass.SemesterClass_Id).OrderBy(x => x.ClassNumber)
                    .ThenBy(x => x.OnShift)
                    .ToListAsync();
                if (data_1.Count > 0)
                {
                    foreach (var item_2 in data_1)
                    {
                        if (semesterClass.WeekDay == item_2.WeekDay)
                        {
                            if ((semesterClass.OnShift >= item_2.OnShift && semesterClass.OnShift <= item_2.EndShift)
                             || (semesterClass.EndShift >= item_2.OnShift && semesterClass.EndShift <= item_2.EndShift)
                            )
                            {
                                return new MessageStatus
                                {
                                    Message = "Đã bị trùng môn",
                                    StatusCode = "400",
                                    SubjectName = $"{item_2.Name}-Lớp .{item_2.ClassNumber}-Thứ {item_2.WeekDay}-Ca({item_2.OnShift}->{item_2.EndShift})"
                                };
                            }
                        }
                    }
                }
            }
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    semesterClass_1!.Name = semesterClass.Name;
                    semesterClass_1.ClassNumber = semesterClass.ClassNumber;
                    semesterClass_1.Classroom = semesterClass.Classroom;
                    semesterClass_1.WeekDay = semesterClass.WeekDay;
                    semesterClass_1.OnShift = semesterClass.OnShift;
                    semesterClass_1.EndShift = semesterClass.EndShift;
                    semesterClass_1.NumberStudent = semesterClass.NumberStudent;
                    semesterClass_1.UserId = user is not null ? user.UserId : null;
                    dbContext.SemestersClass.Update(semesterClass_1);
                    await dbContext.SaveChangesAsync();
                    await trans.CommitAsync();
                    return new MessageStatus
                    {
                        StatusCode = "200",
                        Message = "Cập nhật thành công"
                    };
                }
                catch (Exception)
                {
                    await trans.RollbackAsync();
                    throw;
                }
            }

        }
    }
}
