/*
Day 16: Case Study based on Subqueries and Joins

How to run in SQL Server:
1. Open this file in SQL Server Management Studio or VS Code with the mssql extension.
2. Connect to a practice database.
3. Run the complete script from top to bottom.

The setup section recreates demo Employees and Departments tables so every
query has predictable results.
*/

/* Step 1: Recreate practice tables */
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
    Salary DECIMAL(10, 2) NOT NULL,
    CONSTRAINT FK_Employees_Departments
        FOREIGN KEY (DeptID) REFERENCES dbo.Departments(DeptID)
);
GO

/* Step 2: Insert sample data */
INSERT INTO dbo.Departments (DeptID, DeptName, Location)
VALUES
    (1, 'IT', 'Delhi'),
    (2, 'HR', 'Mumbai'),
    (3, 'Finance', 'Delhi'),
    (4, 'Sales', 'Bengaluru'),
    (5, 'Operations', 'Chennai');

INSERT INTO dbo.Employees (EmpID, EmployeeName, DeptID, Salary)
VALUES
    (101, 'Aarav Sharma', 1, 85000.00),
    (102, 'Diya Verma', 1, 72000.00),
    (103, 'Kabir Mehta', 1, 95000.00),
    (104, 'Isha Rao', 2, 58000.00),
    (105, 'Neha Singh', 2, 64000.00),
    (106, 'Rohan Gupta', 3, 90000.00),
    (107, 'Priya Nair', 3, 88000.00),
    (108, 'Vikram Das', 4, 76000.00),
    (109, 'Ananya Jain', 4, 82000.00),
    (110, 'Rahul Bose', 5, 60000.00);

/* Step 3: View base data */
SELECT * FROM dbo.Departments ORDER BY DeptID;
SELECT * FROM dbo.Employees ORDER BY EmpID;

/* T1: Retrieve employees whose salary is greater than the overall average salary */
SELECT
    EmpID,
    EmployeeName,
    DeptID,
    Salary
FROM dbo.Employees
WHERE Salary > (
    SELECT AVG(Salary)
    FROM dbo.Employees
)
ORDER BY Salary DESC;

/* T2: Retrieve employees working in departments located in 'Delhi' */
SELECT
    EmpID,
    EmployeeName,
    DeptID,
    Salary
FROM dbo.Employees
WHERE DeptID IN (
    SELECT DeptID
    FROM dbo.Departments
    WHERE Location = 'Delhi'
)
ORDER BY EmployeeName;

/* T3: Retrieve employees whose salary is equal to the maximum salary in the organization */
SELECT
    EmpID,
    EmployeeName,
    DeptID,
    Salary
FROM dbo.Employees
WHERE Salary = (
    SELECT MAX(Salary)
    FROM dbo.Employees
);

/* T4: Retrieve employees earning more than their respective department average salary */
SELECT
    E.EmpID,
    E.EmployeeName,
    E.DeptID,
    E.Salary
FROM dbo.Employees AS E
WHERE E.Salary > (
    SELECT AVG(E2.Salary)
    FROM dbo.Employees AS E2
    WHERE E2.DeptID = E.DeptID
)
ORDER BY E.DeptID, E.Salary DESC;

/* T5: Retrieve employees who are the highest paid within their department */
SELECT
    E.EmpID,
    E.EmployeeName,
    E.DeptID,
    E.Salary
FROM dbo.Employees AS E
WHERE E.Salary = (
    SELECT MAX(E2.Salary)
    FROM dbo.Employees AS E2
    WHERE E2.DeptID = E.DeptID
)
ORDER BY E.DeptID;

/* T6: Identify departments where average salary is greater than the overall company average salary */
SELECT
    D.DeptID,
    D.DeptName,
    D.Location,
    AVG(E.Salary) AS DepartmentAverageSalary
FROM dbo.Departments AS D
INNER JOIN dbo.Employees AS E
    ON D.DeptID = E.DeptID
GROUP BY
    D.DeptID,
    D.DeptName,
    D.Location
HAVING AVG(E.Salary) > (
    SELECT AVG(Salary)
    FROM dbo.Employees
)
ORDER BY DepartmentAverageSalary DESC;

/* T7: Rewrite T4 using JOIN instead of subquery */
SELECT
    E.EmpID,
    E.EmployeeName,
    E.DeptID,
    E.Salary,
    DA.DepartmentAverageSalary
FROM dbo.Employees AS E
INNER JOIN
(
    SELECT
        DeptID,
        AVG(Salary) AS DepartmentAverageSalary
    FROM dbo.Employees
    GROUP BY DeptID
) AS DA
    ON E.DeptID = DA.DeptID
WHERE E.Salary > DA.DepartmentAverageSalary
ORDER BY E.DeptID, E.Salary DESC;

/* T8: Rewrite T5 using JOIN and GROUP BY */
SELECT
    E.EmpID,
    E.EmployeeName,
    E.DeptID,
    E.Salary
FROM dbo.Employees AS E
INNER JOIN
(
    SELECT
        DeptID,
        MAX(Salary) AS HighestDepartmentSalary
    FROM dbo.Employees
    GROUP BY DeptID
) AS DS
    ON E.DeptID = DS.DeptID
   AND E.Salary = DS.HighestDepartmentSalary
ORDER BY E.DeptID;

/*
T9: Retrieve employee name, department name, and salary where salary is greater
than department average AND department location is 'Delhi'
*/
SELECT
    E.EmployeeName,
    D.DeptName,
    E.Salary,
    DA.DepartmentAverageSalary,
    D.Location
FROM dbo.Employees AS E
INNER JOIN dbo.Departments AS D
    ON E.DeptID = D.DeptID
INNER JOIN
(
    SELECT
        DeptID,
        AVG(Salary) AS DepartmentAverageSalary
    FROM dbo.Employees
    GROUP BY DeptID
) AS DA
    ON E.DeptID = DA.DeptID
WHERE E.Salary > DA.DepartmentAverageSalary
  AND D.Location = 'Delhi'
ORDER BY D.DeptName, E.Salary DESC;
