using BookstoreAdoNetApp.Data;
using BookstoreAdoNetApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAdoNetApp.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController(BookRepository repository, ILogger<BooksController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return Ok(await repository.GetAllWithReaderAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await repository.GetByIdAsync(id);
        return book is null ? NotFound(new { message = $"Book with id {id} was not found." }) : Ok(book);
    }

    [HttpGet("dataset")]
    public async Task<IActionResult> GetDisconnectedData()
    {
        var dataSet = await repository.GetBooksDataSetAsync();
        var table = dataSet.Tables["Books"];

        return Ok(new
        {
            table = table?.TableName,
            rows = table?.Rows.Count ?? 0,
            columns = table?.Columns.Cast<System.Data.DataColumn>().Select(column => column.ColumnName)
        });
    }

    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(Book book)
    {
        try
        {
            var newId = await repository.AddWithStoredProcedureAsync(book);
            var created = await repository.GetByIdAsync(newId) ?? new Book
            {
                BookId = newId,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                PublishedYear = book.PublishedYear
            };
            return CreatedAtAction(nameof(GetBook), new { id = newId }, created);
        }
        catch (Exception ex) when (ex is ArgumentException or Microsoft.Data.SqlClient.SqlException)
        {
            logger.LogWarning(ex, "Unable to create book.");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, Book book)
    {
        try
        {
            return await repository.UpdateWithStoredProcedureAsync(id, book)
                ? NoContent()
                : NotFound(new { message = $"Book with id {id} was not found." });
        }
        catch (Exception ex) when (ex is ArgumentException or Microsoft.Data.SqlClient.SqlException)
        {
            logger.LogWarning(ex, "Unable to update book.");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        return await repository.DeleteWithStoredProcedureAsync(id)
            ? NoContent()
            : NotFound(new { message = $"Book with id {id} was not found." });
    }
}
