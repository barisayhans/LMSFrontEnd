using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
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
    public class BorrowRepositoryAsync : GenericRepositoryAsync<Borrow>, IBorrowRepositoryAsync
    {
        private readonly DbSet<Borrow> _borrowTransactions;

        public BorrowRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _borrowTransactions = dbContext.Set<Borrow>();
        }

        public async Task<IEnumerable<Borrow>> GetUserBorrowedBooksAsync(string userId)
        {
            return await _borrowTransactions
                .Where(b => b.UserId.Equals(userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Borrow>> GetOverdueBooksAsync()
        {
            return await _borrowTransactions
                .Where(b => b.Status == BorrowStatus.Overdue)
                .ToListAsync();
        }

        Task<IEnumerable<Borrow>> IBorrowRepositoryAsync.GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Borrow>>(_borrowTransactions.AsEnumerable());
        }

        public async Task<int> GetOverdueBooksCountAsync()
        {
            return await _borrowTransactions
                .CountAsync(b => b.Status == BorrowStatus.Overdue);
        }
    }
}
