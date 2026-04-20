# Day 4 Practice Assignment

This assignment contains five collection-based C# console scenarios plus an OOP feature demo.

## Scenarios

| Scenario | Collections Used |
| --- | --- |
| E-Commerce Order Management System | `List<Order>`, `Dictionary<int, Customer>`, `HashSet<string>`, `Queue<Order>`, `Stack<string>` |
| Social Media User Engagement System | `List<string>`, `Dictionary<string, int>`, `HashSet<int>`, `Stack<string>`, `Queue<string>` |
| Banking Transaction System | `List<Transaction>`, `Dictionary<string, double>`, `Queue<Transaction>`, `Stack<Transaction>`, `HashSet<string>` |
| Music Playlist Manager | `LinkedList<string>`, `SortedList<int, string>`, `SortedDictionary<string, string>` |
| Task Scheduler System | `Queue<string>`, `Stack<string>`, `List<string>`, `SortedDictionary<int, string>`, `HashSet<string>` |

## OOP Features Covered

| Feature | Example |
| --- | --- |
| Static | `TrainingModule.CreatedModuleCount` tracks object count. |
| Constructors | `TrainingModule` initializes module name and duration. |
| Interfaces | `IPrintable` defines printable behavior. |
| Const | `TrainingModule.PlatformName` stores a compile-time constant. |

## SOLID Approach

The code is split by responsibility:

| Layer | Responsibility |
| --- | --- |
| Demo classes | Run each assignment scenario. |
| Service interfaces | Define behavior without depending on implementation details. |
| Service classes | Contain collection logic and business operations. |
| Model classes | Store scenario data such as orders, customers, and transactions. |
