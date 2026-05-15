using ECommerceApp.Models;

namespace ECommerceApp.Services;

public class BookStoreRepository
{
    private readonly object _syncRoot = new();
    private readonly List<Author> _authors =
    [
        new Author { Id = 1, Name = "George Orwell" },
        new Author { Id = 2, Name = "Jane Austen" }
    ];

    private readonly List<Book> _books =
    [
        new Book { Id = 1, Title = "1984", AuthorId = 1, PublicationYear = 1949 },
        new Book { Id = 2, Title = "Animal Farm", AuthorId = 1, PublicationYear = 1945 },
        new Book { Id = 3, Title = "Pride and Prejudice", AuthorId = 2, PublicationYear = 1813 }
    ];

    private int _nextAuthorId = 3;
    private int _nextBookId = 4;

    public IReadOnlyList<Author> GetAuthors()
    {
        lock (_syncRoot)
        {
            return _authors.Select(CloneAuthor).ToList();
        }
    }

    public Author? GetAuthor(int id)
    {
        lock (_syncRoot)
        {
            return _authors.Where(author => author.Id == id).Select(CloneAuthor).FirstOrDefault();
        }
    }

    public Author AddAuthor(Author author)
    {
        lock (_syncRoot)
        {
            var created = new Author { Id = _nextAuthorId++, Name = author.Name.Trim() };
            _authors.Add(created);
            return CloneAuthor(created);
        }
    }

    public bool UpdateAuthor(int id, Author author)
    {
        lock (_syncRoot)
        {
            var existing = _authors.FirstOrDefault(item => item.Id == id);
            if (existing is null)
            {
                return false;
            }

            existing.Name = author.Name.Trim();
            return true;
        }
    }

    public bool DeleteAuthor(int id)
    {
        lock (_syncRoot)
        {
            var existing = _authors.FirstOrDefault(item => item.Id == id);
            if (existing is null)
            {
                return false;
            }

            _books.RemoveAll(book => book.AuthorId == id);
            _authors.Remove(existing);
            return true;
        }
    }

    public IReadOnlyList<Book> GetBooks()
    {
        lock (_syncRoot)
        {
            return _books.Select(CloneBook).ToList();
        }
    }

    public Book? GetBook(int id)
    {
        lock (_syncRoot)
        {
            return _books.Where(book => book.Id == id).Select(CloneBook).FirstOrDefault();
        }
    }

    public IReadOnlyList<Book>? GetBooksByAuthor(int authorId)
    {
        lock (_syncRoot)
        {
            if (_authors.All(author => author.Id != authorId))
            {
                return null;
            }

            return _books.Where(book => book.AuthorId == authorId).Select(CloneBook).ToList();
        }
    }

    public Book AddBook(Book book)
    {
        lock (_syncRoot)
        {
            var created = new Book
            {
                Id = _nextBookId++,
                Title = book.Title.Trim(),
                AuthorId = book.AuthorId,
                PublicationYear = book.PublicationYear
            };

            _books.Add(created);
            return CloneBook(created);
        }
    }

    public bool UpdateBook(int id, Book book)
    {
        lock (_syncRoot)
        {
            var existing = _books.FirstOrDefault(item => item.Id == id);
            if (existing is null)
            {
                return false;
            }

            existing.Title = book.Title.Trim();
            existing.AuthorId = book.AuthorId;
            existing.PublicationYear = book.PublicationYear;
            return true;
        }
    }

    public bool DeleteBook(int id)
    {
        lock (_syncRoot)
        {
            var existing = _books.FirstOrDefault(item => item.Id == id);
            if (existing is null)
            {
                return false;
            }

            _books.Remove(existing);
            return true;
        }
    }

    public bool AuthorExists(int authorId)
    {
        lock (_syncRoot)
        {
            return _authors.Any(author => author.Id == authorId);
        }
    }

    private static Author CloneAuthor(Author author) => new()
    {
        Id = author.Id,
        Name = author.Name
    };

    private static Book CloneBook(Book book) => new()
    {
        Id = book.Id,
        Title = book.Title,
        AuthorId = book.AuthorId,
        PublicationYear = book.PublicationYear
    };
}
