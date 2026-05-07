-- DDL for Blood Bank Management
CREATE TABLE Donors (DonorID INT PRIMARY KEY, Name VARCHAR(100), BloodGroup VARCHAR(5));
CREATE TABLE Inventory (BloodGroup VARCHAR(5) PRIMARY KEY, UnitsAvailable INT);
CREATE TABLE Requests (RequestID INT PRIMARY KEY, HospitalName VARCHAR(100), BloodGroup VARCHAR(5), UnitsRequested INT, Status VARCHAR(20));
CREATE TABLE AuditLogs (LogID INT PRIMARY KEY, TableName VARCHAR(50), Operation VARCHAR(50), LogDate DATETIME DEFAULT GETDATE());
