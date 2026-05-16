# W5 Day 3 - Bookstore Application with ADO.NET

This folder contains `BookstoreAdoNetApp`, an ASP.NET Core Web API assignment for managing books with ADO.NET.

## Covered Requirements

- SQL Server connectivity with `SqlConnection`.
- CRUD operations with `SqlCommand`.
- SQL injection prevention through parameterized queries.
- Stored procedure calls for add, update, and delete.
- `SqlDataReader` for connected forward-only reads.
- `SqlDataAdapter`, `DataSet`, and `DataTable` for disconnected data handling.
- SQL setup script in `BookstoreAdoNetApp/Data/database.sql`.

## API Routes

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/books` | Read books using `SqlDataReader` |
| GET | `/api/books/{id}` | Read one book |
| GET | `/api/books/dataset` | Demonstrate disconnected `DataSet` loading |
| POST | `/api/books` | Add a book through stored procedure |
| PUT | `/api/books/{id}` | Update a book through stored procedure |
| DELETE | `/api/books/{id}` | Delete a book through stored procedure |
