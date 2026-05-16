# Database First Notes

For a real SQL Server database that already contains `Authors`, `Books`, `Genres`, and a join table such as `BookGenres`, reverse engineer the schema with:

```bash
dotnet ef dbcontext scaffold "Server=localhost;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Models -c LibraryDbContext --context-dir Data
```

After scaffolding, keep generated entity classes focused on database shape and place custom business logic in partial classes or services so future scaffold refreshes do not overwrite it.
