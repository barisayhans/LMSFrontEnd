using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IFineRepositoryAsync : IGenericRepositoryAsync<Fine>
    {
        Task<IReadOnlyList<Fine>> GetFinesByUserIdAsync(string userId);
    }
}
