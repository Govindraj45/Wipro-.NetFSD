namespace LibraryManagementSystem;

/// <summary>
/// Represents a registered library borrower and the books currently checked out by them.
/// </summary>
public class Borrower
{
    private readonly List<Book> _borrowedBooks = [];

    public Borrower(string name, string libraryCardNumber)
    {
        Name = RequireValue(name, nameof(name));
        LibraryCardNumber = RequireValue(libraryCardNumber, nameof(libraryCardNumber));
    }

    public string Name { get; }

    public string LibraryCardNumber { get; }

    public IReadOnlyList<Book> BorrowedBooks => _borrowedBooks.AsReadOnly();

    public void BorrowBook(Book book)
    {
        ArgumentNullException.ThrowIfNull(book);

        if (_borrowedBooks.Any(b => b.ISBN == book.ISBN))
        {
            throw new InvalidOperationException("This borrower already has the book.");
        }

        book.Borrow();
        _borrowedBooks.Add(book);
    }

    public void ReturnBook(Book book)
    {
        ArgumentNullException.ThrowIfNull(book);

        if (!_borrowedBooks.Contains(book))
        {
            throw new InvalidOperationException("This borrower has not borrowed the book.");
        }

        book.Return();
        _borrowedBooks.Remove(book);
    }

    private static string RequireValue(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be empty.", parameterName);
        }

        return value.Trim();
    }
}
