# Bank Management System Database

## Domain Description
This is a Capstone Database Project for the **Banking and Financial Services** domain. It simulates a real-world enterprise database system, designed and implemented using Microsoft SQL Server to ensure data integrity, security, and efficient querying.

## Features Implemented
- Customer KYC and Account Management
- Transaction processing (Deposits, Withdrawals)
- Audit logging for all financial transactions
- High balance tracking

## Database Design Approach
Normalized to 3NF/BCNF. Uses robust foreign keys to link Transactions to Accounts and Customers.
The database incorporates constraints (PRIMARY KEY, FOREIGN KEY, CHECK, UNIQUE, NOT NULL) and is fully documented with an ER Diagram (`ERD/er_diagram.png`).

## How to Execute Scripts
1. Open SQL Server Management Studio (SSMS).
2. Connect to your local or remote SQL Server instance.
3. Open and execute the scripts from the `SQL/` folder in the following order:
   - `ddl_scripts.sql` (Creates Tables)
   - `views.sql` (Creates Views)
   - `functions.sql` (Creates Functions)
   - `triggers.sql` (Creates Triggers)
   - `dml_scripts.sql` (Inserts Sample Data)
   - `queries.sql` (Runs Complex Queries)

## Sample Queries & Outputs
This repository includes a minimum of complex queries (utilizing JOINs, aggregations, subqueries) in the `queries.sql` file. Sample results of these queries are documented in the `Output/` folder.
