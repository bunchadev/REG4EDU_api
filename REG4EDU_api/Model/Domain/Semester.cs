using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class Semester
    {
        [Key]
        public Guid SemesterId { get; set; }
        public string Name { get; set; } = null!;
        public string? SemesterName { get; set; }
        public int Level { get; set; }
        public bool Status { get; set; } = false;
    }
}
