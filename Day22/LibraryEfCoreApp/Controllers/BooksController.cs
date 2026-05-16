using LibraryEfCoreApp.Data;
using LibraryEfCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryEfCoreApp.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController(LibraryDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return Ok(await db.Books
            .Include(book => book.Author)
            .Include(book => book.Genres)
            .AsNoTracking()
            .OrderBy(book => book.Title)
            .ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await db.Books
            .Include(item => item.Author)
            .Include(item => item.Genres)
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.BookId == id);

        return book is null ? NotFound(new { message = $"Book with id {id} was not found." }) : Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(Book book)
    {
        if (!await db.Authors.AnyAsync(author => author.AuthorId == book.AuthorId))
        {
            return BadRequest(new { message = $"Author with id {book.AuthorId} does not exist." });
        }

        db.Books.Add(book);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, Book book)
    {
        var existing = await db.Books.FindAsync(id);
        if (existing is null)
        {
            return NotFound(new { message = $"Book with id {id} was not found." });
        }

        existing.Title = book.Title;
        existing.AuthorId = book.AuthorId;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var existing = await db.Books.FindAsync(id);
        if (existing is null)
        {
            return NotFound(new { message = $"Book with id {id} was not found." });
        }

        db.Books.Remove(existing);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
