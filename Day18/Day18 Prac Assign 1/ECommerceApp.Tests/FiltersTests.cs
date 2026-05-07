using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Xunit;
using ECommerceApp.Filters;

namespace ECommerceApp.Tests
{
    public class FiltersTests
    {
        [Fact]
        public void AuthenticationFilter_ShouldReturnUnauthorized_WhenNotAuthenticated()
        {
            // Arrange
            var filter = new AuthenticationFilter();
            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var filterContext = new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>());

            // Act
            filter.OnAuthorization(filterContext);

            // Assert
            Assert.IsType<UnauthorizedResult>(filterContext.Result);
        }

        [Fact]
        public void AuthenticationFilter_ShouldAllow_WhenAuthenticated()
        {
            // Arrange
            var filter = new AuthenticationFilter();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-User-Auth"] = "valid-token";
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var filterContext = new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>());

            // Act
            filter.OnAuthorization(filterContext);

            // Assert
            Assert.Null(filterContext.Result); // Should not set a result (allows request to proceed)
        }

        [Fact]
        public void GlobalExceptionFilter_ShouldSetErrorViewResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<GlobalExceptionFilter>>();
            var metadataProvider = new EmptyModelMetadataProvider();
            
            var filter = new GlobalExceptionFilter(mockLogger.Object, metadataProvider);
            
            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new System.Exception("Test Exception")
            };

            // Act
            filter.OnException(exceptionContext);

            // Assert
            Assert.True(exceptionContext.ExceptionHandled);
            var viewResult = Assert.IsType<ViewResult>(exceptionContext.Result);
            Assert.Equal("Error", viewResult.ViewName);
        }
    }
}
