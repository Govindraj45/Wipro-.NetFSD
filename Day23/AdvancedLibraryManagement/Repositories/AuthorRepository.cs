using AdvancedLibraryManagement.Data;
using AdvancedLibraryManagement.Models;

namespace AdvancedLibraryManagement.Repositories;

public class AuthorRepository : EfRepository<Author>
{
    public AuthorRepository(LibraryDbContext db) : base(db)
    {
    }
}
