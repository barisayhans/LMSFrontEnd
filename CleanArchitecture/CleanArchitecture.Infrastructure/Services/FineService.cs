using AutoMapper;
using CleanArchitecture.Core.DTOs.Fine;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class FineService : IFineService
    {
        private readonly IFineRepositoryAsync _fineRepository;
        private readonly IMapper _mapper;
        public FineService(IFineRepositoryAsync fineRepository, IMapper mapper) 
        {
            _fineRepository = fineRepository;
            _mapper = mapper;
        }
        public async Task<FineDto> GetUserFinesAsync(string userId)
        {
            var fines = await _fineRepository.GetFinesByUserIdAsync(userId);
            if(fines == null)
            {
                throw new KeyNotFoundException("No fine found for the user.");
            }
            var fineDto = _mapper.Map<FineDto>(fines.FirstOrDefault());
            return fineDto;
        }

        public async Task<FineDto> IssueFineAsync(string userId, decimal amount)
        {
            var fine = new Fine
            {
                UserId = userId,
                Amount = amount,
                IssuedAt = DateTime.UtcNow,
                IsPaid = false
            };

            var createdFine = await _fineRepository.AddAsync(fine);
            var fineDto = _mapper.Map<FineDto>(createdFine);
            return fineDto;
        }

        public async Task PayFineAsync(string userId, Guid fineId)
        {
            var fine = await _fineRepository.GetByIdAsync(fineId);
            if (fine == null || !fine.UserId.Equals(userId))
            {
                throw new KeyNotFoundException("Fine not found for the user.");
            }

            fine.IsPaid = true;
            await _fineRepository.UpdateAsync(fine);
        }
    }
}
