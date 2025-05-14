using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class FineRepositoryAsync : GenericRepositoryAsync<Fine>, IFineRepositoryAsync
    {
        private readonly DbSet<Fine> _fines;
        public FineRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _fines = dbContext.Set<Fine>();
        }

        public async Task<IReadOnlyList<Fine>> GetFinesByUserIdAsync(string userId)
        {
            return await _fines.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
