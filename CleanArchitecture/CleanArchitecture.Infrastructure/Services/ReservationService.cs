using AutoMapper;
using CleanArchitecture.Core.DTOs.Reservation;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepositoryAsync _reservationRepository;
        private readonly IMapper _mapper;
        public ReservationService(IReservationRepositoryAsync reservationRepository, IMapper mapper) 
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }
        public async Task<bool> CancelReservationAsync(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }

            reservation.Status = ReservationStatus.Cancelled;
            await _reservationRepository.UpdateAsync(reservation);
            return true;
        }

        public async Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(string userId)
        {
            var reservations = await _reservationRepository.GetUserReservationsAsync(userId);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations ?? Enumerable.Empty<Reservation>());
        }

        public async Task<bool> ProcessReservationQueueAsync(Guid bookId)
        {
            var pendingReservations = await _reservationRepository.GetPendingReservationsAsync(bookId);
            if (pendingReservations == null || !pendingReservations.Any())
            {
                return false;
            }

            var nextReservation = pendingReservations.FirstOrDefault();
            if (nextReservation == null)
            {
                return false;
            }

            nextReservation.Status = ReservationStatus.Notified;
            await _reservationRepository.UpdateAsync(nextReservation);
            return true;
        }

        public async Task<ReservationDto> ReserveBookAsync(string userId, Guid bookId)
        {
            var reservation = new Reservation
            {
                BookId = bookId,
                UserId = userId,
                ReservedAt = DateTime.UtcNow,
                Status = ReservationStatus.Pending
            };

            var addedReservation = await _reservationRepository.AddAsync(reservation);
            return _mapper.Map<ReservationDto>(addedReservation);
        }
    }
}
