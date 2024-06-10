using Microsoft.EntityFrameworkCore;
using REG4EDU_api.Data;
using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto.Notification;

namespace REG4EDU_api.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DevDbContext dbContext;

        public NotificationRepository(DevDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<string> CreateNotification(NotificationDto notification)
        {
            var semester = await dbContext.Semesters
                .FirstOrDefaultAsync(x => x.Name == notification.SemesterName);
            var user = await dbContext.Users
                .FirstOrDefaultAsync(x => x.UserName == notification.UserName);
            if (user is not null && semester is not null)
            {
                var myNotification = new Notification
                {
                    SemesterId = semester.SemesterId,
                    UsersId = user.UserId,
                    CreatorName = notification.CreatorName,
                    Content = notification.Content
                };
                await dbContext.AddAsync(myNotification);
                await dbContext.SaveChangesAsync();
                return "200";
            }
            return "400";
        }

        public async Task<List<NotificationResDto>> GetNotification(NotificationDto_1 notification)
        {
            var semester = await dbContext.Semesters
                .FirstOrDefaultAsync(x => x.Name == notification.SemesterName);
            var notifications = await dbContext.Notification
                .Where(x => x.UsersId == notification.UserId && x.SemesterId == semester!.SemesterId)
                .OrderByDescending(x => x.CreateAt)
                .Select(x =>
                     new NotificationResDto
                     {
                         Id = x.Id,
                         CreatorName = x.CreatorName,
                         Content = x.Content,
                         CreateAt = x.CreateAt.ToString("yyyy-MM-dd HH:mm:ss")
                     }
                ).ToListAsync();
            return notifications;
        }
    }
}
