using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class StudentClass
    {
        [Key]
        public Guid StudentClass_Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid SemesterClass_Id { get; set; }
        public Student? Student { get; set; }
        public SemesterClass? SemesterClass { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
