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
    public class ReservationRepositoryAsync : GenericRepositoryAsync<Reservation>, IReservationRepositoryAsync
    {
        private readonly DbSet<Reservation> _reservations;

        public ReservationRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _reservations = dbContext.Set<Reservation>();
        }

        public async Task<IEnumerable<Reservation>> GetUserReservationsAsync(string userId)
        {
            return await _reservations
                .Where(r => r.UserId.Equals(userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetPendingReservationsAsync(Guid bookId)
        {
            return await _reservations
                .Where(r => r.BookId == bookId && r.Status == ReservationStatus.Pending)
                .OrderBy(r => r.ReservedAt)
                .ToListAsync();
        }

        public async Task<int> GetActiveReservationsCountAsync()
        {
            return await _reservations
                .CountAsync(r => r.Status == ReservationStatus.Notified);
        }
    }
}
