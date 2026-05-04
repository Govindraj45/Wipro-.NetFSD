# Day 16: Case Study based on Subqueries and Joins

This folder contains the SQL Server solution for the Day 16 case study.

## File

- `SubqueriesAndJoinsCaseStudy.sql` - creates sample `Employees` and `Departments` tables, inserts practice data, and solves tasks T1 to T9.

## Task Coverage

| Task | Focus |
| --- | --- |
| T1 | Scalar subquery with `AVG()` |
| T2 | `IN` subquery filtered by department location |
| T3 | Aggregate subquery with `MAX()` |
| T4 | Correlated subquery using department average salary |
| T5 | Correlated subquery using maximum salary per department |
| T6 | Nested subquery in `HAVING` |
| T7 | JOIN rewrite of T4 |
| T8 | JOIN and `GROUP BY` rewrite of T5 |
| T9 | JOIN, derived table, and location filtering |

## Run in VS Code

1. Install the SQL Server (mssql) extension in VS Code.
2. Open `SubqueriesAndJoinsCaseStudy.sql`.
3. Connect to your SQL Server practice database.
4. Run the script from top to bottom.

Use a practice database because the script recreates the demo `Employees` and `Departments` tables.
