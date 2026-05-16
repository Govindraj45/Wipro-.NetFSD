using AdvancedLibraryManagement.Data;
using AdvancedLibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedLibraryManagement.Repositories;

public class BookRepository : EfRepository<Book>, IBookRepository
{
    public BookRepository(LibraryDbContext db) : base(db)
    {
    }

    public async Task<IReadOnlyList<Book>> GetPagedWithDetailsAsync(string? search, int page, int pageSize)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 50);

        var query = Db.Books
            .Include(book => book.Author)
            .Include(book => book.Genres)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(book =>
                book.Title.Contains(search) ||
                (book.Author != null && book.Author.Name.Contains(search)));
        }

        return await query
            .OrderBy(book => book.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Book?> GetWithDetailsAsync(int id)
    {
        return await Db.Books
            .Include(book => book.Author)
            .Include(book => book.Genres)
            .FirstOrDefaultAsync(book => book.BookId == id);
    }

    public async Task<Book> AddBookAsync(string title, int authorId, int[] genreIds)
    {
        var genres = await Db.Genres.Where(genre => genreIds.Contains(genre.GenreId)).ToListAsync();
        var book = new Book
        {
            Title = title.Trim(),
            AuthorId = authorId,
            Genres = genres
        };

        await Db.Books.AddAsync(book);
        await Db.SaveChangesAsync();
        return book;
    }

    public async Task<bool> UpdateBookAsync(int id, string title, int authorId, int[] genreIds)
    {
        var book = await Db.Books.Include(item => item.Genres).FirstOrDefaultAsync(item => item.BookId == id);
        if (book is null)
        {
            return false;
        }

        book.Title = title.Trim();
        book.AuthorId = authorId;
        book.Genres = await Db.Genres.Where(genre => genreIds.Contains(genre.GenreId)).ToListAsync();
        await Db.SaveChangesAsync();
        return true;
    }
}
