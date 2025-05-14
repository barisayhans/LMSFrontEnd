using CleanArchitecture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class WaitlistController : BaseApiController
    {
        private readonly IWaitlistService _waitlistService;

        public WaitlistController(IWaitlistService waitlistService)
        {
            _waitlistService = waitlistService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToWaitlistAsync(String userId, Guid bookId)
        {
            await _waitlistService.AddToWaitlistAsync(userId, bookId);
            return Ok();
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetWaitlistByBookIdAsync(Guid bookId)
        {
            var waitlist = await _waitlistService.GetWaitlistByBookIdAsync(bookId);
            return Ok(waitlist);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromWaitlistAsync(string userId, Guid bookId)
        {
            await _waitlistService.RemoveFromWaitlistAsync(userId, bookId);
            return NoContent();
        }

        [HttpPost("notify-next/{bookId}")]
        public async Task<IActionResult> NotifyNextInLineAsync(Guid bookId)
        {
            await _waitlistService.NotifyNextInLineAsync(bookId);
            return Ok();
        }
    }
}
