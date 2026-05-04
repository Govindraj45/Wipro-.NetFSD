# Day 15 Practice Assignment 2

Daily Coding Assignment Wipro NGA .NET Cohort.

## Projects

- `AdvancedRazorPagesApp` - Razor Pages application demonstrating complex model binding, collection binding, partial views, and custom routes.
- `MvcModelBindingApp` - MVC application demonstrating simple and complex model binding with separated Models, Views, and Controllers.

## Assignment 1: Advanced Razor Pages Implementation

Implemented:

- `Product` model with `ProductID`, `Name`, `Description`, and a collection of `Category` objects.
- `/Products` page for adding products with multiple categories and listing saved products.
- `_ProductSummary.cshtml` partial view reused by the product list and category page.
- Custom routes configured in `Startup.cs`:
  - `/Products/{id:int}`
  - `/Products/Category/{categoryName}`

## Assignment 2: MVC Pattern and Model Binding

Implemented:

- `Person` model with simple fields: `FirstName`, `LastName`, and `Age`.
- Nested `Address` model with `Street`, `City`, and `ZipCode`.
- MVC controller actions for GET and POST form handling.
- Views for entering and displaying submitted model-bound data.

## Run

```bash
dotnet build Day15PracticeAssignment2.sln
dotnet run --project AdvancedRazorPagesApp
dotnet run --project MvcModelBindingApp
```

Run the commands from this folder: `Day15/PracticeAssignment2`.
