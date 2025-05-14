using CleanArchitecture.Core.DTOs.BorrowTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface IBorrowService
    {
        Task<BorrowTransactionDto> BorrowBookAsync(string userId, Guid bookId, int dayCount);

        Task ReturnBookAsync(Guid transactionId);

        Task<IEnumerable<BorrowTransactionDto>> GetUserBorrowedBooksAsync(string userId);

        Task<IEnumerable<BorrowTransactionDto>> GetOverdueBooksAsync();

        Task CalculateLateFeesAsync(Guid transactionId, decimal dailyFee);
    }
}
