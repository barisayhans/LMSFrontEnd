using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IBorrowRepositoryAsync : IGenericRepositoryAsync<Borrow>
    {
        Task<IEnumerable<Borrow>> GetUserBorrowedBooksAsync(string userId);
        Task<IEnumerable<Borrow>> GetOverdueBooksAsync();
        new Task<IEnumerable<Borrow>> GetAllAsync();
        Task<int> GetOverdueBooksCountAsync();

    }
}
