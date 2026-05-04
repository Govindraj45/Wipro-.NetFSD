using AdvancedRazorPagesApp.Models;

namespace AdvancedRazorPagesApp.Services;

public class ProductStore
{
    private readonly List<Product> _products =
    [
        new()
        {
            ProductID = 101,
            Name = "Learning Portal",
            Description = "ASP.NET Core training portal for cohort assignments.",
            Categories =
            [
                new Category { Name = "Education" },
                new Category { Name = "Web" }
            ]
        },
        new()
        {
            ProductID = 102,
            Name = "Payroll Tracker",
            Description = "Internal dashboard for employee payroll review.",
            Categories =
            [
                new Category { Name = "Finance" },
                new Category { Name = "HR" }
            ]
        }
    ];

    public IReadOnlyList<Product> GetAll() => _products;

    public Product? GetById(int productId)
    {
        return _products.FirstOrDefault(product => product.ProductID == productId);
    }

    public IReadOnlyList<Product> GetByCategory(string categoryName)
    {
        return _products
            .Where(product => product.Categories.Any(category =>
                string.Equals(category.Name, categoryName, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    public bool ProductIdExists(int productId)
    {
        return _products.Any(product => product.ProductID == productId);
    }

    public void Add(Product product)
    {
        product.Categories = product.Categories
            .Where(category => !string.IsNullOrWhiteSpace(category.Name))
            .Select(category => new Category { Name = category.Name.Trim() })
            .ToList();

        _products.Add(product);
    }
}
