namespace REG4EDU_api.Model.Dto.Notification
{
    public class NotificationDto
    {
        public string SemesterName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string CreatorName { get; set; } = null!;
        public string? Content { get; set; }
    }
}
