using System.Data;
using BookstoreAdoNetApp.Models;
using Microsoft.Data.SqlClient;

namespace BookstoreAdoNetApp.Data;

public class BookRepository(IConfiguration configuration, ILogger<BookRepository> logger)
{
    private readonly string _connectionString = configuration.GetConnectionString("BookstoreDb")
        ?? throw new InvalidOperationException("Connection string 'BookstoreDb' is missing.");

    public async Task<IReadOnlyList<Book>> GetAllWithReaderAsync()
    {
        var books = new List<Book>();

        await using var connection = CreateConnection();
        await using var command = new SqlCommand("SELECT BookId, Title, Author, Price, PublishedYear FROM Books ORDER BY BookId", connection);
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            books.Add(MapBook(reader));
        }

        return books;
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        await using var connection = CreateConnection();
        await using var command = new SqlCommand("SELECT BookId, Title, Author, Price, PublishedYear FROM Books WHERE BookId = @BookId", connection);
        command.Parameters.Add("@BookId", SqlDbType.Int).Value = id;

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapBook(reader) : null;
    }

    public async Task<int> AddWithStoredProcedureAsync(Book book)
    {
        ValidateBook(book);

        await using var connection = CreateConnection();
        await using var command = new SqlCommand("usp_AddBook", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        AddBookParameters(command, book);
        var idParameter = command.Parameters.Add("@NewBookId", SqlDbType.Int);
        idParameter.Direction = ParameterDirection.Output;

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
        return (int)idParameter.Value;
    }

    public async Task<bool> UpdateWithStoredProcedureAsync(int id, Book book)
    {
        ValidateBook(book);

        await using var connection = CreateConnection();
        await using var command = new SqlCommand("usp_UpdateBook", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@BookId", SqlDbType.Int).Value = id;
        AddBookParameters(command, book);

        await connection.OpenAsync();
        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteWithStoredProcedureAsync(int id)
    {
        await using var connection = CreateConnection();
        await using var command = new SqlCommand("usp_DeleteBook", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@BookId", SqlDbType.Int).Value = id;

        await connection.OpenAsync();
        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<int> AddWithParameterizedSqlAsync(Book book)
    {
        ValidateBook(book);

        const string sql = """
            INSERT INTO Books (Title, Author, Price, PublishedYear)
            OUTPUT INSERTED.BookId
            VALUES (@Title, @Author, @Price, @PublishedYear)
            """;

        await using var connection = CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        AddBookParameters(command, book);

        await connection.OpenAsync();
        return Convert.ToInt32(await command.ExecuteScalarAsync());
    }

    public async Task<DataSet> GetBooksDataSetAsync()
    {
        await using var connection = CreateConnection();
        using var adapter = new SqlDataAdapter("SELECT BookId, Title, Author, Price, PublishedYear FROM Books ORDER BY BookId", connection);
        using var builder = new SqlCommandBuilder(adapter);
        var dataSet = new DataSet("BookstoreDataSet");

        await Task.Run(() => adapter.Fill(dataSet, "Books"));
        logger.LogInformation("Loaded {Count} books into a disconnected DataSet.", dataSet.Tables["Books"]?.Rows.Count ?? 0);

        return dataSet;
    }

    public async Task<int> SaveBooksDataSetAsync(DataSet dataSet)
    {
        if (!dataSet.Tables.Contains("Books"))
        {
            throw new ArgumentException("DataSet must contain a Books table.", nameof(dataSet));
        }

        await using var connection = CreateConnection();
        using var adapter = new SqlDataAdapter("SELECT BookId, Title, Author, Price, PublishedYear FROM Books", connection);
        using var builder = new SqlCommandBuilder(adapter);

        return await Task.Run(() => adapter.Update(dataSet, "Books"));
    }

    private SqlConnection CreateConnection() => new(_connectionString);

    private static void AddBookParameters(SqlCommand command, Book book)
    {
        command.Parameters.Add("@Title", SqlDbType.NVarChar, 150).Value = book.Title.Trim();
        command.Parameters.Add("@Author", SqlDbType.NVarChar, 100).Value = book.Author.Trim();
        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = book.Price;
        command.Parameters.Add("@PublishedYear", SqlDbType.Int).Value = book.PublishedYear;
    }

    private static void ValidateBook(Book book)
    {
        book.Title = book.Title.Trim();
        book.Author = book.Author.Trim();

        if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author))
        {
            throw new ArgumentException("Title and Author are required.");
        }
    }

    private static Book MapBook(SqlDataReader reader) => new()
    {
        BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
        Title = reader.GetString(reader.GetOrdinal("Title")),
        Author = reader.GetString(reader.GetOrdinal("Author")),
        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
        PublishedYear = reader.GetInt32(reader.GetOrdinal("PublishedYear"))
    };
}
