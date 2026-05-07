# Hotel Reservation System Database

## Domain Description
This is a Capstone Database Project for the **Hospitality** domain. It simulates a real-world enterprise database system, designed and implemented using Microsoft SQL Server to ensure data integrity, security, and efficient querying.

## Features Implemented
- Guest profiles and Room availability
- Reservation booking and check-out tracking
- Automated room status updates via triggers
- Billing and invoicing

## Database Design Approach
Normalized to 3NF. Tracks temporal data like CheckIn/CheckOut to ensure room availability.
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
