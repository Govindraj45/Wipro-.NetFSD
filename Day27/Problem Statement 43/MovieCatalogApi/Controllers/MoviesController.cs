using Microsoft.AspNetCore.Mvc;
using MovieCatalogApi.Models;
using MovieCatalogApi.Services;

namespace MovieCatalogApi.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController(MovieCatalogRepository repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Movie>> GetMovies()
    {
        return Ok(repository.GetMovies());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Movie> GetMovie(int id)
    {
        var movie = repository.GetMovie(id);
        return movie is null ? NotFound(new { message = $"Movie with id {id} was not found." }) : Ok(movie);
    }

    [HttpPost]
    public ActionResult<Movie> CreateMovie(Movie movie)
    {
        if (!repository.DirectorExists(movie.DirectorId))
        {
            return BadRequest(new { message = $"Director with id {movie.DirectorId} does not exist." });
        }

        var created = repository.AddMovie(movie);
        return CreatedAtAction(nameof(GetMovie), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateMovie(int id, Movie movie)
    {
        if (!repository.DirectorExists(movie.DirectorId))
        {
            return BadRequest(new { message = $"Director with id {movie.DirectorId} does not exist." });
        }

        return repository.UpdateMovie(id, movie)
            ? NoContent()
            : NotFound(new { message = $"Movie with id {id} was not found." });
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteMovie(int id)
    {
        return repository.DeleteMovie(id)
            ? NoContent()
            : NotFound(new { message = $"Movie with id {id} was not found." });
    }
}
