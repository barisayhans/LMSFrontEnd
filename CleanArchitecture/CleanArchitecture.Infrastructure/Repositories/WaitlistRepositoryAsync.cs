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
    public class WaitlistRepositoryAsync : GenericRepositoryAsync<Waitlist>, IWaitlistRepositoryAsync
    {
        private readonly DbSet<Waitlist> _waitlists;
        public WaitlistRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _waitlists = dbContext.Set<Waitlist>();
        }

        public async Task<IEnumerable<Waitlist>> GetWaitlistByBookIdAsync(Guid bookId)
        {
            return await _waitlists.Where(x => x.BookId == bookId).ToListAsync();
        }

        public async Task<Waitlist> GetNextInLineAsync(Guid bookId)
        {
            return await _waitlists.OrderBy(x => x.CreatedAt).FirstOrDefaultAsync(x => x.BookId == bookId);
        }

        public async Task<int> GetPositionInLineAsync(String userId, Guid bookId)
        {
            var waitlist = await _waitlists.Where(x => x.BookId == bookId).OrderBy(x => x.CreatedAt).ToListAsync();
            return waitlist.FindIndex(x => x.UserId.Equals(userId)) + 1;
        }

        public async Task<int> GetWaitlistLengthAsync(Guid bookId)
        {
            return await _waitlists.Where(x => x.BookId == bookId).CountAsync();
        }

        public async Task<IEnumerable<Waitlist>> GetWaitlistByUserIdAsync(string userId)
        {
            return await _waitlists.Where(x => x.UserId.Equals(userId)).ToListAsync();
        }
    }
}
