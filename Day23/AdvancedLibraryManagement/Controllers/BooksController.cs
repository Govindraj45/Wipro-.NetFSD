using AdvancedLibraryManagement.Models;
using AdvancedLibraryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedLibraryManagement.Controllers;

public record BookRequest(string Title, int AuthorId, int[] GenreIds);

public class BooksController(
    IBookRepository books,
    AuthorRepository authors,
    GenreRepository genres,
    ILogger<BooksController> logger) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.Authors = await authors.GetAllAsync();
        ViewBag.Genres = await genres.GetAllAsync();
        return View();
    }

    [HttpGet("/api/books")]
    public async Task<IActionResult> GetBooks(string? search, int page = 1, int pageSize = 10)
    {
        var data = await books.GetPagedWithDetailsAsync(search, page, pageSize);
        return Json(data.Select(ToDto));
    }

    [HttpPost("/api/books")]
    public async Task<IActionResult> CreateBook([FromBody] BookRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new { message = "Title is required." });
            }

            var created = await books.AddBookAsync(request.Title, request.AuthorId, request.GenreIds);
            var withDetails = await books.GetWithDetailsAsync(created.BookId);
            return Json(ToDto(withDetails ?? created));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "AJAX create failed.");
            return BadRequest(new { message = "Unable to create the book." });
        }
    }

    [HttpPut("/api/books/{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] BookRequest request)
    {
        try
        {
            var updated = await books.UpdateBookAsync(id, request.Title, request.AuthorId, request.GenreIds);
            return updated ? NoContent() : NotFound(new { message = $"Book with id {id} was not found." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "AJAX update failed.");
            return BadRequest(new { message = "Unable to update the book." });
        }
    }

    [HttpDelete("/api/books/{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var existing = await books.GetByIdAsync(id);
        if (existing is null)
        {
            return NotFound(new { message = $"Book with id {id} was not found." });
        }

        books.Delete(existing);
        await books.SaveChangesAsync();
        return NoContent();
    }

    private static object ToDto(Book book) => new
    {
        id = book.BookId,
        book.Title,
        author = book.Author?.Name ?? "Unknown",
        genres = book.Genres.Select(genre => genre.Name).OrderBy(name => name).ToArray()
    };
}
