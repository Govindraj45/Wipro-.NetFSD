using ECommerceApp.Models;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController(BookStoreRepository repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return Ok(repository.GetBooks());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Book> GetBook(int id)
    {
        var book = repository.GetBook(id);
        return book is null ? NotFound(new { message = $"Book with id {id} was not found." }) : Ok(book);
    }

    [HttpPost]
    public ActionResult<Book> CreateBook(Book book)
    {
        if (!repository.AuthorExists(book.AuthorId))
        {
            return BadRequest(new { message = $"Author with id {book.AuthorId} does not exist." });
        }

        var created = repository.AddBook(book);
        return CreatedAtAction(nameof(GetBook), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateBook(int id, Book book)
    {
        if (!repository.AuthorExists(book.AuthorId))
        {
            return BadRequest(new { message = $"Author with id {book.AuthorId} does not exist." });
        }

        return repository.UpdateBook(id, book) ? NoContent() : NotFound(new { message = $"Book with id {id} was not found." });
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook(int id)
    {
        return repository.DeleteBook(id) ? NoContent() : NotFound(new { message = $"Book with id {id} was not found." });
    }
}
