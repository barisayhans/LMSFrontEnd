using CleanArchitecture.Core.DTOs.Fine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface IFineService
    {
        Task<FineDto> GetUserFinesAsync(string userId);

        Task PayFineAsync(string userId, Guid fineId);

        Task<FineDto> IssueFineAsync(string userId, decimal amount);
    }
}
