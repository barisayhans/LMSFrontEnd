using CleanArchitecture.Core.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string userId, string message);

        Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(string userId);

        Task MarkAsReadAsync(Guid notificationId);
    }
}
