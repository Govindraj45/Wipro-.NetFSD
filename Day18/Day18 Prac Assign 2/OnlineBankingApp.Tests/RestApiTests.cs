using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.Controllers;
using OnlineBankingApp.Models;
using OnlineBankingApp.Services;

namespace OnlineBankingApp.Tests
{
    public class RestApiTests
    {
        [Fact]
        public void GetMovies_ReturnsSeededMovies()
        {
            var controller = new MoviesController(new MovieCatalogRepository());

            var result = controller.GetMovies().Result;

            var okResult = Assert.IsType<OkObjectResult>(result);
            var movies = Assert.IsAssignableFrom<IEnumerable<Movie>>(okResult.Value);
            Assert.Contains(movies, movie => movie.Title == "Inception");
        }

        [Fact]
        public void GetMovie_ReturnsNotFound_ForMissingMovie()
        {
            var controller = new MoviesController(new MovieCatalogRepository());

            var result = controller.GetMovie(999).Result;

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void CreateMovie_ReturnsCreated_WhenDirectorExists()
        {
            var repository = new MovieCatalogRepository();
            var controller = new MoviesController(repository);
            var movie = new Movie
            {
                Title = "Barbie",
                DirectorId = 2,
                ReleaseYear = 2023
            };

            var result = controller.CreateMovie(movie).Result;

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var createdMovie = Assert.IsType<Movie>(created.Value);
            Assert.Equal("Barbie", repository.GetMovie(createdMovie.Id)?.Title);
        }

        [Fact]
        public void CreateMovie_ReturnsBadRequest_WhenDirectorIsMissing()
        {
            var controller = new MoviesController(new MovieCatalogRepository());

            var result = controller.CreateMovie(new Movie { Title = "Unknown", DirectorId = 999, ReleaseYear = 2024 }).Result;

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetMoviesByDirector_ReturnsNotFound_WhenDirectorIsMissing()
        {
            var controller = new DirectorsController(new MovieCatalogRepository());

            var result = controller.GetMoviesByDirector(999).Result;

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
