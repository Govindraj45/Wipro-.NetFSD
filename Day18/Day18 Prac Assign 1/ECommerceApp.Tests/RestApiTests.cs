using ECommerceApp.Controllers;
using ECommerceApp.Models;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ECommerceApp.Tests
{
    public class RestApiTests
    {
        [Fact]
        public void GetBooks_ReturnsSeededBooks()
        {
            var controller = new BooksController(new BookStoreRepository());

            var result = controller.GetBooks().Result;

            var okResult = Assert.IsType<OkObjectResult>(result);
            var books = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Assert.Contains(books, book => book.Title == "1984");
        }

        [Fact]
        public void GetBook_ReturnsNotFound_ForMissingBook()
        {
            var controller = new BooksController(new BookStoreRepository());

            var result = controller.GetBook(999).Result;

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void CreateBook_ReturnsCreated_WhenAuthorExists()
        {
            var repository = new BookStoreRepository();
            var controller = new BooksController(repository);
            var book = new Book
            {
                Title = "Emma",
                AuthorId = 2,
                PublicationYear = 1815
            };

            var result = controller.CreateBook(book).Result;

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var createdBook = Assert.IsType<Book>(created.Value);
            Assert.Equal("Emma", repository.GetBook(createdBook.Id)?.Title);
        }

        [Fact]
        public void CreateBook_ReturnsBadRequest_WhenAuthorIsMissing()
        {
            var controller = new BooksController(new BookStoreRepository());

            var result = controller.CreateBook(new Book { Title = "Unknown", AuthorId = 999, PublicationYear = 2024 }).Result;

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetBooksByAuthor_ReturnsNotFound_WhenAuthorIsMissing()
        {
            var controller = new AuthorsController(new BookStoreRepository());

            var result = controller.GetBooksByAuthor(999).Result;

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
