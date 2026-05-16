CREATE DATABASE BookstoreDb;
GO

USE BookstoreDb;
GO

CREATE TABLE Books
(
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(150) NOT NULL,
    Author NVARCHAR(100) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    PublishedYear INT NOT NULL
);
GO

CREATE OR ALTER PROCEDURE usp_AddBook
    @Title NVARCHAR(150),
    @Author NVARCHAR(100),
    @Price DECIMAL(10, 2),
    @PublishedYear INT,
    @NewBookId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Books (Title, Author, Price, PublishedYear)
    VALUES (@Title, @Author, @Price, @PublishedYear);

    SET @NewBookId = SCOPE_IDENTITY();
END;
GO

CREATE OR ALTER PROCEDURE usp_UpdateBook
    @BookId INT,
    @Title NVARCHAR(150),
    @Author NVARCHAR(100),
    @Price DECIMAL(10, 2),
    @PublishedYear INT
AS
BEGIN
    UPDATE Books
    SET Title = @Title,
        Author = @Author,
        Price = @Price,
        PublishedYear = @PublishedYear
    WHERE BookId = @BookId;
END;
GO

CREATE OR ALTER PROCEDURE usp_DeleteBook
    @BookId INT
AS
BEGIN
    DELETE FROM Books
    WHERE BookId = @BookId;
END;
GO

INSERT INTO Books (Title, Author, Price, PublishedYear)
VALUES
    ('Clean Code', 'Robert C. Martin', 650.00, 2008),
    ('The Pragmatic Programmer', 'Andrew Hunt', 700.00, 1999);
