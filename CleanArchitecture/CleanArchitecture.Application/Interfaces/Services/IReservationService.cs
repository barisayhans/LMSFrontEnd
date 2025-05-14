using CleanArchitecture.Core.DTOs.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface IReservationService
    {
        Task<ReservationDto> ReserveBookAsync(string userId, Guid bookId);

        Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(string userId);

        Task<bool> CancelReservationAsync(Guid reservationId);

        Task<bool> ProcessReservationQueueAsync(Guid bookId);
    }
}
