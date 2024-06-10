using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        public string? RoleName { get; set; }
        public string? CreatorName { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
