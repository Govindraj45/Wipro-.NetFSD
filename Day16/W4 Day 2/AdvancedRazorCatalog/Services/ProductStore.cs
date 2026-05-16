using AdvancedRazorCatalog.Models;

namespace AdvancedRazorCatalog.Services;

public class ProductStore
{
    private readonly List<Product> _products =
    [
        new Product
        {
            ProductId = 1,
            Name = "Travel Backpack",
            Description = "Compact bag for work and weekend trips.",
            Categories = [new Category { Name = "Accessories" }, new Category { Name = "Travel" }]
        },
        new Product
        {
            ProductId = 2,
            Name = "Wireless Mouse",
            Description = "Lightweight mouse for productivity.",
            Categories = [new Category { Name = "Electronics" }]
        }
    ];

    private int _nextId = 3;

    public IReadOnlyList<Product> GetAll() => _products.OrderBy(product => product.Name).ToList();

    public Product? GetById(int id) => _products.FirstOrDefault(product => product.ProductId == id);

    public IReadOnlyList<Product> GetByCategory(string slug) =>
        _products.Where(product => product.Categories.Any(category => category.Slug == slug)).ToList();

    public void Add(ProductInputModel input)
    {
        var categories = input.Categories
            .Where(category => !string.IsNullOrWhiteSpace(category.Name))
            .Select(category => new Category { Name = category.Name.Trim() })
            .ToList();

        _products.Add(new Product
        {
            ProductId = _nextId++,
            Name = input.Name.Trim(),
            Description = input.Description.Trim(),
            Categories = categories
        });
    }
}
