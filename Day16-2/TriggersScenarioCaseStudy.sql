/*
Day 16-2: SQL Case Study - Triggers Scenario

How to run in VS Code:
1. Install the SQL Server (mssql) extension.
2. Open this file in VS Code.
3. Connect to your SQL Server practice database.
4. Run the complete script from top to bottom.

Use a practice database because this script recreates the demo Employees,
Departments, and EmployeeAudit tables.
*/

/* Step 1: Drop existing demo objects */
IF OBJECT_ID('dbo.trg_Employees_ValidateInsert', 'TR') IS NOT NULL
BEGIN
    DROP TRIGGER dbo.trg_Employees_ValidateInsert;
END;
GO

IF OBJECT_ID('dbo.trg_Employees_AuditInsert', 'TR') IS NOT NULL
BEGIN
    DROP TRIGGER dbo.trg_Employees_AuditInsert;
END;
GO

IF OBJECT_ID('dbo.trg_Employees_ValidateUpdate', 'TR') IS NOT NULL
BEGIN
    DROP TRIGGER dbo.trg_Employees_ValidateUpdate;
END;
GO

IF OBJECT_ID('dbo.trg_Employees_AuditSalaryUpdate', 'TR') IS NOT NULL
BEGIN
    DROP TRIGGER dbo.trg_Employees_AuditSalaryUpdate;
END;
GO

IF OBJECT_ID('dbo.trg_Employees_RestrictDelete', 'TR') IS NOT NULL
BEGIN
    DROP TRIGGER dbo.trg_Employees_RestrictDelete;
END;
GO

IF OBJECT_ID('dbo.trg_Employees_AuditDelete', 'TR') IS NOT NULL
BEGIN
    DROP TRIGGER dbo.trg_Employees_AuditDelete;
END;
GO

IF OBJECT_ID('dbo.EmployeeAudit', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.EmployeeAudit;
END;
GO

IF OBJECT_ID('dbo.Employees', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Employees;
END;
GO

IF OBJECT_ID('dbo.Departments', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Departments;
END;
GO

/* Step 2: Create demo tables */
CREATE TABLE dbo.Departments
(
    DeptID INT PRIMARY KEY,
    DeptName VARCHAR(50) NOT NULL,
    Location VARCHAR(50) NOT NULL
);
GO

CREATE TABLE dbo.Employees
(
    EmpID INT PRIMARY KEY,
    EmployeeName VARCHAR(100) NOT NULL,
    DeptID INT NOT NULL,
    Salary INT NOT NULL
);
GO

CREATE TABLE dbo.EmployeeAudit
(
    AuditID INT IDENTITY(1,1) PRIMARY KEY,
    EmpID INT,
    ActionType VARCHAR(10),
    OldSalary INT,
    NewSalary INT,
    ActionDate DATETIME DEFAULT GETDATE()
);
GO

/* Step 3: Insert base department data */
INSERT INTO dbo.Departments (DeptID, DeptName, Location)
VALUES
    (1, 'IT', 'Delhi'),
    (2, 'HR', 'Mumbai'),
    (3, 'Finance', 'Delhi'),
    (4, 'Sales', 'Bengaluru');
GO

/*
T1: Prevent insertion of employees with salary less than 30000.
T7: Restrict inserting employees into departments that do not exist in Departments.

SQL Server allows only one INSTEAD OF INSERT trigger on a table, so both
insert validations are implemented together.
*/
CREATE TRIGGER dbo.trg_Employees_ValidateInsert
ON dbo.Employees
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM inserted
        WHERE Salary < 30000
    )
    BEGIN
        THROW 50001, 'Employee salary must be at least 30000.', 1;
    END;

    IF EXISTS
    (
        SELECT 1
        FROM inserted AS I
        LEFT JOIN dbo.Departments AS D
            ON I.DeptID = D.DeptID
        WHERE D.DeptID IS NULL
    )
    BEGIN
        THROW 50002, 'Employee must belong to an existing department.', 1;
    END;

    INSERT INTO dbo.Employees
    (
        EmpID,
        EmployeeName,
        DeptID,
        Salary
    )
    SELECT
        EmpID,
        EmployeeName,
        DeptID,
        Salary
    FROM inserted;
END;
GO

/* T2: Automatically log every new employee insertion into audit table */
CREATE TRIGGER dbo.trg_Employees_AuditInsert
ON dbo.Employees
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.EmployeeAudit
    (
        EmpID,
        ActionType,
        OldSalary,
        NewSalary
    )
    SELECT
        EmpID,
        'INSERT',
        NULL,
        Salary
    FROM inserted;
END;
GO

/* T3: Prevent updates where salary is reduced by more than 20% */
CREATE TRIGGER dbo.trg_Employees_ValidateUpdate
ON dbo.Employees
INSTEAD OF UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM inserted AS I
        INNER JOIN deleted AS D
            ON I.EmpID = D.EmpID
        WHERE I.Salary < D.Salary * 0.80
    )
    BEGIN
        THROW 50003, 'Salary cannot be reduced by more than 20%.', 1;
    END;

    IF EXISTS
    (
        SELECT 1
        FROM inserted AS I
        LEFT JOIN dbo.Departments AS D
            ON I.DeptID = D.DeptID
        WHERE D.DeptID IS NULL
    )
    BEGIN
        THROW 50004, 'Updated employee department must exist.', 1;
    END;

    UPDATE E
    SET
        E.EmployeeName = I.EmployeeName,
        E.DeptID = I.DeptID,
        E.Salary = I.Salary
    FROM dbo.Employees AS E
    INNER JOIN inserted AS I
        ON E.EmpID = I.EmpID;
END;
GO

/* T4: Log salary changes whenever an employee's salary is updated */
CREATE TRIGGER dbo.trg_Employees_AuditSalaryUpdate
ON dbo.Employees
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.EmployeeAudit
    (
        EmpID,
        ActionType,
        OldSalary,
        NewSalary
    )
    SELECT
        I.EmpID,
        'UPDATE',
        D.Salary,
        I.Salary
    FROM inserted AS I
    INNER JOIN deleted AS D
        ON I.EmpID = D.EmpID
    WHERE I.Salary <> D.Salary;
END;
GO

/* T5: Prevent deletion of employees from the IT department */
CREATE TRIGGER dbo.trg_Employees_RestrictDelete
ON dbo.Employees
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM deleted AS E
        INNER JOIN dbo.Departments AS D
            ON E.DeptID = D.DeptID
        WHERE D.DeptName = 'IT'
    )
    BEGIN
        THROW 50005, 'Employees from IT department cannot be deleted.', 1;
    END;

    DELETE E
    FROM dbo.Employees AS E
    INNER JOIN deleted AS D
        ON E.EmpID = D.EmpID;
