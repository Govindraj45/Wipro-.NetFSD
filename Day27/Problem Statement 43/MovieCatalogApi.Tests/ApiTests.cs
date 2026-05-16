using Microsoft.AspNetCore.Mvc;
using MovieCatalogApi.Controllers;
using MovieCatalogApi.Models;
using MovieCatalogApi.Services;
using Xunit;

namespace MovieCatalogApi.Tests;

public class ApiTests
{
    [Fact]
    public void GetMovies_ReturnsSeedData()
    {
        var controller = new MoviesController(new MovieCatalogRepository());

        var result = controller.GetMovies().Result;

        var ok = Assert.IsType<OkObjectResult>(result);
        var movies = Assert.IsAssignableFrom<IEnumerable<Movie>>(ok.Value);
        Assert.Contains(movies, movie => movie.Title == "Inception");
    }

    [Fact]
    public void CreateMovie_ReturnsBadRequest_ForUnknownDirector()
    {
        var controller = new MoviesController(new MovieCatalogRepository());

        var result = controller.CreateMovie(new Movie { Title = "Test", DirectorId = 999, ReleaseYear = 2024 }).Result;

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void GetMoviesByDirector_ReturnsNotFound_ForUnknownDirector()
    {
        var controller = new DirectorsController(new MovieCatalogRepository());

        var result = controller.GetMoviesByDirector(999).Result;

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
