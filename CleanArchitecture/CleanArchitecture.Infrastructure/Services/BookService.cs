using CleanArchitecture.Core.DTOs.Book;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CleanArchitecture.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepositoryAsync _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepositoryAsync bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<BookDto> AddBookAsync(CreateBookDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var book = _mapper.Map<Book>(dto);
            await _bookRepository.AddAsync(book);
            return _mapper.Map<BookDto>(book);
        }

        public async Task UpdateBookAsync(Guid bookId, BookDto dto)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            _mapper.Map(dto, book);
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(Guid bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            await _bookRepository.DeleteAsync(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(Guid bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> IsBookAvailableAsync(Guid bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            return book != null && book.Copies > 0;
        }

        public async Task<IEnumerable<BookDto>> SearchBooksAsync(BookSearchDto dto)
        {
            var books = await _bookRepository.SearchAsync(dto);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<string>> GetAllGenresAsync()
        {
            var genres = await _bookRepository.GetAllAsync();
            return genres.Select(b => b.Genre).Distinct().OrderBy(g => g);
        }

    }
}
