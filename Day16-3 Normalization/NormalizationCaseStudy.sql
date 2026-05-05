/*
Day 16-3: SQL Case Study - Normalization

How to run in VS Code:
1. Install the SQL Server (mssql) extension.
2. Open this file in VS Code.
3. Connect to your SQL Server practice database.
4. Run the complete script from top to bottom.

Use a practice database because this script recreates demo banking tables.
The case study starts with one unnormalized transaction table, then converts it
into a normalized FinTech schema using 1NF, 2NF, and 3NF design ideas.
*/

/* Step 1: Drop existing demo objects */
IF OBJECT_ID('dbo.vw_TransactionReport', 'V') IS NOT NULL
BEGIN
    DROP VIEW dbo.vw_TransactionReport;
END;
GO

IF OBJECT_ID('dbo.FraudLogs', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.FraudLogs;
END;
GO

IF OBJECT_ID('dbo.Transactions', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Transactions;
END;
GO

IF OBJECT_ID('dbo.Accounts', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Accounts;
END;
GO

IF OBJECT_ID('dbo.CustomerPhoneNumbers', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.CustomerPhoneNumbers;
END;
GO

IF OBJECT_ID('dbo.Merchants', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Merchants;
END;
GO

IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Customers;
END;
GO

IF OBJECT_ID('dbo.RawTransactionRecords', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.RawTransactionRecords;
END;
GO

/* Step 2: Create an unnormalized source table */
CREATE TABLE dbo.RawTransactionRecords
(
    RawID INT IDENTITY(1,1) PRIMARY KEY,
    TxnID INT NOT NULL,
    AccountID INT NOT NULL,
    AccountType VARCHAR(30) NOT NULL,
    AccountBalance DECIMAL(12, 2) NOT NULL,
    CustomerID INT NOT NULL,
    CustomerName VARCHAR(100) NOT NULL,
    CustomerCity VARCHAR(50) NOT NULL,
    CustomerRiskScore INT NOT NULL,
    PhoneNumbers VARCHAR(100) NOT NULL,
    MerchantID INT NOT NULL,
    MerchantName VARCHAR(100) NOT NULL,
    MerchantCategory VARCHAR(50) NOT NULL,
    Amount DECIMAL(12, 2) NOT NULL,
    TxnDate DATETIME NOT NULL,
    Status VARCHAR(20) NOT NULL,
    RiskFlag VARCHAR(30) NULL,
    FraudCreatedDate DATETIME NULL
);
GO

INSERT INTO dbo.RawTransactionRecords
(
    TxnID,
    AccountID,
    AccountType,
    AccountBalance,
    CustomerID,
    CustomerName,
    CustomerCity,
    CustomerRiskScore,
    PhoneNumbers,
    MerchantID,
    MerchantName,
    MerchantCategory,
    Amount,
    TxnDate,
    Status,
    RiskFlag,
    FraudCreatedDate
)
VALUES
    (9001, 501, 'Savings', 125000.00, 101, 'Amit Sharma', 'Delhi', 72, '9876543210,9876543211', 301, 'Amazon India', 'E-Commerce', 2500.00, '2026-05-01T10:15:00', 'SUCCESS', NULL, NULL),
    (9002, 501, 'Savings', 125000.00, 101, 'Amit Sharma', 'Delhi', 72, '9876543210,9876543211', 302, 'Flipkart', 'E-Commerce', 4200.00, '2026-05-01T12:20:00', 'FAILED', 'HIGH_RISK', '2026-05-01T12:21:00'),
    (9003, 502, 'Current', 245000.00, 102, 'Neha Verma', 'Mumbai', 35, '9988776655', 303, 'Big Bazaar', 'Retail', 1800.00, '2026-05-02T09:30:00', 'SUCCESS', NULL, NULL),
    (9004, 503, 'Savings', 98000.00, 103, 'Rahul Mehta', 'Bengaluru', 81, '9123456780,9123456781', 304, 'Uber', 'Travel', 950.00, '2026-05-02T20:10:00', 'FAILED', 'SUSPICIOUS_LOCATION', '2026-05-02T20:12:00'),
    (9005, 504, 'Salary', 76000.00, 104, 'Priya Nair', 'Chennai', 44, '9000011111', 301, 'Amazon India', 'E-Commerce', 3100.00, '2026-05-03T11:05:00', 'SUCCESS', NULL, NULL),
    (9006, 503, 'Savings', 98000.00, 103, 'Rahul Mehta', 'Bengaluru', 81, '9123456780,9123456781', 305, 'IRCTC', 'Travel', 2200.00, '2026-05-03T18:40:00', 'SUCCESS', NULL, NULL);
GO

/* T1: View unnormalized data and identify repeated/non-atomic values */
SELECT
    RawID,
    TxnID,
    CustomerName,
    AccountID,
    PhoneNumbers,
    MerchantName,
    Amount,
    Status
FROM dbo.RawTransactionRecords
ORDER BY RawID;

SELECT
    CustomerID,
    CustomerName,
    PhoneNumbers
FROM dbo.RawTransactionRecords
WHERE PhoneNumbers LIKE '%,%'
GROUP BY
    CustomerID,
    CustomerName,
    PhoneNumbers;

/* Step 3: Create normalized tables */
CREATE TABLE dbo.Customers
(
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(100) NOT NULL,
    City VARCHAR(50) NOT NULL,
    RiskScore INT NOT NULL,
    CONSTRAINT CK_Customers_RiskScore
        CHECK (RiskScore BETWEEN 0 AND 100)
);
GO

CREATE TABLE dbo.CustomerPhoneNumbers
(
    CustomerID INT NOT NULL,
    PhoneNumber VARCHAR(20) NOT NULL,
    CONSTRAINT PK_CustomerPhoneNumbers
        PRIMARY KEY (CustomerID, PhoneNumber),
    CONSTRAINT FK_CustomerPhoneNumbers_Customers
        FOREIGN KEY (CustomerID) REFERENCES dbo.Customers(CustomerID)
);
GO

CREATE TABLE dbo.Accounts
(
    AccountID INT PRIMARY KEY,
    CustomerID INT NOT NULL,
    AccountType VARCHAR(30) NOT NULL,
    Balance DECIMAL(12, 2) NOT NULL,
    CONSTRAINT FK_Accounts_Customers
        FOREIGN KEY (CustomerID) REFERENCES dbo.Customers(CustomerID)
);
GO

CREATE TABLE dbo.Merchants
(
    MerchantID INT PRIMARY KEY,
    MerchantName VARCHAR(100) NOT NULL,
    MerchantCategory VARCHAR(50) NOT NULL
);
GO

CREATE TABLE dbo.Transactions
(
    TxnID INT PRIMARY KEY,
    AccountID INT NOT NULL,
    MerchantID INT NOT NULL,
    Amount DECIMAL(12, 2) NOT NULL,
    TxnDate DATETIME NOT NULL,
    Status VARCHAR(20) NOT NULL,
    CONSTRAINT FK_Transactions_Accounts
        FOREIGN KEY (AccountID) REFERENCES dbo.Accounts(AccountID),
    CONSTRAINT FK_Transactions_Merchants
        FOREIGN KEY (MerchantID) REFERENCES dbo.Merchants(MerchantID),
    CONSTRAINT CK_Transactions_Status
        CHECK (Status IN ('SUCCESS', 'FAILED', 'PENDING'))
);
GO

CREATE TABLE dbo.FraudLogs
(
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    TxnID INT NOT NULL,
    RiskFlag VARCHAR(30) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    CONSTRAINT FK_FraudLogs_Transactions
        FOREIGN KEY (TxnID) REFERENCES dbo.Transactions(TxnID)
);
GO

/* Step 4: Move distinct data into normalized tables */
INSERT INTO dbo.Customers
(
    CustomerID,
    CustomerName,
    City,
    RiskScore
)
SELECT DISTINCT
    CustomerID,
    CustomerName,
    CustomerCity,
    CustomerRiskScore
FROM dbo.RawTransactionRecords;

INSERT INTO dbo.CustomerPhoneNumbers
(
    CustomerID,
    PhoneNumber
)
SELECT DISTINCT
    R.CustomerID,
    TRIM(S.value) AS PhoneNumber
FROM dbo.RawTransactionRecords AS R
CROSS APPLY STRING_SPLIT(R.PhoneNumbers, ',') AS S;

INSERT INTO dbo.Accounts
(
    AccountID,
    CustomerID,
    AccountType,
    Balance
)
SELECT DISTINCT
    AccountID,
    CustomerID,
    AccountType,
    AccountBalance
FROM dbo.RawTransactionRecords;

INSERT INTO dbo.Merchants
(
    MerchantID,
    MerchantName,
    MerchantCategory
)
SELECT DISTINCT
    MerchantID,
    MerchantName,
    MerchantCategory
FROM dbo.RawTransactionRecords;

INSERT INTO dbo.Transactions
(
    TxnID,
    AccountID,
    MerchantID,
    Amount,
    TxnDate,
    Status
)
SELECT DISTINCT
    TxnID,
    AccountID,
    MerchantID,
    Amount,
    TxnDate,
    Status
FROM dbo.RawTransactionRecords;

INSERT INTO dbo.FraudLogs
(
    TxnID,
    RiskFlag,
    CreatedDate
)
SELECT
    TxnID,
    RiskFlag,
    FraudCreatedDate
FROM dbo.RawTransactionRecords
WHERE RiskFlag IS NOT NULL;
GO

/* T2: 1NF - each phone number is now stored as one atomic value */
SELECT
    C.CustomerID,
    C.CustomerName,
    P.PhoneNumber
FROM dbo.Customers AS C
INNER JOIN dbo.CustomerPhoneNumbers AS P
    ON C.CustomerID = P.CustomerID
ORDER BY C.CustomerID, P.PhoneNumber;

/* T3: 2NF - account details depend on AccountID, not on each transaction row */
SELECT
    A.AccountID,
    C.CustomerName,
    A.AccountType,
    A.Balance
FROM dbo.Accounts AS A
INNER JOIN dbo.Customers AS C
    ON A.CustomerID = C.CustomerID
ORDER BY A.AccountID;

/* T4: 3NF - merchant details are stored once and referenced by transactions */
SELECT
    M.MerchantID,
    M.MerchantName,
    M.MerchantCategory,
    COUNT(T.TxnID) AS TransactionCount
FROM dbo.Merchants AS M
LEFT JOIN dbo.Transactions AS T
    ON M.MerchantID = T.MerchantID
GROUP BY
    M.MerchantID,
    M.MerchantName,
    M.MerchantCategory
ORDER BY M.MerchantID;

/* T5: Retrieve complete transaction details using joins */
SELECT
    T.TxnID,
    C.CustomerName,
    A.AccountType,
    M.MerchantName,
    M.MerchantCategory,
    T.Amount,
    T.TxnDate,
    T.Status
FROM dbo.Transactions AS T
INNER JOIN dbo.Accounts AS A
    ON T.AccountID = A.AccountID
INNER JOIN dbo.Customers AS C
    ON A.CustomerID = C.CustomerID
INNER JOIN dbo.Merchants AS M
    ON T.MerchantID = M.MerchantID
ORDER BY T.TxnID;

/* T6: Retrieve fraud logs without duplicating transaction/customer data */
SELECT
    F.LogID,
    F.TxnID,
    C.CustomerName,
    T.Amount,
    T.Status,
    F.RiskFlag,
    F.CreatedDate
FROM dbo.FraudLogs AS F
INNER JOIN dbo.Transactions AS T
    ON F.TxnID = T.TxnID
INNER JOIN dbo.Accounts AS A
    ON T.AccountID = A.AccountID
INNER JOIN dbo.Customers AS C
    ON A.CustomerID = C.CustomerID
ORDER BY F.LogID;
GO

/* T7: Create a reporting view over normalized tables */
CREATE VIEW dbo.vw_TransactionReport
AS
SELECT
    T.TxnID,
    C.CustomerID,
    C.CustomerName,
    C.City,
    C.RiskScore,
    A.AccountID,
    A.AccountType,
    M.MerchantID,
    M.MerchantName,
    M.MerchantCategory,
    T.Amount,
    T.TxnDate,
    T.Status
FROM dbo.Transactions AS T
INNER JOIN dbo.Accounts AS A
    ON T.AccountID = A.AccountID
INNER JOIN dbo.Customers AS C
    ON A.CustomerID = C.CustomerID
INNER JOIN dbo.Merchants AS M
    ON T.MerchantID = M.MerchantID;
GO

SELECT
    TxnID,
    CustomerName,
    City,
    AccountType,
    MerchantName,
    Amount,
    Status
FROM dbo.vw_TransactionReport
ORDER BY TxnID;

/* T8: Business query - high-risk customers with failed transactions */
SELECT
    CustomerID,
    CustomerName,
    RiskScore,
    COUNT(TxnID) AS FailedTransactionCount,
    SUM(Amount) AS FailedAmount
FROM dbo.vw_TransactionReport
WHERE Status = 'FAILED'
  AND RiskScore >= 70
GROUP BY
    CustomerID,
    CustomerName,
    RiskScore
ORDER BY FailedTransactionCount DESC, FailedAmount DESC;

/* T9: Verify that normalization did not lose transaction records */
SELECT
    (SELECT COUNT(*) FROM dbo.RawTransactionRecords) AS RawTransactionCount,
    (SELECT COUNT(*) FROM dbo.Transactions) AS NormalizedTransactionCount;

/* T10: Optional useful indexes after normalization */
CREATE INDEX IX_Accounts_CustomerID
ON dbo.Accounts(CustomerID);
GO

CREATE INDEX IX_Transactions_AccountID_TxnDate
ON dbo.Transactions(AccountID, TxnDate DESC);
GO

CREATE INDEX IX_Transactions_Status
ON dbo.Transactions(Status);
GO

CREATE INDEX IX_FraudLogs_TxnID
ON dbo.FraudLogs(TxnID);
GO

EXEC sp_helpindex 'dbo.Transactions';
GO
