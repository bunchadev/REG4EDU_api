using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class RolePermission
    {
        [Key]
        public Guid RolePermission_Id { get; set; }
        public Guid Role_Id { get; set; }
        public Guid Permission_Id { get; set; }
        public Role? Role { get; set; }
        public Permission? Permission { get; set; }
    }
}
