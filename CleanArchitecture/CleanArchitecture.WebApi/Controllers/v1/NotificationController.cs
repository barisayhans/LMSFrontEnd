using CleanArchitecture.Core.DTOs.Notification;
using CleanArchitecture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetUserNotifications(string userId)
        {
            var notifications = await _notificationService.GetUserNotificationsAsync(userId);
            return Ok(notifications);
        }

        [HttpPost("mark-as-read/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(Guid notificationId)
        {
            await _notificationService.MarkAsReadAsync(notificationId);
            return NoContent();
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequest request)
        {
            await _notificationService.SendNotificationAsync(request.UserId, request.Message);
            return Ok();
        }
    }

    
}
