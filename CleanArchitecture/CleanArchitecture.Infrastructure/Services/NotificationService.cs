using AutoMapper;
using CleanArchitecture.Core.DTOs.Notification;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepositoryAsync _notificationRepository;
        private readonly IMapper _mapper;
        public NotificationService(INotificationRepositoryAsync notificationRepository, IMapper mapper) 
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(string userId)
        {            
            var userNotifications = await _notificationRepository.GetNotificationsByUserId(userId);
            if (userNotifications == null)
            {
                throw new KeyNotFoundException("No notifications found for the user.");
            }
            return _mapper.Map<IEnumerable<NotificationDto>>(userNotifications);
        }

        public async Task MarkAsReadAsync(Guid notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);
            if (notification == null)
            {
                throw new KeyNotFoundException("Notification not found.");
            }

            notification.IsRead = true;
            await _notificationRepository.UpdateAsync(notification);
        }

        public async Task SendNotificationAsync(string userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                IsRead = false
            };

            await _notificationRepository.AddAsync(notification);
        }
    }
}
