using AutoMapper;
using CleanArchitecture.Core.DTOs.BorrowTransaction;
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
    public class BorrowService : IBorrowService
    {
        private readonly IMapper _mapper;
        private readonly IBorrowRepositoryAsync _borrowRepository;

        public BorrowService(IMapper mapper, IBorrowRepositoryAsync borrowRepository)
        {
            _mapper = mapper;
            _borrowRepository = borrowRepository;
        }

        public async Task<BorrowTransactionDto> BorrowBookAsync(string userId, Guid bookId, int dayCount)
        {            
            var borrow = new Borrow
            {
                UserId = userId,
                BookId = bookId,
                BorrowedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(dayCount), 
                Status = BorrowStatus.Borrowed
            };
            
            await _borrowRepository.AddAsync(borrow);
            
            var borrowTransactionDto = _mapper.Map<BorrowTransactionDto>(borrow);

            return borrowTransactionDto;
        }

        public async Task CalculateLateFeesAsync(Guid transactionId, decimal dailyFee)
        {
            var borrow = await _borrowRepository.GetByIdAsync(transactionId);
            if (borrow == null)
            {
                throw new KeyNotFoundException("Borrow transaction not found.");
            }

            if (borrow.Status == BorrowStatus.Borrowed && DateTime.UtcNow > borrow.DueDate)
            {
                var daysLate = (DateTime.UtcNow - borrow.DueDate).Days;
                borrow.LateFee = daysLate * dailyFee;
                borrow.Status = BorrowStatus.Overdue;

                await _borrowRepository.UpdateAsync(borrow);
            }
        }

        public async Task<IEnumerable<BorrowTransactionDto>> GetOverdueBooksAsync()
        {
            var overdueBorrows = await _borrowRepository.GetOverdueBooksAsync();
            if (overdueBorrows == null)
            {
                throw new KeyNotFoundException("No overdue books found.");
            }

            var overdueBorrowDtos = _mapper.Map<IEnumerable<BorrowTransactionDto>>(overdueBorrows);

            return overdueBorrowDtos;
        }

        public async Task<IEnumerable<BorrowTransactionDto>> GetUserBorrowedBooksAsync(string userId)
        {
            var userBorrows = await _borrowRepository.GetUserBorrowedBooksAsync(userId);
            if (userBorrows == null || !userBorrows.Any())
            {
                throw new KeyNotFoundException("No borrowed books found for the user.");
            }

            var userBorrowDtos = _mapper.Map<IEnumerable<BorrowTransactionDto>>(userBorrows);

            return userBorrowDtos;
        }

        public async Task ReturnBookAsync(Guid transactionId)
        {
            var borrow = await _borrowRepository.GetByIdAsync(transactionId);
            if (borrow == null)
            {
                throw new KeyNotFoundException("Borrow transaction not found.");
            }

            if (borrow.Status != BorrowStatus.Borrowed)
            {
                throw new InvalidOperationException("The book is not currently borrowed.");
            }

            borrow.ReturnedAt = DateTime.UtcNow;
            borrow.Status = BorrowStatus.Returned;

            await _borrowRepository.UpdateAsync(borrow);
        }
    }
}
