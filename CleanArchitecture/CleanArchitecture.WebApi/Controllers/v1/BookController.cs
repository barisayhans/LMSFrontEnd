using CleanArchitecture.Core.DTOs.Book;
using CleanArchitecture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BookController : BaseApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/v1/book
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // GET api/v1/book/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/v1/book
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookDto createBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdBook = await _bookService.AddBookAsync(createBookDto);
            return CreatedAtAction(nameof(Get), createdBook);
        }

        // PUT api/v1/book/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _bookService.UpdateBookAsync(id, bookDto);

            return Ok();
        }

        // DELETE api/v1/book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

        [HttpGet("genres")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _bookService.GetAllGenresAsync();
            return Ok(genres);
        }

    }

}
