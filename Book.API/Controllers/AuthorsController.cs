using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Book.API.Data;
using Book.API.Dtos.Author;
using AutoMapper;
using Book.API.Static;
using Book.API.Dtos.Book;
using AutoMapper.QueryableExtensions;

namespace Book.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        public class BooksController : ControllerBase
        {
            private readonly BookContext _context;
            private readonly IMapper mapper;

            public BooksController(BookContext context, IMapper mapper)
            {
                _context = context;
                this.mapper = mapper;
            }

            // GET: api/Books
            [HttpGet]
            public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
            {
                var bookDtos = await _context.Books
                    .Include(q => q.Author)
                    .ProjectTo<BookReadOnlyDto>(mapper.ConfigurationProvider)
                    .ToListAsync();
                return Ok(bookDtos);
            }

            // GET: api/Books/5
            [HttpGet("{id}")]
            public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
            {
                var book = await _context.Books
                    .Include(q => q.Author)
                    .ProjectTo<BookDetailsDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(q => q.Id == id);

                if (book == null)
                {
                    return NotFound();
                }

                return book;
            }

            // PUT: api/Books/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
            {

                if (id != bookDto.Id)
                {
                    return BadRequest();
                }

                var book = await _context.Books.FindAsync(id);

                if (book == null)
                {
                    return NotFound();
                }

                mapper.Map(bookDto, book);
                _context.Entry(book).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BookExistsAsync(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            // POST: api/Books
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto bookDto)
            {
                var book = mapper.Map<Data.Book>(bookDto);
                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }

            // DELETE: api/Books/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteBook(int id)
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private async Task<bool> BookExistsAsync(int id)
            {
                return await _context.Books.AnyAsync(e => e.Id == id);
            }
        }
    }
}
