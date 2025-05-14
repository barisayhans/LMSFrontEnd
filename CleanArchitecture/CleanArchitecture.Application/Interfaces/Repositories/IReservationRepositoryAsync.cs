using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IReservationRepositoryAsync : IGenericRepositoryAsync<Reservation>
    {
        Task<IEnumerable<Reservation>> GetUserReservationsAsync(string userId);
        Task<IEnumerable<Reservation>> GetPendingReservationsAsync(Guid bookId);
        Task<int> GetActiveReservationsCountAsync();

    }
}
