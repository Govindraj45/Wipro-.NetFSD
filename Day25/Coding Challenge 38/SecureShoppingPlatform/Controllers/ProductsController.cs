using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureShoppingPlatform.Models;
using SecureShoppingPlatform.Services;

namespace SecureShoppingPlatform.Controllers;

public class ProductsController(ProductService products) : Controller
{
    public IActionResult Index() => View(products.GetAll());

    public IActionResult Details(int id)
    {
        var product = products.GetById(id);
        if (product is null)
        {
            return NotFound();
        }

        ViewBag.ReviewInput = new ReviewInputModel { ProductId = product.Id };
        return View(product);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Review(ReviewInputModel input)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Details), new { id = input.ProductId });
        }

        products.AddReview(input.ProductId, input.Comment);
        return RedirectToAction(nameof(Details), new { id = input.ProductId });
    }
}
