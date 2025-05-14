using CleanArchitecture.Core.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface IBookService
    {
        Task<BookDto> AddBookAsync(CreateBookDto dto);
        Task UpdateBookAsync(Guid bookId, BookDto dto);
        Task DeleteBookAsync(Guid bookId);
        Task<BookDto> GetBookByIdAsync(Guid bookId);
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<IEnumerable<BookDto>> SearchBooksAsync(BookSearchDto dto);
        Task<bool> IsBookAvailableAsync(Guid bookId);
        Task<IEnumerable<string>> GetAllGenresAsync();
    }
}
