namespace LibraryManagementSystem;

/// <summary>
/// Coordinates books and borrowers for the library management workflow.
/// </summary>
public class Library
{
    private readonly List<Book> _books = [];
    private readonly List<Borrower> _borrowers = [];

    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    public IReadOnlyList<Borrower> Borrowers => _borrowers.AsReadOnly();

    public void AddBook(Book book)
    {
        ArgumentNullException.ThrowIfNull(book);

        if (_books.Any(existingBook => existingBook.ISBN == book.ISBN))
        {
            throw new InvalidOperationException("A book with the same ISBN already exists.");
        }

        _books.Add(book);
    }

    public void RegisterBorrower(Borrower borrower)
    {
        ArgumentNullException.ThrowIfNull(borrower);

        if (_borrowers.Any(existingBorrower =>
                existingBorrower.LibraryCardNumber == borrower.LibraryCardNumber))
        {
            throw new InvalidOperationException("A borrower with the same library card number already exists.");
        }

        _borrowers.Add(borrower);
    }

    public void BorrowBook(string isbn, string libraryCardNumber)
    {
        Book book = FindBook(isbn);
        Borrower borrower = FindBorrower(libraryCardNumber);

        borrower.BorrowBook(book);
    }

    public void ReturnBook(string isbn, string libraryCardNumber)
    {
        Book book = FindBook(isbn);
        Borrower borrower = FindBorrower(libraryCardNumber);

        borrower.ReturnBook(book);
    }

    public IReadOnlyList<Book> ViewBooks()
    {
        return Books;
    }

    public IReadOnlyList<Borrower> ViewBorrowers()
    {
        return Borrowers;
    }

    private Book FindBook(string isbn)
    {
        string normalizedIsbn = RequireValue(isbn, nameof(isbn));

        return _books.FirstOrDefault(book => book.ISBN == normalizedIsbn)
            ?? throw new KeyNotFoundException("Book not found.");
    }

    private Borrower FindBorrower(string libraryCardNumber)
    {
        string normalizedLibraryCardNumber = RequireValue(libraryCardNumber, nameof(libraryCardNumber));

        return _borrowers.FirstOrDefault(borrower =>
                borrower.LibraryCardNumber == normalizedLibraryCardNumber)
            ?? throw new KeyNotFoundException("Borrower not found.");
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
