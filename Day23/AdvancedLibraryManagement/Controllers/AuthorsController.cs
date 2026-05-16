using AdvancedLibraryManagement.Models;
using AdvancedLibraryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedLibraryManagement.Controllers;

public record AuthorRequest(string Name);

public class AuthorsController(AuthorRepository authors) : Controller
{
    [HttpGet("/api/authors")]
    public async Task<IActionResult> GetAuthors()
    {
        var data = await authors.GetAllAsync();
        return Json(data.OrderBy(author => author.Name).Select(author => new { id = author.AuthorId, author.Name }));
    }

    [HttpPost("/api/authors")]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "Author name is required." });
        }

        var author = new Author { Name = request.Name.Trim() };
        await authors.AddAsync(author);
        await authors.SaveChangesAsync();
        return Json(new { id = author.AuthorId, author.Name });
    }

    [HttpPut("/api/authors/{id:int}")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorRequest request)
    {
        var author = await authors.GetByIdAsync(id);
        if (author is null)
        {
            return NotFound(new { message = $"Author with id {id} was not found." });
        }

        author.Name = request.Name.Trim();
        authors.Update(author);
        await authors.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("/api/authors/{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await authors.GetByIdAsync(id);
        if (author is null)
        {
            return NotFound(new { message = $"Author with id {id} was not found." });
        }

        authors.Delete(author);
        await authors.SaveChangesAsync();
        return NoContent();
    }
}
