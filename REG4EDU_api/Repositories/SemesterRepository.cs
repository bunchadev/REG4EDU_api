using Microsoft.EntityFrameworkCore;
using REG4EDU_api.Data;
using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto;

namespace REG4EDU_api.Repositories
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly DevDbContext dbContext;

        public SemesterRepository(DevDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateSemesterSubject(SemesterSubjectDto semesterSubject)
        {
            var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.Name == semesterSubject.SemesterName);
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (semesterSubject.SubjectId.Any() && semester is not null)
                    {
                        var data = semesterSubject.SubjectId.Select(x =>
                             new SemesterSubject
                             {
                                 SemesterId = semester!.SemesterId,
                                 FK_SubjectId = x
                             }
                        ).ToList();
                        await dbContext.SemesterSubjects.AddRangeAsync(data);
                        await dbContext.SaveChangesAsync();
                        await trans.CommitAsync();
                        return "200";
                    }
                    return "400";
                }
                catch (Exception)
                {
                    await trans.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<List<Semester>> GetAllSemester()
        {
            IQueryable<Semester> query = dbContext.Semesters.AsQueryable();
            List<Semester> semesters = await query.ToListAsync();
            if (semesters.Any()) return semesters;
            return [];
        }

        public async Task<Semester> GetSemester()
        {
            var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.Status);
            return semester ?? new Semester();
        }

        public async Task<List<SemesterSubjectDto_1>> GetSemesterSubjectMajors(SemesterSubjectDto_2 semesterSubject)
        {
            var subjects = await dbContext.MajorsSubjects
                .Where(x => x.Category == semesterSubject.Category && x.Majors!.MajorsCode == semesterSubject.MajorsCode && x.IsActive)
                .GroupJoin(
                  dbContext.SemesterSubjects,
                  x => x.FK_SubjectId,
                  y => y.FK_SubjectId,
                  (x, y) => new { MajorsSubject_1 = x, SemesterSubject = y })
                .SelectMany(
                   xy => xy.SemesterSubject.DefaultIfEmpty(),
                   (a, b) => new { MajorsSubject = a.MajorsSubject_1, SemesterSubject = b }
                 )
                .Where(xy =>
                   xy.SemesterSubject == null ||
                   xy.SemesterSubject.Semester == null ||
                   xy.SemesterSubject.Semester.Name != semesterSubject.SemesterName
                 )
                .Select(xy =>
                  new SemesterSubjectDto_1
                  {
                      SubjectId = xy.MajorsSubject.FK_SubjectId,
                      SubjectName = xy.MajorsSubject.Subject!.SubjectName
                  }
                 )
                .ToListAsync();
            return subjects ?? [];
        }

        public async Task<Semester> GetSemesterWithCode(string semesterName)
        {
            var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.Name == semesterName);
            if (semester is not null) return semester;
            return new Semester();
        }

        public async Task<string> UpdateSemester(string semesterName)
        {
            var semesters = await dbContext.Semesters.ToListAsync();
            foreach (var semester in semesters)
            {
                if (semester.Name == semesterName)
                {
                    if (semester.Status == true)
                    {
                        semester.Status = false;
                    }
                    else
                    {
                        semester.Status = true;
                    }
                }
                else semester.Status = false;
            }
            dbContext.Semesters.UpdateRange(semesters);
            await dbContext.SaveChangesAsync();
            return "200";
        }
    }
}
