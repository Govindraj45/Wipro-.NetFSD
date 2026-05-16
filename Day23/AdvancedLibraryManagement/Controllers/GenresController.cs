using AdvancedLibraryManagement.Models;
using AdvancedLibraryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedLibraryManagement.Controllers;

public record GenreRequest(string Name);

public class GenresController(GenreRepository genres) : Controller
{
    [HttpGet("/api/genres")]
    public async Task<IActionResult> GetGenres()
    {
        var data = await genres.GetAllAsync();
        return Json(data.OrderBy(genre => genre.Name).Select(genre => new { id = genre.GenreId, genre.Name }));
    }

    [HttpPost("/api/genres")]
    public async Task<IActionResult> CreateGenre([FromBody] GenreRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "Genre name is required." });
        }

        var genre = new Genre { Name = request.Name.Trim() };
        await genres.AddAsync(genre);
        await genres.SaveChangesAsync();
        return Json(new { id = genre.GenreId, genre.Name });
    }

    [HttpPut("/api/genres/{id:int}")]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreRequest request)
    {
        var genre = await genres.GetByIdAsync(id);
        if (genre is null)
        {
            return NotFound(new { message = $"Genre with id {id} was not found." });
        }

        genre.Name = request.Name.Trim();
        genres.Update(genre);
        await genres.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("/api/genres/{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await genres.GetByIdAsync(id);
        if (genre is null)
        {
            return NotFound(new { message = $"Genre with id {id} was not found." });
        }

        genres.Delete(genre);
        await genres.SaveChangesAsync();
        return NoContent();
    }
}
