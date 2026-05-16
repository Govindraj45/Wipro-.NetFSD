using OnlineBookstoreApp.Models;

namespace OnlineBookstoreApp.Services;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books =
    [
        new Book { Id = 1, Title = "Clean Architecture", Author = "Robert C. Martin", Isbn = "9780134494166", Price = 760, Description = "A practical guide for application structure." },
        new Book { Id = 2, Title = "Domain-Driven Design", Author = "Eric Evans", Isbn = "9780321125217", Price = 880, Description = "Designing complex software with better boundaries." },
        new Book { Id = 3, Title = "Refactoring", Author = "Martin Fowler", Isbn = "9780134757599", Price = 690, Description = "Improving code with small safe transformations." }
    ];

    private int _nextId = 4;

    public IReadOnlyList<Book> GetAll() => _books.OrderBy(book => book.Title).ToList();

    public Book? GetById(int id) => _books.FirstOrDefault(book => book.Id == id);

    public void Add(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);
    }

    public bool Update(Book book)
    {
        var existing = GetById(book.Id);
        if (existing is null)
        {
            return false;
        }

        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Isbn = book.Isbn;
        existing.Price = book.Price;
        existing.Description = book.Description;
        return true;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        return existing is not null && _books.Remove(existing);
    }
}
