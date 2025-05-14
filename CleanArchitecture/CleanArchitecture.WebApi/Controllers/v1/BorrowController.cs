using CleanArchitecture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]

    public class BorrowController : BaseApiController
    {
        private readonly IBorrowService _borrowService;

        public BorrowController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBookAsync(String userId, Guid bookId, int dayCount)
        {
            var result = await _borrowService.BorrowBookAsync(userId, bookId, dayCount);
            return Ok(result);
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBookAsync(Guid transactionId)
        {
            await _borrowService.ReturnBookAsync(transactionId);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBorrowedBooksAsync(string userId)
        {
            var result = await _borrowService.GetUserBorrowedBooksAsync(userId);
            return Ok(result);
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueBooksAsync()
        {
            var result = await _borrowService.GetOverdueBooksAsync();
            return Ok(result);
        }

        [HttpPost("calculate-late-fees")]
        public async Task<IActionResult> CalculateLateFeesAsync(Guid transactionId, decimal dailyFee)
        {
            await _borrowService.CalculateLateFeesAsync(transactionId, dailyFee);
            return NoContent();
        }
    }
}
