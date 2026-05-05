# Day 16-3: SQL Case Study - Normalization

This folder contains the SQL Server normalization case study for a FinTech banking transaction scenario.

## File

- `NormalizationCaseStudy.sql` - starts with an unnormalized transaction table, then converts it into normalized `Customers`, `CustomerPhoneNumbers`, `Accounts`, `Merchants`, `Transactions`, and `FraudLogs` tables.

## Task Coverage

| Task | Normalization Focus |
| --- | --- |
| T1 | Identify repeated and non-atomic values in an unnormalized table |
| T2 | Apply 1NF by storing each phone number as an atomic row |
| T3 | Apply 2NF by separating account data from transaction data |
| T4 | Apply 3NF by separating merchant details from transaction data |
| T5 | Retrieve complete transaction details using normalized joins |
| T6 | Store fraud logs without duplicating customer or transaction data |
| T7 | Create a reporting view over normalized tables |
| T8 | Run a business query for high-risk customers with failed transactions |
| T9 | Verify that transaction counts match before and after normalization |
| T10 | Add useful indexes after normalization |

## Run in VS Code

1. Install the SQL Server (mssql) extension in VS Code.
2. Open `NormalizationCaseStudy.sql`.
3. Connect to your SQL Server practice database.
4. Run the script from top to bottom.

Use a practice database because the script recreates the demo banking tables.
