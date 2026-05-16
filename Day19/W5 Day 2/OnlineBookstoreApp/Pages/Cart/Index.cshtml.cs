using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookstoreApp.Models;
using OnlineBookstoreApp.Services;

namespace OnlineBookstoreApp.Pages.Cart;

public class IndexModel(CartSessionService cart) : PageModel
{
    public IReadOnlyList<CartItem> Items { get; private set; } = [];

    public void OnGet()
    {
        Items = cart.GetCart();
    }

    public IActionResult OnPostRemove(int bookId)
    {
        cart.RemoveFromCart(bookId);
        return RedirectToPage();
    }
}
