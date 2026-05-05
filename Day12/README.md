# Day 12: SOLID Principles and Database Basics

This folder contains the Day 12 exercises from the session notes.

## Contents

- `DigitalWalletSolid/` - a SOLID-compliant .NET digital wallet solution with a console demo and MSTest tests.
- `DatabaseBasicsAndEcommerceMiniProject.sql` - SQL Server practice script for CRUD, joins, subqueries, indexes, views, and transactions.

## SOLID Task Coverage

| Task | Focus | Implementation |
| --- | --- | --- |
| T1 | Identify design issues | README mapping below |
| T2 | Separate payment, notification, and transaction responsibilities | `WalletService`, payment classes, notification sender, transaction repository |
| T3 | Payment abstraction | `IPaymentMethod` |
| T4 | Multiple payment types | `UpiPayment`, `CardPayment`, `NetBankingPayment`, `WalletPayment` |
| T5 | Notification abstraction | `INotificationSender` |
| T6 | Constructor dependency injection | `WalletService` constructor |
| T7 | Service uses interfaces instead of concrete classes | `WalletService` depends on abstractions |
| T8 | Add new payment method without changing existing service | `WalletPayment` |
| T9 | Replace payment method safely | MSTest LSP substitution test |
| T10 | Unit tests | `DigitalWallet.Tests` |

## SOLID Mapping

- SRP: wallet balance logic, payment execution, notifications, and transaction storage are separated.
- OCP: new payment methods can be added by implementing `IPaymentMethod`.
- LSP: all payment methods can replace each other through the same `IPaymentMethod` contract.
- ISP: notification and payment interfaces are small and focused.
- DIP: `WalletService` depends on `IPaymentMethod`, `INotificationSender`, and `ITransactionRepository`, not concrete classes.

## Run the .NET Demo

```bash
cd DigitalWalletSolid
dotnet run --project DigitalWallet.Console/DigitalWallet.Console.csproj
```

## Run Tests

```bash
cd DigitalWalletSolid
dotnet test DigitalWalletSolid.sln
```

## Run SQL

1. Open `DatabaseBasicsAndEcommerceMiniProject.sql` in SSMS or VS Code.
2. Connect to SQL Server.
3. Run the script from top to bottom.

Use a practice database because the script creates `CompanyDB` and recreates demo tables.
