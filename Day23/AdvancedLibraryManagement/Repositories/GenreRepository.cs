using AdvancedLibraryManagement.Data;
using AdvancedLibraryManagement.Models;

namespace AdvancedLibraryManagement.Repositories;

public class GenreRepository : EfRepository<Genre>
{
    public GenreRepository(LibraryDbContext db) : base(db)
    {
    }
}
