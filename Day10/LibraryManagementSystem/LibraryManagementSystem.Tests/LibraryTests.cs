using LibraryManagementSystem;

namespace LibraryManagementSystem.Tests;

/// <summary>
/// Unit tests for the simple library management workflow.
/// </summary>
public class LibraryTests
{
    [Test]
    public void AddBook_NewBook_AddsBookToLibrary()
    {
        Library library = new();
        Book book = new("Clean Code", "Robert C. Martin", "9780132350884");

        library.AddBook(book);

        Assert.That(library.Books, Has.Count.EqualTo(1));
        Assert.That(library.Books[0], Is.SameAs(book));
        Assert.That(library.Books[0].IsBorrowed, Is.False);
    }

    [Test]
    public void RegisterBorrower_NewBorrower_AddsBorrowerToLibrary()
    {
        Library library = new();
        Borrower borrower = new("Aarav Sharma", "LC1001");

        library.RegisterBorrower(borrower);

        Assert.That(library.Borrowers, Has.Count.EqualTo(1));
        Assert.That(library.Borrowers[0], Is.SameAs(borrower));
        Assert.That(library.Borrowers[0].BorrowedBooks, Is.Empty);
    }

    [Test]
    public void BorrowBook_AvailableBook_MarksBookAsBorrowed()
    {
        (Library library, Book book, Borrower borrower) = CreateLibraryWithBookAndBorrower();

        library.BorrowBook(book.ISBN, borrower.LibraryCardNumber);

        Assert.That(book.IsBorrowed, Is.True);
    }

    [Test]
    public void BorrowBook_AvailableBook_AssociatesBookWithBorrower()
    {
        (Library library, Book book, Borrower borrower) = CreateLibraryWithBookAndBorrower();

        library.BorrowBook(book.ISBN, borrower.LibraryCardNumber);

        Assert.That(borrower.BorrowedBooks, Has.Count.EqualTo(1));
        Assert.That(borrower.BorrowedBooks[0], Is.SameAs(book));
    }

    [Test]
    public void ReturnBook_BorrowedBook_MarksBookAsAvailable()
    {
        (Library library, Book book, Borrower borrower) = CreateLibraryWithBookAndBorrower();
        library.BorrowBook(book.ISBN, borrower.LibraryCardNumber);

        library.ReturnBook(book.ISBN, borrower.LibraryCardNumber);

        Assert.That(book.IsBorrowed, Is.False);
    }

    [Test]
    public void ReturnBook_BorrowedBook_RemovesBookFromBorrower()
    {
        (Library library, Book book, Borrower borrower) = CreateLibraryWithBookAndBorrower();
        library.BorrowBook(book.ISBN, borrower.LibraryCardNumber);

        library.ReturnBook(book.ISBN, borrower.LibraryCardNumber);

        Assert.That(borrower.BorrowedBooks, Is.Empty);
    }

    [Test]
    public void ViewBooks_ReturnsAllBooksWithCurrentStatuses()
    {
        Library library = new();
        Book availableBook = new("The Pragmatic Programmer", "Andrew Hunt and David Thomas", "9780201616224");
        Book borrowedBook = new("Refactoring", "Martin Fowler", "9780134757599");
        Borrower borrower = new("Diya Mehta", "LC1002");

        library.AddBook(availableBook);
        library.AddBook(borrowedBook);
        library.RegisterBorrower(borrower);
        library.BorrowBook(borrowedBook.ISBN, borrower.LibraryCardNumber);

        IReadOnlyList<Book> books = library.ViewBooks();

        Assert.That(books, Has.Count.EqualTo(2));
        Assert.That(books.Single(book => book.ISBN == availableBook.ISBN).IsBorrowed, Is.False);
        Assert.That(books.Single(book => book.ISBN == borrowedBook.ISBN).IsBorrowed, Is.True);
    }

    [Test]
    public void ViewBorrowers_ReturnsAllBorrowersWithBorrowedBooks()
    {
        Library library = new();
        Book book = new("Design Patterns", "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides", "9780201633610");
        Borrower borrowerWithoutBook = new("Kabir Rao", "LC1003");
        Borrower borrowerWithBook = new("Ananya Iyer", "LC1004");

        library.AddBook(book);
        library.RegisterBorrower(borrowerWithoutBook);
        library.RegisterBorrower(borrowerWithBook);
        library.BorrowBook(book.ISBN, borrowerWithBook.LibraryCardNumber);

        IReadOnlyList<Borrower> borrowers = library.ViewBorrowers();

        Assert.That(borrowers, Has.Count.EqualTo(2));
        Assert.That(borrowers.Single(borrower => borrower.LibraryCardNumber == "LC1003").BorrowedBooks, Is.Empty);
        Assert.That(borrowers.Single(borrower => borrower.LibraryCardNumber == "LC1004").BorrowedBooks, Contains.Item(book));
    }

    private static (Library Library, Book Book, Borrower Borrower) CreateLibraryWithBookAndBorrower()
    {
        Library library = new();
        Book book = new("Domain-Driven Design", "Eric Evans", "9780321125217");
        Borrower borrower = new("Rohan Patel", "LC1000");

        library.AddBook(book);
        library.RegisterBorrower(borrower);

        return (library, book, borrower);
    }
}
