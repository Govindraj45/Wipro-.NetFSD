# Day17 - Practice Assignment 3: Advanced Order Management System

## Overview
Complete Order Management System demonstrating all advanced validation concepts including model-level validation using `IValidatableObject`, custom validation, and complex business rules.

## Project Structure

```
OrderManagementApp/
├── Models/
│   ├── Customer.cs           (Customer information)
│   ├── Order.cs              (IValidatableObject - model-level validation)
│   └── OrderItem.cs          (IValidatableObject - item validation)
├── Controllers/
│   └── OrderController.cs    (Routing, model binding, custom validation)
├── Views/
│   └── Order/
│       ├── Index.cshtml      (List orders with filtering)
│       ├── Create.cshtml     (Create order with validation)
│       ├── Edit.cshtml       (Edit order)
│       └── Details.cshtml    (Order details)
└── README.md                 (This file)
```

## Concepts Implemented

### 1. Model-Level Validation (IValidatableObject)

#### Order.cs
```csharp
public class Order : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        // Complex business rules validation
        // - Delivery date after order date
        // - Max 30 days delivery window
        // - Valid status checking
        // - Discount limits
        // - Total amount limits
    }
}
```

**Rules Implemented:**
- ✓ Delivery date must be after order date
- ✓ Delivery date cannot be more than 30 days from order date
- ✓ Status must be valid (Pending, Processing, Shipped, Delivered, Cancelled)
- ✓ Discount cannot exceed total amount
- ✓ Order total cannot exceed ₹1,00,00,000 (1 crore)
- ✓ Shipping address required for shipped orders

#### OrderItem.cs
```csharp
public class OrderItem : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        // Item-level business rules
        // - Single item total limits
        // - Bulk order discount requirements
    }
}
```

**Rules Implemented:**
- ✓ Single item total cannot exceed ₹5,00,000
- ✓ Bulk orders (500+ units) must have discount
- ✓ Bulk orders must have at least 5% discount

### 2. Custom Server-Side Validation

In Controller:
```csharp
public IActionResult Create(Order order)
{
    // Built-in validation runs first
    if (!ModelState.IsValid)
        return View(order);
    
    // Custom business logic
    if (order.DiscountAmount < 0)
        ModelState.AddModelError("DiscountAmount", "Custom error");
}
```

### 3. Multiple Binding Types

#### GET Binding (Query String)
```
GET /order/list?customerId=1&status=Pending
```

#### POST Binding (Form Data)
```
POST /order/create
[From Form]
```

#### Route Binding
```
GET /order/details/{id:int}
```

### 4. Routing Implementation

**Attribute Routing:**
```csharp
[HttpGet("")]
[HttpGet("list")]
public IActionResult Index() { }

[HttpGet("details/{id:int}")]
public IActionResult Details(int id) { }

[HttpGet("search/{query}")]
public IActionResult Search(string query) { }
```

### 5. Remote Validation

**Attribute:**
```csharp
[Remote(
    action: "ValidateOrder",
    controller: "Order"
)]
```

**Endpoint:**
```csharp
[AcceptVerbs("Get", "Post")]
public IActionResult ValidateOrder(DateTime orderDate, DateTime deliveryDate)
{
    if (deliveryDate <= orderDate)
        return Json("Delivery date must be after order date");
    return Json(true);
}
```

### 6. TagHelpers & Validation Display

```html
<!-- Validation for single field -->
<span asp-validation-for="DeliveryDate" class="text-danger"></span>

<!-- Validation summary -->
<div asp-validation-summary="ModelOnly" class="alert alert-danger"></div>

<!-- Form binding -->
<input asp-for="OrderDate" type="date" />
<select asp-for="Status" class="form-select">
```

## Running the Application

```bash
cd OrderManagementApp
dotnet run
```

Navigate to: `https://localhost:5001/order`

## Key Features

### Order List (Index)
- ✓ Filter by Customer
- ✓ Filter by Status
- ✓ Display all orders
- ✓ Edit/View actions

### Create Order
- ✓ Select customer
- ✓ Enter order date
- ✓ Enter delivery date (with model-level validation)
- ✓ Select status
- ✓ Enter shipping address (conditional validation)
- ✓ Enter discount and total amounts
- ✓ All validations run before save

