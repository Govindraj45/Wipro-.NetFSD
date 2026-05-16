using LibraryEfCoreApp.Models;

namespace LibraryEfCoreApp.Data;

public static class SeedData
{
    public static void Initialize(LibraryDbContext db)
    {
        if (db.Books.Any())
        {
            return;
        }

        var fiction = new Genre { Name = "Fiction" };
        var classic = new Genre { Name = "Classic" };
        var technology = new Genre { Name = "Technology" };

        db.Authors.AddRange(
            new Author
            {
                Name = "George Orwell",
                Bio = "English novelist and essayist.",
                Books =
                [
                    new Book { Title = "1984", Genres = [fiction, classic] },
                    new Book { Title = "Animal Farm", Genres = [fiction] }
                ]
            },
            new Author
            {
                Name = "Robert C. Martin",
                Bio = "Software engineer and author.",
                Books =
                [
                    new Book { Title = "Clean Code", Genres = [technology] }
                ]
            });

        db.SaveChanges();
    }
}
