-- DML for Bank Management
INSERT INTO Customers VALUES (1, 'Alice', 'Verified'), (2, 'Bob', 'Verified');
INSERT INTO Accounts VALUES (101, 1, 5000.00), (102, 2, 3000.00);
INSERT INTO Transactions VALUES (1, 101, 1000.00, 'Deposit', GETDATE());
