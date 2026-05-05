/*
Day 12: Database Basics and E-Commerce Mini Project

How to run:
1. Open this file in SSMS or VS Code with the SQL Server (mssql) extension.
2. Connect to a SQL Server instance.
3. Run the script from top to bottom.

Use a practice database because this script creates and recreates demo tables.
*/

IF DB_ID('CompanyDB') IS NULL
BEGIN
    CREATE DATABASE CompanyDB;
END;
GO

USE CompanyDB;
GO

/* Part 1: Basic CRUD practice from the session */
IF OBJECT_ID('dbo.WiproEmployees', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.WiproEmployees;
END;
GO

CREATE TABLE dbo.WiproEmployees
(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Age INT NOT NULL,
    City VARCHAR(50) NOT NULL
);
GO

INSERT INTO dbo.WiproEmployees (Name, Age, City)
VALUES
    ('John Doe', 30, 'New York'),
    ('Jane Smith', 25, 'Los Angeles'),
    ('Alice Johnson', 28, 'Chicago'),
    ('Bob Brown', 35, 'Houston'),
    ('Charlie Davis', 32, 'Phoenix');

SELECT
    ID,
    Name,
    Age,
    City
FROM dbo.WiproEmployees
ORDER BY ID;

UPDATE dbo.WiproEmployees
SET City = 'San Francisco'
WHERE Name = 'Alice Johnson';

UPDATE dbo.WiproEmployees
SET Age = 29
WHERE ID = 3;

DELETE FROM dbo.WiproEmployees
WHERE Name = 'Bob Brown';

SELECT
    ID,
    Name,
    Age,
    City
FROM dbo.WiproEmployees
WHERE Age > 23
ORDER BY Age DESC;

/* Part 2: E-Commerce database mini project */
IF OBJECT_ID('dbo.OrderItems', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.OrderItems;
END;
GO

IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Orders;
END;
GO

IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Products;
END;
GO

IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Customers;
END;
GO

CREATE TABLE dbo.Customers
(
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName VARCHAR(80) NOT NULL,
    City VARCHAR(50) NOT NULL
);
GO

CREATE TABLE dbo.Products
(
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(80) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    StockQuantity INT NOT NULL,
    CONSTRAINT CK_Products_Price CHECK (Price > 0),
    CONSTRAINT CK_Products_StockQuantity CHECK (StockQuantity >= 0)
);
GO

CREATE TABLE dbo.Orders
(
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status VARCHAR(20) NOT NULL,
    CONSTRAINT FK_Orders_Customers
        FOREIGN KEY (CustomerID) REFERENCES dbo.Customers(CustomerID)
);
GO

CREATE TABLE dbo.OrderItems
(
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    CONSTRAINT FK_OrderItems_Orders
        FOREIGN KEY (OrderID) REFERENCES dbo.Orders(OrderID),
    CONSTRAINT FK_OrderItems_Products
        FOREIGN KEY (ProductID) REFERENCES dbo.Products(ProductID),
    CONSTRAINT CK_OrderItems_Quantity CHECK (Quantity > 0)
);
GO

INSERT INTO dbo.Customers (CustomerName, City)
VALUES
    ('Amit Sharma', 'Delhi'),
    ('Neha Verma', 'Mumbai'),
    ('Rahul Mehta', 'Bengaluru');

INSERT INTO dbo.Products (ProductName, Price, StockQuantity)
VALUES
    ('Laptop', 65000.00, 10),
    ('Wireless Mouse', 1200.00, 50),
    ('Keyboard', 2200.00, 30),
    ('Monitor', 15500.00, 15);

INSERT INTO dbo.Orders (CustomerID, Status)
VALUES
    (1, 'PLACED'),
    (2, 'PLACED'),
    (1, 'SHIPPED');

INSERT INTO dbo.OrderItems (OrderID, ProductID, Quantity, UnitPrice)
VALUES
    (1, 1, 1, 65000.00),
    (1, 2, 2, 1200.00),
    (2, 3, 1, 2200.00),
    (3, 4, 1, 15500.00);

/* Join: Which customer placed which product orders? */
SELECT
    C.CustomerName,
    O.OrderID,
    O.OrderDate,
    P.ProductName,
    OI.Quantity,
    OI.UnitPrice,
    OI.Quantity * OI.UnitPrice AS LineTotal,
    O.Status
FROM dbo.Customers AS C
INNER JOIN dbo.Orders AS O
    ON C.CustomerID = O.CustomerID
INNER JOIN dbo.OrderItems AS OI
    ON O.OrderID = OI.OrderID
INNER JOIN dbo.Products AS P
    ON OI.ProductID = P.ProductID
ORDER BY O.OrderID, P.ProductName;

/* Subquery: Products priced above the average product price */
SELECT
    ProductID,
    ProductName,
    Price
FROM dbo.Products
WHERE Price > (
    SELECT AVG(Price)
    FROM dbo.Products
)
ORDER BY Price DESC;

/* View: Reporting layer for order totals */
IF OBJECT_ID('dbo.vw_OrderTotals', 'V') IS NOT NULL
BEGIN
    DROP VIEW dbo.vw_OrderTotals;
END;
GO

CREATE VIEW dbo.vw_OrderTotals
AS
SELECT
    O.OrderID,
    C.CustomerName,
    O.Status,
    SUM(OI.Quantity * OI.UnitPrice) AS OrderTotal
FROM dbo.Orders AS O
INNER JOIN dbo.Customers AS C
    ON O.CustomerID = C.CustomerID
INNER JOIN dbo.OrderItems AS OI
    ON O.OrderID = OI.OrderID
GROUP BY
    O.OrderID,
    C.CustomerName,
    O.Status;
GO

SELECT
    OrderID,
    CustomerName,
    Status,
    OrderTotal
FROM dbo.vw_OrderTotals
ORDER BY OrderTotal DESC;

/* Index: Faster product search */
CREATE INDEX IX_Products_ProductName
ON dbo.Products(ProductName);
GO

/* Transaction: Deduct inventory safely for an order */
BEGIN TRY
    BEGIN TRANSACTION;

    UPDATE P
    SET P.StockQuantity = P.StockQuantity - OI.Quantity
    FROM dbo.Products AS P
    INNER JOIN dbo.OrderItems AS OI
        ON P.ProductID = OI.ProductID
    WHERE OI.OrderID = 1
      AND P.StockQuantity >= OI.Quantity;

    IF @@ROWCOUNT = 0
    BEGIN
        THROW 50001, 'Inventory update failed because stock is insufficient.', 1;
    END;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    BEGIN
        ROLLBACK TRANSACTION;
    END;

    THROW;
END CATCH;
GO

SELECT
    ProductID,
    ProductName,
    StockQuantity
FROM dbo.Products
ORDER BY ProductID;
