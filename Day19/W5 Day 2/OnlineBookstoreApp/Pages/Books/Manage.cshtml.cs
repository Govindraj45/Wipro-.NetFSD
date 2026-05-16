using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookstoreApp.Filters;
using OnlineBookstoreApp.Models;
using OnlineBookstoreApp.Services;

namespace OnlineBookstoreApp.Pages.Books;

[TypeFilter(typeof(AdminInventoryFilter))]
public class ManageModel(IBookRepository repository) : PageModel
{
    [BindProperty]
    public Book Input { get; set; } = new();

    public IReadOnlyList<Book> Books { get; private set; } = [];

    public void OnGet()
    {
        Books = repository.GetAll();
    }

    public IActionResult OnPostSave()
    {
        if (!ModelState.IsValid)
        {
            Books = repository.GetAll();
            return Page();
        }

        if (Input.Id == 0)
        {
            repository.Add(Input);
        }
        else if (!repository.Update(Input))
        {
            return NotFound();
        }

        return RedirectToPage();
    }

    public IActionResult OnPostEdit(int id)
    {
        var book = repository.GetById(id);
        if (book is null)
        {
            return NotFound();
        }

        Input = new Book
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Isbn = book.Isbn,
            Price = book.Price,
            Description = book.Description
        };
        Books = repository.GetAll();
        return Page();
    }

    public IActionResult OnPostDelete(int id)
    {
        repository.Delete(id);
        return RedirectToPage();
    }
}
