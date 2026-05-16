using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController(BookStoreRepository repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Author>> GetAuthors()
    {
        return Ok(repository.GetAuthors());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Author> GetAuthor(int id)
    {
        var author = repository.GetAuthor(id);
        return author is null ? NotFound(new { message = $"Author with id {id} was not found." }) : Ok(author);
    }

    [HttpGet("{authorId:int}/books")]
    public ActionResult<IEnumerable<Book>> GetBooksByAuthor(int authorId)
    {
        var books = repository.GetBooksByAuthor(authorId);
        return books is null ? NotFound(new { message = $"Author with id {authorId} was not found." }) : Ok(books);
    }

    [HttpPost]
    public ActionResult<Author> CreateAuthor(Author author)
    {
        var created = repository.AddAuthor(author);
        return CreatedAtAction(nameof(GetAuthor), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAuthor(int id, Author author)
    {
        return repository.UpdateAuthor(id, author)
            ? NoContent()
            : NotFound(new { message = $"Author with id {id} was not found." });
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAuthor(int id)
    {
        return repository.DeleteAuthor(id)
            ? NoContent()
            : NotFound(new { message = $"Author with id {id} was not found." });
    }
}
