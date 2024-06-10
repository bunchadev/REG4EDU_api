using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class Subject
    {
        [Key]
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public int NumberOfCredits { get; set; }
        public string? MajorName { get; set; }
        public string SubjectCode { get; set; } = "ABCDEF";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid? ParentId { get; set; }
        public ICollection<SemesterClass>? SemesterClasses { get; set; }
    }
}
