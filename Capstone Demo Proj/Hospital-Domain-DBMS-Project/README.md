# Hospital Management System Database

## Project Overview
This capstone project simulates a real-world enterprise database system for a **Hospital Management System (Domain P1)**. It is designed to manage patient records, doctor details, appointments, treatments, and billing information. The project implements a fully normalized relational database (3NF) using Microsoft SQL Server.

## Features Implemented
- **Patient Registration & History:** Tracks patient details and their medical history.
- **Doctor Management:** Stores doctor profiles and specialization mapping.
- **Appointment Booking:** Manages appointment scheduling and status tracking.
- **Treatment Records:** Logs treatments, diagnoses, and prescriptions.
- **Billing System:** Handles invoicing and payment tracking for treatments.
- **Data Integrity:** Enforces relationships and validation via PK/FK, CHECK, UNIQUE, and NOT NULL constraints.
- **Advanced Querying & Reporting:** Implements complex queries using Joins, Subqueries, and Aggregate Functions.
- **Automation & Security:** Uses Triggers for audit logging and Views for data abstraction.

## Database Design Approach
The database was designed following standard normalization rules up to 3NF/BCNF:
- **Entities Identified:** `Patients`, `Doctors`, `Appointments`, `Treatments`, `Billing`, `AuditLogs`.
- **Relationships:**
  - `Patients` (1) to `Appointments` (M)
  - `Doctors` (1) to `Appointments` (M)
  - `Appointments` (1) to `Treatments` (1)
  - `Treatments` (1) to `Billing` (1)

## How to Execute Scripts
To deploy this database locally, execute the SQL scripts in the following order using SQL Server Management Studio (SSMS):
1. **`SQL/ddl_scripts.sql`**: Creates the database, tables, and constraints.
2. **`SQL/views.sql`**: Creates reporting views.
3. **`SQL/functions.sql`**: Creates scalar and table-valued functions.
4. **`SQL/triggers.sql`**: Creates audit logging and automation triggers.
5. **`SQL/dml_scripts.sql`**: Inserts sample data into all tables.
6. **`SQL/queries.sql`**: Contains 10 complex queries that demonstrate database capabilities.

## Sample Queries & Outputs
Please refer to `SQL/queries.sql` for the executable queries. The `Output/` folder contains sample execution results.
