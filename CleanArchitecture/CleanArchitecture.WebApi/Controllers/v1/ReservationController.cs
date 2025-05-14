using CleanArchitecture.Core.DTOs.Reservation;
using CleanArchitecture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ReservationController : BaseApiController
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("reserve")]
        public async Task<IActionResult> ReserveBook([FromBody] ReserveBookRequest request)
        {
            var result = await _reservationService.ReserveBookAsync(request.UserId, request.BookId);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserReservations(string userId)
        {
            var result = await _reservationService.GetUserReservationsAsync(userId);
            return Ok(result);
        }

        [HttpPost("cancel/{reservationId}")]
        public async Task<IActionResult> CancelReservation(Guid reservationId)
        {
            var result = await _reservationService.CancelReservationAsync(reservationId);
            return Ok(result);
        }

        [HttpPost("process-queue/{bookId}")]
        public async Task<IActionResult> ProcessReservationQueue(Guid bookId)
        {
            var result = await _reservationService.ProcessReservationQueueAsync(bookId);
            return Ok(result);
        }
    }
   
}
