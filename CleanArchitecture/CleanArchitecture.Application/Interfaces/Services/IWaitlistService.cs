using CleanArchitecture.Core.DTOs.Waitlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface IWaitlistService
    {
        Task AddToWaitlistAsync(string userId, Guid bookId);

        Task<IEnumerable<WaitlistDto>> GetWaitlistByBookIdAsync(Guid bookId);

        Task RemoveFromWaitlistAsync(string userId, Guid bookId);

        Task NotifyNextInLineAsync(Guid bookId);
    }
}
