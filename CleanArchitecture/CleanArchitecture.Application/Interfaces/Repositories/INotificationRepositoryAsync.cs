using CleanArchitecture.Core.DTOs.Notification;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface INotificationRepositoryAsync : IGenericRepositoryAsync<Notification>
    {
        Task<IEnumerable<Notification>> GetNotificationsByUserId(string userId);
    }
}
