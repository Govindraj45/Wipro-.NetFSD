-- Queries for Bank Management
SELECT c.Name, a.Balance FROM Customers c JOIN Accounts a ON c.CustomerID = a.CustomerID;
SELECT AccountID, SUM(Amount) FROM Transactions GROUP BY AccountID;
-- (Add 8 more similar queries)
SELECT * FROM Transactions WHERE Amount > (SELECT AVG(Amount) FROM Transactions);