END;
GO

/* T6: Log deletion of employees in audit table */
CREATE TRIGGER dbo.trg_Employees_AuditDelete
ON dbo.Employees
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.EmployeeAudit
    (
        EmpID,
        ActionType,
        OldSalary,
        NewSalary
    )
    SELECT
        EmpID,
        'DELETE',
        Salary,
        NULL
    FROM deleted;
END;
GO

/* Step 4: Run successful test cases */
INSERT INTO dbo.Employees (EmpID, EmployeeName, DeptID, Salary)
VALUES
    (101, 'Aarav Sharma', 1, 85000),
    (102, 'Diya Verma', 2, 55000),
    (103, 'Kabir Mehta', 3, 72000),
    (104, 'Neha Singh', 4, 68000);
GO

/* T4 test: allowed salary update, less than 20% reduction */
UPDATE dbo.Employees
SET Salary = 70000
WHERE EmpID = 101;
GO

/* T6 test: allowed delete because employee is not in IT */
DELETE FROM dbo.Employees
WHERE EmpID = 102;
GO

/* Step 5: View final data and audit trail */
SELECT
    EmpID,
    EmployeeName,
    DeptID,
    Salary
FROM dbo.Employees
ORDER BY EmpID;

SELECT
    AuditID,
    EmpID,
    ActionType,
    OldSalary,
    NewSalary,
    ActionDate
FROM dbo.EmployeeAudit
ORDER BY AuditID;

/*
Optional failure tests:
Run each statement separately to see the trigger validation message.

-- T1 failure: salary less than 30000
INSERT INTO dbo.Employees (EmpID, EmployeeName, DeptID, Salary)
VALUES (105, 'Low Salary Employee', 2, 25000);

-- T3 failure: salary reduced by more than 20%
UPDATE dbo.Employees
SET Salary = 50000
WHERE EmpID = 101;

-- T5 failure: deleting an IT employee is blocked
DELETE FROM dbo.Employees
WHERE EmpID = 101;

-- T7 failure: department does not exist
INSERT INTO dbo.Employees (EmpID, EmployeeName, DeptID, Salary)
VALUES (106, 'Invalid Department Employee', 99, 50000);
*/
