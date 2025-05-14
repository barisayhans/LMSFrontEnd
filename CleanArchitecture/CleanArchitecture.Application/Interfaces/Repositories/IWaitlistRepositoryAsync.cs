using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IWaitlistRepositoryAsync : IGenericRepositoryAsync<Waitlist>
    {
        Task<IEnumerable<Waitlist>> GetWaitlistByBookIdAsync(Guid bookId);
        Task<Waitlist> GetNextInLineAsync(Guid bookId);
        Task<int> GetPositionInLineAsync(string userId, Guid bookId);
        Task<int> GetWaitlistLengthAsync(Guid bookId);
        Task<IEnumerable<Waitlist>> GetWaitlistByUserIdAsync(string userId);

    }
}
