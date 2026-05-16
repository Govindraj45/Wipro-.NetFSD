using System.Text.Json;
using OnlineBookstoreApp.Models;

namespace OnlineBookstoreApp.Services;

public class CartSessionService(IHttpContextAccessor httpContextAccessor, IBookRepository books)
{
    private const string CartKey = "BOOKSTORE_CART";

    public IReadOnlyList<CartItem> GetCart()
    {
        var session = httpContextAccessor.HttpContext?.Session;
        var raw = session?.GetString(CartKey);
        return string.IsNullOrWhiteSpace(raw)
            ? []
            : JsonSerializer.Deserialize<List<CartItem>>(raw) ?? [];
    }

    public void AddToCart(int bookId)
    {
        var book = books.GetById(bookId) ?? throw new InvalidOperationException("Book not found.");
        var items = GetCart().ToList();
        var existing = items.FirstOrDefault(item => item.BookId == bookId);

        if (existing is null)
        {
            items.Add(new CartItem
            {
                BookId = book.Id,
                Title = book.Title,
                Price = book.Price,
                Quantity = 1
            });
        }
        else
        {
            existing.Quantity++;
        }

        Save(items);
    }

    public void RemoveFromCart(int bookId)
    {
        var items = GetCart().Where(item => item.BookId != bookId).ToList();
        Save(items);
    }

    public void Clear() => Save([]);

    private void Save(IReadOnlyList<CartItem> items)
    {
        httpContextAccessor.HttpContext?.Session.SetString(CartKey, JsonSerializer.Serialize(items));
    }
}
