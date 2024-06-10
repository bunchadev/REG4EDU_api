using System.ComponentModel.DataAnnotations;

namespace REG4EDU_api.Model.Domain
{
    public class UserSubject
    {
        [Key]
        public Guid UserSubject_Id { get; set; }
        public Guid FK_SubjectId { get; set; }
        public Guid UserId { get; set; }
        public Subject? Subject { get; set; }
        public User? User { get; set; }
    }
}
