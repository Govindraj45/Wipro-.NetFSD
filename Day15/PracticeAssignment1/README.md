# Day 15 Practice Assignment 1

Daily Coding Assignment Wipro NGA .NET Cohort.

## Projects

- `MiddlewareStaticFilesApp` - ASP.NET Core middleware application with static files and basic security headers.
- `RazorPagesItemsApp` - Razor Pages application with PageModel classes, Razor syntax, form binding, and dynamic item display.

## Assignment 1: Building a .NET Core Application with Middleware

Implemented:

- Startup-based middleware configuration.
- Request and response logging middleware.
- Custom error page for unhandled exceptions.
- Static files from `wwwroot`:
  - `index.html`
  - `css/site.css`
  - `js/site.js`
- HTTPS redirection before static file serving.
- Content Security Policy and `X-Content-Type-Options` headers.

## Assignment 2: Implementing Razor Pages with Page Models

Implemented:

- Item list page at `/Items`.
- Add item page at `/Items/Create`.
- `InventoryItem` model with validation attributes.
- PageModel classes for list and create flows.
- Razor `foreach` display and property binding through `[BindProperty]`.
- In-memory `ItemStore` so submitted items appear in the list immediately.

## Run

Run these commands from this folder:

```bash
dotnet build Day15PracticeAssignment1.sln
dotnet run --project MiddlewareStaticFilesApp
dotnet run --project RazorPagesItemsApp
```
