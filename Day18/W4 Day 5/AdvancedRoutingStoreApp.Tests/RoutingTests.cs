using AdvancedRoutingStoreApp.Controllers;
using AdvancedRoutingStoreApp.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace AdvancedRoutingStoreApp.Tests;

public class RoutingTests
{
    [Fact]
    public void AllowedCategoryRouteConstraint_AllowsKnownCategory()
    {
        var constraint = new AllowedCategoryRouteConstraint();
        var values = new RouteValueDictionary { ["category"] = "books" };

        var matched = constraint.Match(null, null, "category", values, RouteDirection.IncomingRequest);

        Assert.True(matched);
    }

    [Fact]
    public void PriceRangeRouteConstraint_BlocksUnknownRange()
    {
        var constraint = new PriceRangeRouteConstraint();
        var values = new RouteValueDictionary { ["priceRange"] = "free" };

        var matched = constraint.Match(null, null, "priceRange", values, RouteDirection.IncomingRequest);

        Assert.False(matched);
    }

    [Fact]
    public void GuidSlugRouteConstraint_RequiresValidGuid()
    {
        var constraint = new GuidSlugRouteConstraint();
        var values = new RouteValueDictionary { ["promoId"] = Guid.NewGuid().ToString() };

        var matched = constraint.Match(null, null, "promoId", values, RouteDirection.IncomingRequest);

        Assert.True(matched);
    }

    [Fact]
    public void Checkout_RedirectsGuestToLoginRequired()
    {
        var controller = new ProductsController();

        var result = controller.Checkout("guest");

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("LoginRequired", redirect.ActionName);
        Assert.Equal("Users", redirect.ControllerName);
    }

    [Fact]
    public void Dashboard_ReturnsAdminPayload()
    {
        var controller = new DashboardController();

        var result = controller.Index("admin");

        Assert.IsType<JsonResult>(result);
    }
}
