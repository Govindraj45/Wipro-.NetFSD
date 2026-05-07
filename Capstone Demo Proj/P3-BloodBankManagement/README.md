# Blood Bank Management System Database

## Domain Description
This is a Capstone Database Project for the **Healthcare and Blood Inventory** domain. It simulates a real-world enterprise database system, designed and implemented using Microsoft SQL Server to ensure data integrity, security, and efficient querying.

## Features Implemented
- Donor registration and blood typing
- Inventory tracking by blood group
- Hospital request management
- Low stock alerts

## Database Design Approach
Normalized to 3NF. Connects Requests to Inventory for accurate stock levels.
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
