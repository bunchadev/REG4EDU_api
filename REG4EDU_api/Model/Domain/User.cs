using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid? RoleId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Role? Role { get; set; }
        public Department? Department { get; set; }
    }
}
