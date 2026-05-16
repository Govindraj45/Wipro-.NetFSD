# W5 Day 4 - Library System with EF Core

This folder contains `LibraryEfCoreApp`, an ASP.NET Core Web API demonstrating EF Core Code First concepts for a library system.

## Covered Requirements

- Code First entities for `Book`, `Author`, and `Genre`.
- One-to-many relationship from Author to Books.
- Many-to-many relationship from Books to Genres using Fluent API.
- CRUD operations for books, authors, and genres.
- Optimized query for books with authors and genres using `Include`, `AsNoTracking`, and ordering.
- Database First guidance in `LibraryEfCoreApp/Data/database-first-notes.md`.

The project uses EF Core InMemory so it can build and run without requiring a local SQL Server during assignment review.
