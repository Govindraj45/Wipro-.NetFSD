# Day 16-2: SQL Case Study - Triggers Scenario

This folder contains the SQL Server trigger case study for data validation, audit tracking, and business rule enforcement.

## File

- `TriggersScenarioCaseStudy.sql` - creates demo `Employees`, `Departments`, and `EmployeeAudit` tables, then creates triggers for tasks T1 to T7.

## Task Coverage

| Task | Trigger Type | Focus |
| --- | --- | --- |
| T1 | `INSTEAD OF INSERT` | Prevent employees with salary less than 30000 |
| T2 | `AFTER INSERT` | Log new employee insertion in `EmployeeAudit` |
| T3 | `INSTEAD OF UPDATE` | Block salary reductions greater than 20% |
| T4 | `AFTER UPDATE` | Log salary changes with old and new salary |
| T5 | `INSTEAD OF DELETE` | Prevent deleting employees from the IT department |
| T6 | `AFTER DELETE` | Log deleted employee ID and old salary |
| T7 | `INSTEAD OF INSERT` | Validate employee `DeptID` against `Departments` |

## Run in VS Code

1. Install the SQL Server (mssql) extension in VS Code.
2. Open `TriggersScenarioCaseStudy.sql`.
3. Connect to your SQL Server practice database.
4. Run the script from top to bottom.

Use a practice database because the script recreates the demo `Employees`, `Departments`, and `EmployeeAudit` tables.

SQL Server allows only one `INSTEAD OF INSERT` trigger on a table, so T1 and T7 are implemented together in one insert-validation trigger.
