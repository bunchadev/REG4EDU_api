namespace REG4EDU_api.Model.Domain
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid UsersId { get; set; }
        public Guid SemesterId { get; set; }
        public string CreatorName { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
