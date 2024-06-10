using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class Permission
    {
        [Key]
        public Guid Id { get; set; }
        public string? PermissionName { get; set; }
        public string? ApiEndpoint { get; set; }
        public string? Description { get; set; }
        public int Group { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
