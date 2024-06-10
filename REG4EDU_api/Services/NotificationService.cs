using REG4EDU_api.Model.Dto.Notification;
using REG4EDU_api.Repositories;

namespace REG4EDU_api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }
        public async Task<string> CreateNotification(NotificationDto notification)
        {
            return await notificationRepository.CreateNotification(notification);
        }

        public async Task<List<NotificationResDto>> GetNotification(NotificationDto_1 notification)
        {
            return await notificationRepository.GetNotification(notification);
        }
    }
}
