using AutoMapper;
using CleanArchitecture.Core.DTOs.Waitlist;
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
    public class WaitlistService : IWaitlistService
    {
        private readonly IWaitlistRepositoryAsync _waitlistRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public WaitlistService(IWaitlistRepositoryAsync waitlistRepository, IMapper mapper, INotificationService notificationService)
        {
            _waitlistRepository = waitlistRepository;
            _mapper = mapper;
            _notificationService = notificationService;
        }
        public async Task AddToWaitlistAsync(string userId, Guid bookId)
        {
            var waitlist = new Waitlist
            {
                UserId = userId,
                BookId = bookId,
                Position = await _waitlistRepository.GetPositionInLineAsync(userId, bookId),
                Status = WaitStatus.Waiting
            };

            await _waitlistRepository.AddAsync(waitlist);
        }

        public async Task<IEnumerable<WaitlistDto>> GetWaitlistByBookIdAsync(Guid bookId)
        {
            var waitlist = await _waitlistRepository.GetWaitlistByBookIdAsync(bookId);
            if(waitlist == null)
                throw new KeyNotFoundException("Waitlist is empty.");

            return _mapper.Map<IEnumerable<WaitlistDto>>(waitlist);
        }

        public async Task NotifyNextInLineAsync(Guid bookId)
        {
            var nextInLine = await _waitlistRepository.GetNextInLineAsync(bookId);
            if (nextInLine == null)
                throw new KeyNotFoundException("No one is next in line.");

            nextInLine.Status = WaitStatus.Notified;
            await _waitlistRepository.UpdateAsync(nextInLine);

            //notify the user            
             await _notificationService.SendNotificationAsync(nextInLine.UserId, "You are next in line for the book.");
        }

        public async Task RemoveFromWaitlistAsync(string userId, Guid bookId)
        {
            var waitlist = await _waitlistRepository.GetWaitlistByBookIdAsync(bookId);
            var entryToRemove = waitlist?.FirstOrDefault(w => w.UserId.Equals(userId));

            if (entryToRemove == null)
                throw new KeyNotFoundException("User not found in the waitlist.");

            await _waitlistRepository.DeleteAsync(entryToRemove);
        }
    }
}
