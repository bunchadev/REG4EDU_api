using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class Student
    {
        [Key]
        public Guid StudentId { get; set; }
        public string? UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Password { get; set; } = null!;
        public int NumberOfCredits { get; set; } = 0;
        public string? Status { get; set; } = "BT";
        public Guid MajorsId { get; set; }
        public Majors? Majors { get; set; }
    }
}
