using SecureShoppingPlatform.Models;

namespace SecureShoppingPlatform.Services;

public class ProductService
{
    private readonly List<Product> _products =
    [
        new Product { Id = 1, Name = "Noise Cancelling Headphones", Description = "Comfortable over-ear headphones for work and travel.", Price = 12999 },
        new Product { Id = 2, Name = "Mechanical Keyboard", Description = "Tactile keyboard with compact layout.", Price = 7499 }
    ];

    public IReadOnlyList<Product> GetAll() => _products;

    public Product? GetById(int id) => _products.FirstOrDefault(product => product.Id == id);

    public void AddReview(int productId, string comment)
    {
        var product = GetById(productId) ?? throw new InvalidOperationException("Product not found.");
        product.Reviews.Add(comment.Trim());
    }
}
