using LibraryEfCoreApp.Data;
using LibraryEfCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryEfCoreApp.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController(LibraryDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
    {
        return Ok(await db.Authors.Include(author => author.Books).AsNoTracking().ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Author>> GetAuthor(int id)
    {
        var author = await db.Authors.Include(item => item.Books).AsNoTracking().FirstOrDefaultAsync(item => item.AuthorId == id);
        return author is null ? NotFound(new { message = $"Author with id {id} was not found." }) : Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<Author>> CreateAuthor(Author author)
    {
        db.Authors.Add(author);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorId }, author);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAuthor(int id, Author author)
    {
        var existing = await db.Authors.FindAsync(id);
        if (existing is null)
        {
            return NotFound(new { message = $"Author with id {id} was not found." });
        }

        existing.Name = author.Name;
        existing.Bio = author.Bio;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var existing = await db.Authors.FindAsync(id);
        if (existing is null)
        {
            return NotFound(new { message = $"Author with id {id} was not found." });
        }

        db.Authors.Remove(existing);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
