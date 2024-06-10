using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class MajorsSubject
    {
        [Key]
        public Guid MajorsSubject_Id { get; set; }
        public string? Category { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid MajorsId { get; set; }
        public Guid FK_SubjectId { get; set; }
        public Majors? Majors { get; set; }
        public Subject? Subject { get; set; }
    }
}
