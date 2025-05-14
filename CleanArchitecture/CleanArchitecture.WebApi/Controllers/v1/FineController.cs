using CleanArchitecture.Core.DTOs.Fine;
using CleanArchitecture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FineController : BaseApiController
    {
        private readonly IFineService _fineService;

        public FineController(IFineService fineService)
        {
            _fineService = fineService;
        }

        // GET: api/v1/fine/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserFines(string userId)
        {
            var fines = await _fineService.GetUserFinesAsync(userId);
            return Ok(fines);
        }

        // POST: api/v1/fine/issue
        [HttpPost("issue")]
        public async Task<IActionResult> IssueFine([FromBody] IssueFineRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fine = await _fineService.IssueFineAsync(request.UserId, request.Amount);
            return CreatedAtAction(nameof(GetUserFines), new { userId = request.UserId }, fine);
        }

        // POST: api/v1/fine/pay
        [HttpPost("pay")]
        public async Task<IActionResult> PayFine([FromBody] PayFineRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _fineService.PayFineAsync(request.UserId, request.FineId);
            return Ok();
        }
    } 

    
}