### Edit Order
- ✓ Update order details
- ✓ Re-validate all business rules
- ✓ Update status

### Order Details
- ✓ View complete order information
- ✓ Show associated items
- ✓ Display customer details

## Validation Flow

```
1. Form Submission
   ↓
2. Data Annotation Validation (Client-side HTML5 + Server-side)
   ↓
3. Model Binding
   ↓
4. IValidatableObject.Validate() (Model-Level)
   - Delivery date checks
   - Status validation
   - Discount/Amount checks
   - Address requirements
   ↓
5. Custom Controller Validation
   - Business-specific rules
   ↓
6. If Valid: Save to Database
   Else: Redisplay with Errors
```

## Testing Model-Level Validation

### Test Case 1: Invalid Dates
- Order Date: 2024-12-15
- Delivery Date: 2024-12-14 (Before order date)
- **Expected:** Error - "Delivery date must be after order date"

### Test Case 2: Exceeded Time Window
- Order Date: 2024-12-15
- Delivery Date: 2025-02-15 (> 30 days)
- **Expected:** Error - "Delivery date cannot be more than 30 days from order date"

### Test Case 3: Invalid Status
- Status: "InProgress" (not in valid list)
- **Expected:** Error - Status validation fails

### Test Case 4: Missing Address
- Status: "Shipped"
- Shipping Address: Empty
- **Expected:** Error - "Shipping address is required for shipped orders"

### Test Case 5: Discount Exceeds Total
- Total Amount: ₹50,000
- Discount: ₹60,000
- **Expected:** Error - "Discount cannot exceed total order amount"

### Test Case 6: Exceeds Maximum Limit
- Items Total: ₹10,00,00,001 (1 crore + 1)
- **Expected:** Error - "Order total exceeds maximum allowed limit"

## Code Example: Complete Validation

```csharp
// 1. Data Annotations (Automatic)
[Required]
[Range(0, 100)]
public decimal DiscountPercent { get; set; }

// 2. Model-Level (IValidatableObject)
public IEnumerable<ValidationResult> Validate(ValidationContext context)
{
    if (DeliveryDate <= OrderDate)
        yield return new ValidationResult("Invalid dates");
}

// 3. Custom Server-Side (In Controller)
if (order.DiscountAmount < 0)
    ModelState.AddModelError("DiscountAmount", "Custom error");

// 4. Remote Validation (AJAX)
[Remote(action: "ValidateOrder", controller: "Order")]
```

## Advanced Features

### 1. Foreign Key Relationships
- Orders linked to Customers
- OrderItems linked to Orders

### 2. Navigation Properties
- Order.Items (List of OrderItems)
- Customer.Orders (List of Orders)

### 3. Calculated Properties
```csharp
[Display(Name = "Full Name")]
public string FullName => $"{FirstName} {LastName}";

[Display(Name = "Item Total")]
public decimal ItemTotal => Quantity * UnitPrice * (1 - DiscountPercent / 100);
```

### 4. Complex Validation Rules
- Cross-field validation (dates)
- Collection validation (items in order)
- Conditional validation (status-dependent)
- Business rule enforcement (discounts, limits)

## Best Practices Applied

✅ **DO:**
- Use `IValidatableObject` for complex rules
- Implement both client and server validation
- Use `[FromQuery]` and `[FromRoute]` explicitly
- Return meaningful error messages
- Validate in controller for additional checks

❌ **DON'T:**
- Trust only client-side validation
- Put all logic in data annotations
- Skip model-level validation
- Expose internal error details
- Create deeply nested validations

## Summary

This practice assignment demonstrates:
1. **Model-Level Validation** - Using `IValidatableObject` for complex business rules
2. **Custom Validation** - Adding validation in controllers
3. **Multiple Binding Types** - GET, POST, Route parameters
4. **Advanced Routing** - Attribute routing with constraints
5. **TagHelpers** - Form generation and validation display
6. **Remote Validation** - Server-side validation via AJAX
7. **Conditional Validation** - Rules that depend on other properties
8. **Entity Relationships** - Foreign keys and navigation properties

---

**Difficulty Level:** Advanced
**Estimated Time:** 3-4 hours
**Topics Covered:** All validation types, model-level validation, custom validation, routing, binding

**Good Luck! 🚀**
