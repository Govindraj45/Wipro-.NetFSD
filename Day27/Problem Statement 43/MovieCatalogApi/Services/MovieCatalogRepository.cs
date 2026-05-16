using MovieCatalogApi.Models;

namespace MovieCatalogApi.Services;

public class MovieCatalogRepository
{
    private readonly object _sync = new();
    private readonly List<Director> _directors =
    [
        new() { Id = 1, Name = "Christopher Nolan" },
        new() { Id = 2, Name = "Greta Gerwig" }
    ];

    private readonly List<Movie> _movies =
    [
        new() { Id = 1, Title = "Inception", DirectorId = 1, ReleaseYear = 2010 },
        new() { Id = 2, Title = "Interstellar", DirectorId = 1, ReleaseYear = 2014 },
        new() { Id = 3, Title = "Lady Bird", DirectorId = 2, ReleaseYear = 2017 }
    ];

    private int _nextDirectorId = 3;
    private int _nextMovieId = 4;

    public IReadOnlyList<Director> GetDirectors()
    {
        lock (_sync)
        {
            return _directors.Select(CloneDirector).ToList();
        }
    }

    public Director? GetDirector(int id)
    {
        lock (_sync)
        {
            return _directors.Where(director => director.Id == id).Select(CloneDirector).FirstOrDefault();
        }
    }

    public Director AddDirector(Director director)
    {
        lock (_sync)
        {
            var created = new Director { Id = _nextDirectorId++, Name = director.Name.Trim() };
            _directors.Add(created);
            return CloneDirector(created);
        }
    }

    public bool UpdateDirector(int id, Director director)
    {
        lock (_sync)
        {
            var existing = _directors.FirstOrDefault(item => item.Id == id);
            if (existing is null)
            {
                return false;
            }

            existing.Name = director.Name.Trim();
            return true;
        }
    }

    public bool DeleteDirector(int id)
    {
        lock (_sync)
        {
            var existing = _directors.FirstOrDefault(item => item.Id == id);
            if (existing is null)
            {
                return false;
            }

            _movies.RemoveAll(movie => movie.DirectorId == id);
            _directors.Remove(existing);
            return true;
        }
    }

    public IReadOnlyList<Movie> GetMovies()
    {
        lock (_sync)
        {
            return _movies.Select(CloneMovie).ToList();
        }
    }

    public Movie? GetMovie(int id)
    {
        lock (_sync)
        {
            return _movies.Where(movie => movie.Id == id).Select(CloneMovie).FirstOrDefault();
        }
    }

    public IReadOnlyList<Movie>? GetMoviesByDirector(int directorId)
    {
        lock (_sync)
        {
            if (_directors.All(director => director.Id != directorId))
            {
                return null;
            }

            return _movies.Where(movie => movie.DirectorId == directorId).Select(CloneMovie).ToList();
        }
    }

    public Movie AddMovie(Movie movie)
    {
        lock (_sync)
        {
            var created = new Movie
            {
                Id = _nextMovieId++,
                Title = movie.Title.Trim(),
                DirectorId = movie.DirectorId,
                ReleaseYear = movie.ReleaseYear
            };

            _movies.Add(created);
            return CloneMovie(created);
        }
    }

    public bool UpdateMovie(int id, Movie movie)
    {
        lock (_sync)
        {
            var existing = _movies.FirstOrDefault(item => item.Id == id);
            if (existing is null)
            {
                return false;
            }

            existing.Title = movie.Title.Trim();
            existing.DirectorId = movie.DirectorId;
            existing.ReleaseYear = movie.ReleaseYear;
            return true;
        }
    }

    public bool DeleteMovie(int id)
    {
        lock (_sync)
        {
            var existing = _movies.FirstOrDefault(item => item.Id == id);
            if (existing is null)
            {
                return false;
            }

            _movies.Remove(existing);
            return true;
        }
    }

    public bool DirectorExists(int id)
    {
        lock (_sync)
        {
            return _directors.Any(director => director.Id == id);
        }
    }

    private static Director CloneDirector(Director director) => new()
    {
        Id = director.Id,
        Name = director.Name
    };

    private static Movie CloneMovie(Movie movie) => new()
    {
        Id = movie.Id,
        Title = movie.Title,
        DirectorId = movie.DirectorId,
        ReleaseYear = movie.ReleaseYear
    };
}
