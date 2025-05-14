using CleanArchitecture.Core.DTOs.Book;
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
    public class BookRepositoryAsync : GenericRepositoryAsync<Book>, IBookRepositoryAsync
    {
        private readonly DbSet<Book> _books;

        public BookRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _books = dbContext.Set<Book>();
        }

        public async Task<IEnumerable<Book>> SearchAsync(BookSearchDto dto)
        {
            IQueryable<Book> query = _books;

            if (!string.IsNullOrEmpty(dto.ISBN))
                query = query.Where(b => b.ISBN.Contains(dto.ISBN));

            if (!string.IsNullOrEmpty(dto.Title))
                query = query.Where(b => b.Title.Contains(dto.Title));

            if (!string.IsNullOrEmpty(dto.Author))
                query = query.Where(b => b.Author.Contains(dto.Author));

            if (!string.IsNullOrEmpty(dto.Description))
                query = query.Where(b => b.Description.Contains(dto.Description));

            if (!string.IsNullOrEmpty(dto.Genre))
                query = query.Where(b => b.Genre.Contains(dto.Genre));

            if (dto.PublishedAfter != default)
                query = query.Where(b => b.PublishDate >= dto.PublishedAfter);

            if (dto.PublishedBefore != default)
                query = query.Where(b => b.PublishDate <= dto.PublishedBefore);

            if (dto.MinCopies > 0)
                query = query.Where(b => b.Copies >= dto.MinCopies);

            if (dto.MaxCopies > 0)
                query = query.Where(b => b.Copies <= dto.MaxCopies);

            if (dto.Status != default)
                query = query.Where(b => b.Status == dto.Status);

            return await query.ToListAsync();
        }

        Task<IEnumerable<Book>> IBookRepositoryAsync.GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Book>>(_books.AsEnumerable());
        }
    }
}
