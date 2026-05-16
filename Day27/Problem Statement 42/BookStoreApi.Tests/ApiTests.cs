using BookStoreApi.Controllers;
using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BookStoreApi.Tests;

public class ApiTests
{
    [Fact]
    public void GetBooks_ReturnsSeedData()
    {
        var controller = new BooksController(new BookStoreRepository());

        var result = controller.GetBooks().Result;

        var ok = Assert.IsType<OkObjectResult>(result);
        var books = Assert.IsAssignableFrom<IEnumerable<Book>>(ok.Value);
        Assert.Contains(books, book => book.Title == "1984");
    }

    [Fact]
    public void CreateBook_ReturnsBadRequest_ForUnknownAuthor()
    {
        var controller = new BooksController(new BookStoreRepository());

        var result = controller.CreateBook(new Book { Title = "Test", AuthorId = 999, PublicationYear = 2024 }).Result;

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void GetBooksByAuthor_ReturnsNotFound_ForUnknownAuthor()
    {
        var controller = new AuthorsController(new BookStoreRepository());

        var result = controller.GetBooksByAuthor(999).Result;

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
