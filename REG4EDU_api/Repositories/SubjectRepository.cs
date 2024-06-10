using Microsoft.EntityFrameworkCore;
using REG4EDU_api.Data;
using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto;
using REG4EDU_api.Model.Dto.GetStudent;
using REG4EDU_api.Model.Dto.GroupClass;

namespace REG4EDU_api.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DevDbContext dbContext;

        public SubjectRepository(DevDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Subject?> CreateSubject(CreateSubject createSubject)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var major = await dbContext.Majors.FirstOrDefaultAsync(x => x.MajorsCode == createSubject.MajorsCode);
                if (major is null)
                {
                    throw new Exception("Không tìm thấy Major tương ứng");
                }
                var subject = new Subject
                {
                    SubjectName = createSubject.SubjectName,
                    NumberOfCredits = createSubject.NumberOfCredits,
                    SubjectCode = createSubject.SubjectCode,
                };
                dbContext.Subjects.Add(subject);
                await dbContext.SaveChangesAsync();
                var majorsSubject = new MajorsSubject
                {
                    MajorsId = major.MajorsId,
                    FK_SubjectId = subject.SubjectId,
                    Category = createSubject.Category,
                };
                dbContext.MajorsSubjects.Add(majorsSubject);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return subject;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task<string> DeleteSubject(List<Guid> id)
        {

            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var SubjectMajors = await dbContext.MajorsSubjects
                                .Where(x => id.Contains(x.MajorsSubject_Id))
                                .ExecuteUpdateAsync(y => y.SetProperty(x => x.IsActive, false));
                await transaction.CommitAsync();
                return "200";
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<string> DeleteSubjectSemester(Guid id)
        {
            var SemesterSubject = await dbContext
                .SemesterSubjects
                .FirstOrDefaultAsync(x => x.SemesterSubject_Id == id);
            if (SemesterSubject is not null)
            {
                using (var trans = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        dbContext.SemesterSubjects.Remove(SemesterSubject);
                        await dbContext.SaveChangesAsync();
                        await trans.CommitAsync();
                        return "200";
                    }
                    catch (Exception)
                    {
                        await trans.RollbackAsync();
                        throw;
                    }
                }
            }
            return "400";
        }

        public async Task<List<Subject>> GetAllSubject()
        {
            IQueryable<Subject> query = dbContext.Subjects.AsQueryable();
            List<Subject> subjects = await query.ToListAsync();
            return subjects;
        }
        public async Task<List<SubjectMajorDto>> GetAllSubjectWithMajor()
        {
            IQueryable query = dbContext.MajorsSubjects.AsQueryable();
            List<MajorsSubject> majorsSubjects = await dbContext.MajorsSubjects.Include(m => m.Majors).Include(s => s.Subject).ToListAsync();
            var subjectM = majorsSubjects.Where(x => x.IsActive == true).Select(m =>
            new SubjectMajorDto
            {
                MajorsId = m.MajorsSubject_Id,
                SubjectId = m.FK_SubjectId,
                SubjectName = m.Subject!.SubjectName,
                NumberOfCredits = m.Subject.NumberOfCredits,
                MajorName = m.Majors!.MajorsCode,
                SubjectCode = m.Subject.SubjectCode,
                Category = m.Category,
                CreatedAt = m.Subject.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
            }).ToList();
            return subjectM;
        }
        public async Task<List<SubjectMajorDto>> GetSubjectPagination(int current, int pageSize, string? order, string? field, string? subjectName, string? majorName, int numberOfCredits, string? subjectCode, string? category)
        {
            var query = dbContext.MajorsSubjects
                .Include(m => m.Majors)
                .Include(s => s.Subject)
                .Where(x =>
                    (string.IsNullOrWhiteSpace(subjectName) || x.Subject!.SubjectName == subjectName) &&
                    (string.IsNullOrWhiteSpace(majorName) || x.Majors!.MajorsCode == majorName) &&
                    (numberOfCredits <= 0 || x.Subject!.NumberOfCredits == numberOfCredits) &&
                    (string.IsNullOrWhiteSpace(subjectCode) || x.Subject!.SubjectCode == subjectCode) &&
                    (string.IsNullOrWhiteSpace(category) || x.Category == category) && x.IsActive
                ).OrderBy(x => x.MajorsSubject_Id);
            var skipResults = (current - 1) * pageSize;
            var majorsSubjects = await query.Skip(skipResults).Take(pageSize).ToListAsync();
            if (!string.IsNullOrWhiteSpace(field) && !string.IsNullOrWhiteSpace(order))
            {
                majorsSubjects = field.ToLower() switch
                {
                    "subjectname" => order.ToLower() == "ascend" ? majorsSubjects.OrderBy(x => x.Subject!.SubjectName).ToList() : majorsSubjects.OrderByDescending(x => x.Subject!.SubjectName).ToList(),
                    "numberofcredits" => order.ToLower() == "ascend" ? majorsSubjects.OrderBy(x => x.Subject!.NumberOfCredits).ToList() : majorsSubjects.OrderByDescending(x => x.Subject!.NumberOfCredits).ToList(),
                    "createdat" => order.ToLower() == "ascend" ? majorsSubjects.OrderBy(x => x.Subject!.CreatedAt).ToList() : majorsSubjects.OrderByDescending(x => x.Subject!.CreatedAt).ToList(),
                    _ => majorsSubjects
                };
            }
            return majorsSubjects
                .Select(m => new SubjectMajorDto
                {
                    MajorsId = m.MajorsSubject_Id,
                    SubjectId = m.FK_SubjectId,
                    SubjectName = m.Subject!.SubjectName,
                    NumberOfCredits = m.Subject.NumberOfCredits,
                    MajorName = m.Majors!.MajorsCode,
                    SubjectCode = m.Subject.SubjectCode,
                    Category = m.Category,
                    CreatedAt = m.Subject.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                })
                .ToList();
        }

        public async Task<List<SubjectSemesterDto>> GetSubjectSemesters(string? semesterName, string? majorsCode, string? category, string? order, string? field, string? subjectCode)
        {
            var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.Name == semesterName);
            if (semesterName is not null && majorsCode is not null && category is not null)
            {
                var query = await dbContext.MajorsSubjects
                .Where(x => x.Category == category && x.Majors!.MajorsCode == majorsCode)
                .Join(
                   dbContext.SemesterSubjects,
                   ms => ms.FK_SubjectId,
                   ss => ss.FK_SubjectId,
                   (ms, ss) => ss)
                .Where(x => (x.SemesterId == semester!.SemesterId) && (string.IsNullOrWhiteSpace(subjectCode) || x.Subject!.SubjectCode == subjectCode))
                .Include(x => x.Subject)
                .Select(x =>
                  new SubjectSemesterDto
                  {
                      SemesterSubject_Id = x.SemesterSubject_Id,
                      SemesterId = x.SemesterId,
                      SubjectId = x.FK_SubjectId,
                      SemesterName = semesterName ?? "",
                      SubjectCode = x.Subject!.SubjectCode,
                      SubjectName = x.Subject.SubjectName,
                      CreatedAt = x.Subject.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                  })
                .ToListAsync();
                if (!string.IsNullOrWhiteSpace(field) && !string.IsNullOrWhiteSpace(order))
                {
                    query = field.ToLower() switch
                    {
                        "subjectname" => order.ToLower() == "ascend" ? query.OrderBy(x => x.SubjectName).ToList() : query.OrderByDescending(x => x.SubjectName).ToList(),
                        _ => query
                    };
                }
                return query;
            }
            return [];
        }
        public async Task<int> GetNumberStudent(Guid id)
        {
            return await dbContext.StudentClasses.CountAsync(sc => sc.SemesterClass_Id == id);
        }
        public async Task<List<GroupClassResDto>> GroupClass(GroupClassDto_1 groupClass)
        {
            var semesterClassSubject = await dbContext.SemestersClass
             .Where(x => x.FK_SubjectId == groupClass.SubjectId && x.SemesterId == groupClass.SemesterId)
             .OrderBy(x => x.ClassNumber).ThenBy(x => x.WeekDay).ThenBy(x => x.OnShift)
             .ToListAsync();
            var semesterClassIds = semesterClassSubject.Select(x => x.SemesterClass_Id).ToList();
            var groupSemesterClass = semesterClassSubject
              .GroupBy(x => x.ClassNumber);
            var groupClasses = new List<GroupClassResDto>();
            foreach (var group in groupSemesterClass)
            {
                var data = new GroupClassResDto()
                {
                    Id = Guid.NewGuid(),
                    ClassNumber = group.Key,
                    Name = "",
                };
                foreach (var myClass in group)
                {
                    var myNum = await GetNumberStudent(myClass.SemesterClass_Id);
                    data.Name += $"{myClass.Name}" + $"{(myClass.Describe != null ? $"_{myClass.Describe}" : "")}-" +
                    $"Thứ_{myClass.WeekDay}-" + $"Ca({myClass.OnShift}-{myClass.EndShift})-" + $"{myNum}/{myClass.NumberStudent} ";
                }
                groupClasses.Add(data);
            }
            return groupClasses ?? [];
        }

        public async Task<Subject?> UpdateSubject(UpdateSubjectDto subjectDto)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var subject = new Subject
                    {
                        SubjectId = subjectDto.SubjectId,
                        SubjectName = subjectDto.SubjectName,
                        NumberOfCredits = subjectDto.NumberOfCredits,
                        SubjectCode = subjectDto.SubjectCode ?? "ABCDEF",
                        CreatedAt = DateTime.Now,
                    };
                    if (subjectDto.Check1 == true)
                    {
                        var majorSubject = await dbContext.MajorsSubjects.FirstOrDefaultAsync(x => x.MajorsSubject_Id == subjectDto.MajorsId);
                        if (majorSubject is not null)
                        {
                            majorSubject.Category = subjectDto.Category;
                            if (subjectDto.Check2 == true)
                            {
                                var majors = await dbContext.Majors.FirstOrDefaultAsync(x => x.MajorsCode == subjectDto.MajorName);
                                if (majors is not null) majorSubject.MajorsId = majors.MajorsId;
                            }
                            dbContext.MajorsSubjects.Update(majorSubject);
                        }
                    }
                    dbContext.Subjects.Update(subject);
                    await dbContext.SaveChangesAsync();
                    await trans.CommitAsync();
                    return subject;
                }
                catch (Exception)
                {
                    await trans.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<List<GetStudentResDto>> GetStudentWithClass(GetStudentWithClassDto getStudent)
        {
            var semesterClass = await dbContext.SemestersClass
                .FirstOrDefaultAsync(
                  x =>
                 x.FK_SubjectId == getStudent.SubjectId &&
                 x.SemesterId == getStudent.SemesterId &&
                 x.ClassNumber == getStudent.ClassNumber
                );
            if (semesterClass is not null)
            {
                var students = await dbContext.StudentClasses
                 .Where(x => x.SemesterClass_Id == semesterClass.SemesterClass_Id
                ).Include(x => x.Student).ThenInclude(x => x!.Majors).Select(x =>
                   new GetStudentResDto
                   {
                       StudentId = x.StudentId,
                       UserName = x.Student!.UserName,
                       MajorName = x.Student.Majors!.MajorsName
                   }
                ).ToListAsync();
                return students;
            }
            return [];
        }
    }
}
