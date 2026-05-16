using AdvancedLibraryManagement.Models;

namespace AdvancedLibraryManagement.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<IReadOnlyList<Book>> GetPagedWithDetailsAsync(string? search, int page, int pageSize);
    Task<Book?> GetWithDetailsAsync(int id);
    Task<Book> AddBookAsync(string title, int authorId, int[] genreIds);
    Task<bool> UpdateBookAsync(int id, string title, int authorId, int[] genreIds);
}
