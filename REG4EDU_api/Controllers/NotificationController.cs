using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REG4EDU_api.Model.Dto.Notification;
using REG4EDU_api.Services;

namespace REG4EDU_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationDto notification)
        {
            var result = await notificationService.CreateNotification(notification);
            return Ok(new { statusCode = result });
        }
        [HttpPost]
        public async Task<IActionResult> GetNotification([FromBody] NotificationDto_1 notification)
        {
            var result = await notificationService.GetNotification(notification);
            return Ok(new { statusCode = "200", data = result });
        }
    }
}
