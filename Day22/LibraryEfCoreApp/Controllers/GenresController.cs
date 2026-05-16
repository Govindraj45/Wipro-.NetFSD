using LibraryEfCoreApp.Data;
using LibraryEfCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryEfCoreApp.Controllers;

[ApiController]
[Route("api/genres")]
public class GenresController(LibraryDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
    {
        return Ok(await db.Genres.Include(genre => genre.Books).AsNoTracking().ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Genre>> GetGenre(int id)
    {
        var genre = await db.Genres.Include(item => item.Books).AsNoTracking().FirstOrDefaultAsync(item => item.GenreId == id);
        return genre is null ? NotFound(new { message = $"Genre with id {id} was not found." }) : Ok(genre);
    }

    [HttpPost]
    public async Task<ActionResult<Genre>> CreateGenre(Genre genre)
    {
        db.Genres.Add(genre);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetGenre), new { id = genre.GenreId }, genre);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGenre(int id, Genre genre)
    {
        var existing = await db.Genres.FindAsync(id);
        if (existing is null)
        {
            return NotFound(new { message = $"Genre with id {id} was not found." });
        }

        existing.Name = genre.Name;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var existing = await db.Genres.FindAsync(id);
        if (existing is null)
        {
            return NotFound(new { message = $"Genre with id {id} was not found." });
        }

        db.Genres.Remove(existing);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
