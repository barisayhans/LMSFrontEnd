using CleanArchitecture.Core.DTOs.Book;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IBookRepositoryAsync : IGenericRepositoryAsync<Book>
    {
        public Task<IEnumerable<Book>> SearchAsync(BookSearchDto dto);
        new Task<IEnumerable<Book>> GetAllAsync();
    }
}
