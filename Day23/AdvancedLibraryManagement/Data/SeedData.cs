using AdvancedLibraryManagement.Models;

namespace AdvancedLibraryManagement.Data;

public static class SeedData
{
    public static void Initialize(LibraryDbContext db)
    {
        if (db.Books.Any())
        {
            return;
        }

        var classic = new Genre { Name = "Classic" };
        var dystopian = new Genre { Name = "Dystopian" };
        var software = new Genre { Name = "Software" };

        db.Authors.AddRange(
            new Author
            {
                Name = "George Orwell",
                Books =
                [
                    new Book { Title = "1984", Genres = [classic, dystopian] },
                    new Book { Title = "Animal Farm", Genres = [classic] }
                ]
            },
            new Author
            {
                Name = "Martin Fowler",
                Books =
                [
                    new Book { Title = "Refactoring", Genres = [software] }
                ]
            });

        db.SaveChanges();
    }
}
