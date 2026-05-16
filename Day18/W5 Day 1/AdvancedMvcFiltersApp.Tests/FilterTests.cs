using AdvancedMvcFiltersApp.Filters;
using AdvancedMvcFiltersApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AdvancedMvcFiltersApp.Tests;

public class FilterTests
{
    [Fact]
    public void SessionAuthenticationFilter_ReturnsUnauthorized_WhenMissingHeader()
    {
        var service = new DemoUserContextService();
        var filter = new SessionAuthenticationFilter(service);
        var context = CreateAuthorizationContext();

        filter.OnAuthorization(context);

        Assert.IsType<UnauthorizedObjectResult>(context.Result);
    }

    [Fact]
    public void SessionAuthenticationFilter_AllowsAuthenticatedUser()
    {
        var service = new DemoUserContextService();
        var context = CreateAuthorizationContext();
        context.HttpContext.Request.Headers["X-User-Auth"] = "true";

        var filter = new SessionAuthenticationFilter(service);
        filter.OnAuthorization(context);

        Assert.Null(context.Result);
    }

    [Fact]
    public void RoleAuthorizationFilter_BlocksWrongRole()
    {
        var service = new DemoUserContextService();
        var context = CreateAuthorizationContext();
        context.HttpContext.Request.Headers["X-User-Role"] = "User";

        var filter = new RoleAuthorizationFilter(service, "Admin");
        filter.OnAuthorization(context);

        Assert.IsType<ForbidResult>(context.Result);
    }

    [Fact]
    public void GlobalErrorFilter_HandlesException()
    {
        var logger = new Mock<ILogger<GlobalErrorFilter>>();
        var context = new ExceptionContext(
            new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()),
            new List<IFilterMetadata>())
        {
            Exception = new InvalidOperationException("boom")
        };

        var filter = new GlobalErrorFilter(logger.Object);
        filter.OnException(context);

        Assert.True(context.ExceptionHandled);
        Assert.IsType<ObjectResult>(context.Result);
    }

    private static AuthorizationFilterContext CreateAuthorizationContext()
    {
        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        return new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>());
    }
}
