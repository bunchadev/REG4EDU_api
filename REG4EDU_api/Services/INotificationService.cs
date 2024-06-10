using REG4EDU_api.Model.Dto.Notification;

namespace REG4EDU_api.Services
{
    public interface INotificationService
    {
        Task<string> CreateNotification(NotificationDto notification);
        Task<List<NotificationResDto>> GetNotification(NotificationDto_1 notification);
    }
}
