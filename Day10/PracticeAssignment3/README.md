# Day10 Practice Assignment 3

This solution contains two .NET 8 applications for the Wipro NGA .NET Cohort assignment.

## Assignment 1: MiddlewareStaticFilesApp

- Uses `Startup.cs` to configure the request pipeline.
- Logs incoming request method/path and outgoing response status codes.
- Handles unhandled exceptions with a custom static `error.html` page.
- Serves static files from `wwwroot`.
- Applies HTTPS redirection, Content Security Policy, and browser hardening headers.

Run:

```bash
dotnet run --project MiddlewareStaticFilesApp/MiddlewareStaticFilesApp.csproj
```

Useful URLs:

- `/`
- `/api/status`
- `/throw`

## Assignment 2: RazorPagesItemsApp

- Uses Razor Pages with PageModel classes.
- Includes an item list page and an add-item page.
- Uses Razor syntax to render dynamic table rows with `foreach`.
- Uses `[BindProperty]` and validation attributes to handle form submissions.
- Stores sample data in an in-memory `ItemStore` service.

Run:

```bash
dotnet run --project RazorPagesItemsApp/RazorPagesItemsApp.csproj
```

Useful URLs:

- `/Items`
- `/Items/Create`

## Verification

```bash
dotnet build Day10PracticeAssignment3.sln
```
