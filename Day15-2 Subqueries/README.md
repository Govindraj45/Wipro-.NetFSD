# Day 15-2: SQL Case Study - Subqueries and Joins

This folder contains the SQL Server solution for the Day 15 subqueries and joins exercise.

## File

- `SubqueriesCaseStudy.sql` - creates sample `Employees` and `Departments` tables, inserts practice data, and solves tasks T1 to T10 from the session notes.

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
| T8 | JOIN and grouped derived table rewrite of T5 |
| T9 | JOIN, derived table, and location filtering |
| T10 | Top 2 salaries per department using correlated ranking logic and `DENSE_RANK()` |

## Repo Check

The repository already had a similar subqueries case study under `Day16`, but there was no `Day15-2 Subqueries` folder and the Day 15 T10 ranking exercise was not present there.

## Run in VS Code

1. Install the SQL Server (mssql) extension in VS Code.
2. Open `SubqueriesCaseStudy.sql`.
3. Connect to your SQL Server practice database.
4. Run the script from top to bottom.

Use a practice database because the script recreates the demo `Employees` and `Departments` tables.
