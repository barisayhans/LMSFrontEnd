using CleanArchitecture.Core.DTOs.Notification;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class NotificationRepositoryAsync : GenericRepositoryAsync<Notification>, INotificationRepositoryAsync
    {
        private readonly DbSet<Notification> _notifications;

        public NotificationRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _notifications = dbContext.Set<Notification>();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserId(string userId)
        {
            return await _notifications
                .Where(n => n.UserId == userId)
                .ToListAsync();
        }
    }
}
