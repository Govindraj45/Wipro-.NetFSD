/*
Step by step trigger creation and implementation in MS SQL Server.

How to run in VS Code:
1. Install the "SQL Server (mssql)" extension if it is not already installed.
2. Open this file.
3. Connect to your SQL Server database.
4. Run each section from top to bottom.
*/

/* Step 1: Check the required tables and columns */
SELECT
    TABLE_NAME,
    COLUMN_NAME,
    DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo'
  AND TABLE_NAME IN ('Employees', 'Departments')
ORDER BY TABLE_NAME, ORDINAL_POSITION;

/* Step 2: Drop the trigger if it already exists */
IF OBJECT_ID('dbo.trg_UpdateDepartmentLocation', 'TR') IS NOT NULL
BEGIN
    DROP TRIGGER dbo.trg_UpdateDepartmentLocation;
END;
GO

/* Step 3: Create the AFTER INSERT trigger */
CREATE TRIGGER dbo.trg_UpdateDepartmentLocation
ON dbo.Employees
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE D
    SET D.Location = 'Updated Location'
    FROM dbo.Departments AS D
    INNER JOIN inserted AS I
        ON D.DeptName = I.Department
    WHERE I.Department = 'IT';
END;
GO

/* Step 4: Insert a new employee to test the trigger */
INSERT INTO dbo.Employees
(
    EmpID,
    FirstName,
    LastName,
    Department,
    Salary
)
VALUES
(
    10,
    'John',
    'Snow',
    'IT',
    50000
);

/* Step 5: Verify that the department location was updated */
SELECT
    DeptName,
    Location
FROM dbo.Departments
WHERE DeptName = 'IT';

/*
Notes:
- The trigger uses AFTER INSERT because the department should be updated only
  after a new employee record is successfully inserted.
- The inserted table is a special SQL Server logical table that stores the new
  rows added by the INSERT statement.
- For DELETE triggers, you can use the deleted logical table to insert removed
  records into an audit/log table.
*/
