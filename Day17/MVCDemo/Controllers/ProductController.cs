using Microsoft.AspNetCore.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    // Route attribute for routing
    [Route("api/[controller]")]
    [Route("product")]
    [ApiController]
    public class ProductController : Controller
    {
        // In-memory product list for demo
        private static List<Product> products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Laptop", Price = 50000, Stock = 10, Category = "Electronics", Description = "High-performance laptop" },
            new Product { ProductId = 2, ProductName = "Mouse", Price = 500, Stock = 50, Category = "Electronics", Description = "Wireless mouse" },
            new Product { ProductId = 3, ProductName = "Keyboard", Price = 2000, Stock = 30, Category = "Electronics", Description = "Mechanical keyboard" }
        };

        // GET: product or /api/product
        // Demonstrates: Model Binding from query string, Routing
        [HttpGet("")]
        [HttpGet("list")]
        [Route("")]
        [Route("list")]
        public IActionResult Index([FromQuery] string? category = null, [FromQuery] int pageSize = 10)
        {
            var result = products;
            if (!string.IsNullOrEmpty(category))
            {
                result = products.Where(p => p.Category.Contains(category, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return View(result);
        }

        // GET: product/details/1
        // Demonstrates: Attribute Routing with constraints, Model Binding
        [HttpGet("details/{id:int}")]
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: product/create
        // Demonstrates: Routing
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: product/create
        // Demonstrates: Model Binding from form data, Validation (Data Annotation)
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductName,Price,Stock,Category,Description")] Product product)
        {
            // Server-side validation using ModelState
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            product.ProductId = products.Max(p => p.ProductId) + 1;
            products.Add(product);

            return RedirectToAction(nameof(Details), new { id = product.ProductId });
        }

        // GET: product/edit/1
        // Demonstrates: Routing constraints
        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // POST: product/edit/1
        // Demonstrates: Model Binding, Custom Validation
        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,ProductName,Price,Stock,Category,Description")] Product product)
        {
            if (id != product.ProductId)
                return BadRequest();

            // Custom validation
            if (product.Price <= 0)
            {
                ModelState.AddModelError("Price", "Price must be greater than 0");
            }

            if (product.Stock < 0)
            {
                ModelState.AddModelError("Stock", "Stock cannot be negative");
            }

            if (!ModelState.IsValid)
                return View(product);

            var existingProduct = products.FirstOrDefault(p => p.ProductId == id);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.Category = product.Category;
                existingProduct.Description = product.Description;
            }

            return RedirectToAction(nameof(Details), new { id = product.ProductId });
        }

        // DELETE: product/delete/1
        // Demonstrates: Routing constraint for DELETE method
        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
                return NotFound();

            products.Remove(product);
            return Ok(new { message = "Product deleted successfully" });
        }

        // GET: product/search
        // Demonstrates: Multiple route parameters
        [HttpGet("search/{name}")]
        public IActionResult Search(string name)
        {
            var result = products.Where(p => p.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return Json(result);
        }
    }
}
