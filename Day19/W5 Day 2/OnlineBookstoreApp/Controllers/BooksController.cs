using Microsoft.AspNetCore.Mvc;
using OnlineBookstoreApp.Services;

namespace OnlineBookstoreApp.Controllers;

public class BooksController(IBookRepository books, CartSessionService cart) : Controller
{
    public IActionResult Index()
    {
        return View(books.GetAll());
    }

    public IActionResult Details(int id)
    {
        var book = books.GetById(id);
        return book is null ? NotFound() : View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddToCart(int id)
    {
        cart.AddToCart(id);
        return RedirectToPage("/Cart/Index");
    }
}
