# Day 17 - Practice Assignments

## Assignment 1: Razor Pages - Student Management System

### Objective
Create a complete Razor Pages application for managing student information with proper binding and validation.

### Requirements

#### Part A: Model Creation
Create a `Student` model with the following properties:
- StudentId (Primary Key)
- FirstName (Required, 2-50 chars)
- LastName (Required, 2-50 chars)
- Email (Required, valid email)
- PhoneNumber (Required, 10 digits)
- DateOfBirth (Required, must be 18+)
- GPA (0-4.0)
- EnrollmentDate (Current date)
- Major (CS, IT, ECE, ME)

#### Part B: Razor Pages
1. **Index.cshtml** - List all students (with Search)
   - Display GET binding for search by name
   - Show all students in a table
   - Include action buttons

2. **Create.cshtml** - Register new student
   - Use POST binding
   - Show validation messages
   - Include all form fields

3. **Edit.cshtml** - Edit student details
   - Populate existing data
   - Update student

4. **Details.cshtml** - View full student details
   - Display all information
   - Calculate age from DOB

#### Part C: Validation
Implement:
- Data Annotation validation
- Server-side validation (age check)
- Custom validation (phone number format)

### Expected Output
- Student registration form working
- Search functionality
- Error messages displayed properly
- Two-way binding working correctly

---

## Assignment 2: MVC - Product Inventory System

### Objective
Build a complete MVC application with routing, model binding, and validations.

### Requirements

#### Part A: Models
Create `Product` and `Inventory` models:

**Product:**
- ProductId
- ProductName (Required, 3-100 chars)
- Category (Electronics, Clothing, Food, etc.)
- Price (Positive)
- Description
- ManufacturerEmail (Valid email)

**Inventory:**
- InventoryId
- ProductId (Foreign Key)
- Stock (0-10000)
- ReorderLevel
- WarehouseLocation

#### Part B: Controllers & Routing
Create `ProductController` with routes:

```
GET  /product              → List all products
GET  /product/list?cat=x   → Filter by category
GET  /product/details/{id} → Product details
GET  /product/create       → Create form
POST /product/create       → Save new product
GET  /product/edit/{id}    → Edit form
POST /product/edit/{id}    → Update product
```

#### Part C: Views
1. **Index.cshtml** - Product list with filtering
2. **Create.cshtml** - Add new product form
3. **Edit.cshtml** - Edit product form
4. **Details.cshtml** - Product details

#### Part D: Validation
- Data Annotations
- Custom validation (price > 0)
- Model-level validation (stock warning)
- Remote validation (check if product exists)

#### Part E: TagHelpers
Use TagHelpers for:
- Form generation (`asp-for`)
- Links (`asp-action`, `asp-route-*`)
- Validation messages (`asp-validation-for`)
- Validation summary

### Expected Output
- Full CRUD operations working
- Routing working correctly (attribute routing)
- Validation messages displayed
- Filter by category working
- TagHelpers generating proper HTML

---

## Assignment 3: Advanced - Order Management System

### Objective
Create an advanced application combining Razor Pages and MVC concepts with complex validation.

### Requirements

#### Part A: Create Models
- `Order` - Main order details
- `OrderItem` - Items in order (IValidatableObject)
- `Customer` - Customer information

#### Part B: Implement Model-Level Validation
In `OrderItem`:
```csharp
public class OrderItem : IValidatableObject
{
    // Validate business rules:
    // 1. Quantity * Price cannot exceed 1,000,000
    // 2. Delivery date must be after order date
    // 3. Bulk orders (qty > 100) must have discount
}
```

#### Part C: Custom Validation
- Custom price validation attribute
- Custom phone number validation
- Remote validation for customer email

#### Part D: Multiple Binding Scenarios
1. **GET Binding** - Filter orders by customer
2. **POST Binding** - Create new order
3. **Route Binding** - Order details by ID
4. **Query String** - Pagination

### Expected Output
- Complex validation working
- Multiple binding types functioning
- Proper error messages
- Model-level validation executing

---

## Assignment 4: Routing Deep Dive

### Objective
Master all routing types and constraints.

### Requirements

#### Part A: Conventional Routing
Set up default routes in Program.cs:
```csharp
// Pattern: {controller=Home}/{action=Index}/{id?}
```

#### Part B: Attribute Routing
Create routes with:
- Basic attribute routing
- Route parameters
- Route constraints

#### Part C: Routing Constraints
Implement routes with:
- Integer constraint: `{id:int}`
- String constraint: `{slug:string}`
- Regex constraint
- Datetime constraint
- Guid constraint

#### Part D: Advanced Routing
- Multiple routes to same action
- Catch-all routes
- Optional parameters
- API-style routes

### Code Example
```csharp
[Route("api/[controller]")]
[Route("[controller]")]
public class ProductController : Controller
{
    [HttpGet("")]                           // /product
    [HttpGet("list")]                       // /product/list
    public IActionResult Index() { }
    
    [HttpGet("{id:int}")]                   // /product/123
    public IActionResult Details(int id) { }
    
    [HttpGet("search/{name:regex(^[a-z]+$)}")]
    public IActionResult Search(string name) { }
    
    [HttpGet("{year:int}/{month:int}")]     // /product/2024/12
    public IActionResult ByDate(int year, int month) { }
}
```

### Expected Output
- All routes working correctly
- Constraints enforced
- Invalid routes returning 404

---

## 🎯 Difficulty Levels

| Assignment | Level | Time |
|------------|-------|------|
| 1: Student Management | Beginner | 2-3 hours |
| 2: Product Inventory | Intermediate | 3-4 hours |
| 3: Order Management | Advanced | 4-5 hours |
| 4: Routing Deep Dive | Advanced | 2-3 hours |

---

## ✅ Checklist Before Submission

- [ ] All required properties in models
- [ ] Validation attributes working
- [ ] Server-side validation implemented
- [ ] Client-side validation working
- [ ] All views created and styled
- [ ] TagHelpers used correctly
- [ ] Routing working properly
- [ ] Error messages displayed
- [ ] Forms functional
- [ ] Code follows best practices

---

## 💡 Tips & Tricks

1. **Testing Validation:**
   ```csharp
   // In controller
   if (!ModelState.IsValid)
   {
       var errors = ModelState.Values.SelectMany(v => v.Errors);
   }
   ```

2. **Debugging Binding Issues:**
   - Check `[BindProperty]` is set
   - Verify property names match form field names
   - Use `[FromQuery]`, `[FromRoute]`, `[FromForm]` explicitly

3. **Routing Priority:**
   - More specific routes first
   - Route constraints before generic patterns
   - Test with actual requests

4. **Validation Order:**
   - Server-side validation runs after client validation
   - Use `ModelState.IsValid` to check all validations
   - Custom validation runs in order defined

---

**Good Luck! 🚀**
