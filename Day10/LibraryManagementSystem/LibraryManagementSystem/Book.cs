namespace LibraryManagementSystem;

/// <summary>
/// Represents a book stored by the library.
/// </summary>
public class Book
{
    public Book(string title, string author, string isbn)
    {
        Title = RequireValue(title, nameof(title));
        Author = RequireValue(author, nameof(author));
        ISBN = RequireValue(isbn, nameof(isbn));
    }

    public string Title { get; }

    public string Author { get; }

    public string ISBN { get; }

    public bool IsBorrowed { get; private set; }

    public void Borrow()
    {
        if (IsBorrowed)
        {
            throw new InvalidOperationException("The book is already borrowed.");
        }

        IsBorrowed = true;
    }

    public void Return()
    {
        if (!IsBorrowed)
        {
            throw new InvalidOperationException("The book is not currently borrowed.");
        }

        IsBorrowed = false;
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
