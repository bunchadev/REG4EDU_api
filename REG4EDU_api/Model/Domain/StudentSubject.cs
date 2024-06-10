using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class StudentSubject
    {
        [Key]
        public Guid StudentSubject_Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public bool Complete { get; set; } = false;
        public string? Status { get; set; } = "ĐH";
        public double? Scores { get; set; }
        public Student? Student { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
