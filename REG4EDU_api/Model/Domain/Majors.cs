using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class Majors
    {
        [Key]
        public Guid MajorsId { get; set; }
        public string MajorsName { get; set; } = null!;
        public string MajorsCode { get; set; } = null!;
    }
}
