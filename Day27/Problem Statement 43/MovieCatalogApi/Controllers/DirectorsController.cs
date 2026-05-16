using Microsoft.AspNetCore.Mvc;
using MovieCatalogApi.Models;
using MovieCatalogApi.Services;

namespace MovieCatalogApi.Controllers;

[ApiController]
[Route("api/directors")]
public class DirectorsController(MovieCatalogRepository repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Director>> GetDirectors()
    {
        return Ok(repository.GetDirectors());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Director> GetDirector(int id)
    {
        var director = repository.GetDirector(id);
        return director is null ? NotFound(new { message = $"Director with id {id} was not found." }) : Ok(director);
    }

    [HttpGet("{directorId:int}/movies")]
    public ActionResult<IEnumerable<Movie>> GetMoviesByDirector(int directorId)
    {
        var movies = repository.GetMoviesByDirector(directorId);
        return movies is null ? NotFound(new { message = $"Director with id {directorId} was not found." }) : Ok(movies);
    }

    [HttpPost]
    public ActionResult<Director> CreateDirector(Director director)
    {
        var created = repository.AddDirector(director);
        return CreatedAtAction(nameof(GetDirector), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateDirector(int id, Director director)
    {
        return repository.UpdateDirector(id, director)
            ? NoContent()
            : NotFound(new { message = $"Director with id {id} was not found." });
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteDirector(int id)
    {
        return repository.DeleteDirector(id)
            ? NoContent()
            : NotFound(new { message = $"Director with id {id} was not found." });
    }
}
