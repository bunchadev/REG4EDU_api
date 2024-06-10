using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class SemesterClass
    {
        [Key]
        public Guid SemesterClass_Id { get; set; }
        public string Name { get; set; } = null!;
        public int ClassNumber { get; set; }
        public string Classroom { get; set; } = null!;
        public int WeekDay { get; set; }
        public int OnShift { get; set; }
        public int EndShift { get; set; }
        public int NumberStudent { get; set; } = 30;
        public int NumStudent { get; set; } = 0;
        public string? Describe { get; set; } = null;
        public Guid SemesterId { get; set; }
        public Guid FK_SubjectId { get; set; }
        public Guid? UserId { get; set; }
        public Subject? Subject { get; set; }
        public Semester? Semester { get; set; }
        public User? User { get; set; }
    }
}
