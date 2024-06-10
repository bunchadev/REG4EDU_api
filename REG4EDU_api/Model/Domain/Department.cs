using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }
        public string Name { get; set; } = null!;
        public string DepartmentCode { get; set; } = null!;
    }
}
