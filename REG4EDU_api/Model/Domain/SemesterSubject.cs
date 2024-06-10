using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class SemesterSubject
    {
        [Key]
        public Guid SemesterSubject_Id { get; set; }
        public Guid SemesterId { get; set; }
        public Guid FK_SubjectId { get; set; }
        public Semester? Semester { get; set; }
        public Subject? Subject { get; set; }
    }
}
