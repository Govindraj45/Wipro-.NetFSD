# Banking Transaction System - Exception Handling Case Study

## Problem Statement
The objective of this project is to develop a Banking Transaction System that accurately simulates common banking operations (deposits, withdrawals, and balance inquiries) while strictly enforcing business rules using robust exception handling. 

The system validates that:
- The minimum balance must remain at least ₹1000.
- A withdrawal amount cannot exceed the available balance.
- Deposit and withdrawal amounts must be strictly greater than zero.

## Exception Types Used
To ensure a reliable system, the following exceptions are handled:
1. **`InsufficientBalanceException`** (Custom): Thrown when a withdrawal exceeds the available balance, or when a transaction would cause the account balance to fall below the ₹1000 limit.
2. **`InvalidAmountException`** (Custom): Thrown when an invalid transaction amount (zero or negative) is provided for a deposit or withdrawal.
3. **`ArgumentException`** (Built-in): Used to prevent invalid account creation (e.g., empty or null Account Holder Name).
4. **`InvalidOperationException`** (Built-in): Included in the `try-catch` blocks as a standard safeguard for unexpected state exceptions.
5. **`Exception`** (Built-in): The generic fallback exception catcher.

## Sample Output
```text
========================================
 Banking Transaction System
========================================
[Balance] Account Holder: John Doe | Current Balance: ₹5000

--- Scenario 1: Valid Deposit ---
[Success] Deposited ₹2000. New Balance: ₹7000

--- Scenario 2: Valid Withdraw ---
[Success] Withdrew ₹1500. New Balance: ₹5500

--- Scenario 3: Deposit <= 0 ---
[Error - Invalid Amount]: Deposit amount must be greater than 0.
Transaction attempt finished.

--- Scenario 4: Withdraw > balance ---
[Error - Insufficient Balance]: Cannot withdraw ₹10000. Amount exceeds current balance of ₹5500.
Transaction attempt finished.

--- Scenario 5: Withdraw causing balance < 1000 ---
[Error - Insufficient Balance]: Cannot withdraw ₹4800. Minimum balance of ₹1000 must be maintained.
Transaction attempt finished.

========================================
 End of Banking Operations
========================================
```

## How to Run the Code
This project is built using C# and .NET. You can run it directly from the terminal or using an IDE (Visual Studio, JetBrains Rider, VS Code).

**Using the .NET CLI:**
1. Open your terminal or command prompt.
2. Navigate to the project directory: `cd "Day5 - Assignment/CSharp_ExceptionHandling_CaseStudy"`
3. Compile and execute the code by running:
   ```bash
   dotnet run
   ```
