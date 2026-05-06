# Day 17 - ASP.NET Razor Pages & MVC Complete Guide

## 📚 Overview
This comprehensive tutorial covers ASP.NET Razor Pages and MVC patterns, including model binding, validation, routing, and TagHelpers.

## 📁 Project Structure

```
Day17/
├── Day17Solution.sln
├── RazorPagesDemo/           # Razor Pages Application
│   ├── Models/
│   │   └── Student.cs        # Student model with validation attributes
│   └── Pages/
│       ├── Register.cshtml   # Razor page with binding examples
│       └── Register.cshtml.cs # Page model with lifecycle handlers
│
├── MVCDemo/                  # MVC Application
│   ├── Models/
│   │   ├── Product.cs        # Product model
│   │   └── OrderItem.cs      # Model-level validation example
│   ├── Controllers/
│   │   └── ProductController.cs  # MVC controller with routing
│   └── Views/Product/
│       ├── Index.cshtml      # List view with TagHelpers
│       ├── Create.cshtml     # Form with validation
│       ├── Edit.cshtml       # Edit form
│       └── Details.cshtml    # Details view
│
└── README.md                 # This file
```

---

## 🎯 Key Concepts Covered

### 1. **ASP.NET Web Application Templates**

#### MVC Web App (Fullstack)
- Combines UI + Business Logic + Data Handling
- Uses Model-View-Controller pattern
- HTML Views
- Best for: Enterprise applications

#### Web API (Backend)
- Used for building RESTful services
- No UI, returns JSON/XML
- Best for: Microservices, APIs

#### Web Application (Razor Pages)
- Page-based approach
- Simplified UI + backend integration
- HTML Pages
- Best for: Lightweight applications

---

### 2. **Razor Pages Overview**

**What are Razor Pages?**
- Page-based programming model in ASP.NET Core
- Each page has its own logic and UI combined (C# + HTML).CSHTML
- Cleaner structure compared to MVC
- Less code required
- Built-in model binding

**Project Structure:**
- `Pages/Index.cshtml` - UI layer
- `Pages/Index.cshtml.cs` - Logic layer (PageModel)

**Features:**
✅ Cleaner structure (page-focused)
✅ Less code compared to MVC
✅ Built-in model binding
✅ Easy to maintain

---

### 3. **Page Model Lifecycle**

**Stages of Request Processing:**

```
Request → Routing → Handler Method → Model Binding → Validation → Response
```

#### Handler Methods:

1. **OnGet()** - Handles GET request
   ```csharp
   public void OnGet()
   {
       // Load data for display
   }
   ```

2. **OnPost()** - Handles POST request
   ```csharp
   public IActionResult OnPost()
   {
       if (!ModelState.IsValid)
           return Page();
       // Process form data
       return RedirectToPage("Success");
   }
   ```

3. **OnPut() / OnDelete()** - Optional handlers for other HTTP methods

4. **Model Binding** - Automatic mapping from form data to model properties

5. **Validation Execution** - Server-side validation

6. **Response Rendering** - Return the response

---

### 4. **Property Binding**

#### GET Binding
- Gets data from URL query string
- Used for: Search, Filter, Pagination
- Example: `?id=1&name=John`

```csharp
[BindProperty(SupportsGet = true)]
public string? SearchName { get; set; }
```

#### POST Binding
- Gets data from form submission
- Used for: Submit, Save

```csharp
[BindProperty]
public Student? Student { get; set; }
```

#### One-Way Binding vs Two-Way Binding

| Aspect | One-Way | Two-Way |
|--------|---------|---------|
| Direction | One direction (UI → Backend OR Backend → UI) | Both directions (UI ↔ Backend) |
| Use Case | Display data OR send data | Form input + display updated value |
| Example | `@Model.Name` | `<input asp-for="Name" />` |

---

### 5. **MVC Pattern**

**Model-View-Controller Architecture:**

```
User
  ↓
Request → Controller
           ↓
           Requests Information
           ↓
           Model (Get Data)
           ↓
           Set Data
           ↓
           View (Display Data)
           ↓
           Response
```

**Request/Response Flow:**
```
Request → Routing → Controller → Model → View → Response
```

---

### 6. **Model Binding**

Model binding automatically maps incoming data to model properties:

- **GET Binding**: From query string
- **POST Binding**: From form data
- **Route Binding**: From URL route parameters

```csharp
public IActionResult Create([Bind("Name,Email,Age")] Student student)
{
    // Data is automatically bound from form
}
```

---

### 7. **TagHelpers vs HTML Helpers**

#### TagHelpers (Modern)
```html
<input asp-for="Name" />
<select asp-for="Category">
<a asp-action="Details" asp-route-id="@product.ProductId" />
```

**Advantages:**
- More readable
- HTML-like syntax
- Easier to use
- Better IntelliSense

#### HTML Helpers (Legacy)
```html
@Html.TextBoxFor(m => m.Name)
@Html.DropDownListFor(m => m.Category)
@Html.ActionLink("Details", "Product", new { id = product.ProductId })
```

---

### 8. **Validation Types**

#### A. Data Annotation Validation (Server-side)

```csharp
[Required(ErrorMessage = "Name is required")]
[StringLength(100, MinimumLength = 3)]
public string? Name { get; set; }

[EmailAddress(ErrorMessage = "Invalid email")]
public string? Email { get; set; }

[Range(18, 65, ErrorMessage = "Age must be 18-65")]
public int Age { get; set; }
```

**Common Attributes:**
- `[Required]` - Field is mandatory
- `[StringLength(max, MinimumLength=min)]` - String length range
- `[Range(min, max)]` - Numeric range
- `[EmailAddress]` - Valid email format
- `[RegularExpression]` - Regex pattern
- `[Compare]` - Compare with another field

#### B. Server-Side Custom Validation

```csharp
public IActionResult OnPost()
{
    if (student.Age < 18)
    {
        ModelState.AddModelError("Age", "Must be 18 or older");
    }
    
    if (!ModelState.IsValid)
        return Page();
}
```

#### C. Model-Level Validation (IValidatableObject)

```csharp
public class OrderItem : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        // Complex business rules
        if (DeliveryDate <= OrderDate)
        {
            yield return new ValidationResult(
                "Delivery date must be after order date");
        }
    }
}
```

#### D. Client-Side Validation

- HTML5 attributes: `required`, `type="email"`, `min`, `max`
- jQuery validation scripts
- Validation summary displayed before form submission

```html
<span asp-validation-for="Name" class="text-danger"></span>
<div asp-validation-summary="ModelOnly"></div>
```

#### E. Remote Validation

Check data on the server without form submission:

```csharp
[Remote(action: "VerifyEmail", controller: "Account")]
public string? Email { get; set; }
```

---

### 9. **URL Routing**

#### Conventional Routing (MVC)
```csharp
routes.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

#### Attribute Routing (Modern)
```csharp
[Route("product")]
[Route("api/[controller]")]
public class ProductController : Controller
{
    [HttpGet("details/{id:int}")]
    public IActionResult Details(int id) { }
    
    [HttpGet("search/{name}")]
    public IActionResult Search(string name) { }
}
```

#### Routing Constraints
```csharp
[Route("product/{id:int}")]      // id must be integer
[Route("post/{slug:regex(^[a-z0-9-]+$)}")]  // slug pattern
[Route("files/{*path}")]         // Catch-all route
```

**Common Constraints:**
- `:int` - Integer only
- `:string` - String only
- `:guid` - GUID format
- `:datetime` - DateTime format
- `:regex(pattern)` - Regex pattern

---

## 🚀 Running the Applications

### Prerequisites
- .NET 8.0 SDK
- Visual Studio Code or Visual Studio

### RazorPagesDemo
```bash
cd RazorPagesDemo
dotnet run
# Navigate to: https://localhost:5001/Register
```

**Features:**
- GET Binding: Search students by name
- POST Binding: Register new student
- Two-way binding with `asp-for`
- Validation attributes
- Page model lifecycle

### MVCDemo
```bash
cd MVCDemo
dotnet run
# Navigate to: https://localhost:5001/product
```

**Features:**
- Conventional + Attribute Routing
- TagHelpers for form generation
- Model binding from query strings & forms
- Server-side validation
- Custom validation in controller
- Model-level validation (IValidatableObject)
- CRUD operations

---

## 📝 Code Examples

### Example 1: Razor Pages Property Binding

**Pages/Register.cshtml.cs:**
```csharp
public class RegisterModel : PageModel
{
    // GET Binding from query string
    [BindProperty(SupportsGet = true)]
    public string? SearchName { get; set; }
    
    // POST Binding from form
    [BindProperty]
    public Student? Student { get; set; }
    
    public void OnGet() { }  // Handle GET
    public IActionResult OnPost() { }  // Handle POST
}
```

### Example 2: MVC Controller with Routing

**Controllers/ProductController.cs:**
```csharp
[Route("product")]
public class ProductController : Controller
{
    [HttpGet("list")]
    public IActionResult Index([FromQuery] string? category)
    {
        // Model binding from query string
    }
    
    [HttpGet("details/{id:int}")]
    public IActionResult Details(int id)
    {
        // Routing constraint: id must be integer
    }
}
```

### Example 3: Validation

**Models/Student.cs:**
```csharp
public class Student
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 3)]
    public string? Name { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [Range(18, 65)]
    public int Age { get; set; }
}
```

### Example 4: TagHelpers in View

**Views/Product/Index.cshtml:**
```html
<!-- TagHelper for routing -->
<a asp-action="Details" asp-route-id="@product.ProductId">View</a>

<!-- TagHelper for form binding -->
<input asp-for="Product.Name" class="form-control" />

<!-- Validation message -->
<span asp-validation-for="Product.Name" class="text-danger"></span>
```

---

## 📚 Learning Path

1. **Start with Razor Pages** - Simpler, page-based approach
   - Property binding
   - Page model lifecycle
   - Validation

2. **Then Learn MVC** - More complex but powerful
   - Controllers and Actions
   - Routing (conventional + attribute)
   - Model binding
   - Views

3. **Advanced Topics**
   - Custom validation
   - Remote validation
   - Routing constraints
   - TagHelper creation

---

## 🎓 Best Practices

### ✅ DO
- Use `[BindProperty]` for automatic model binding
- Implement `IValidatableObject` for complex validations
- Use Attribute Routing for clarity
- Validate both client-side and server-side
- Use TagHelpers instead of HTML Helpers

### ❌ DON'T
- Forget `[BindProperty]` - leads to null properties
- Skip server-side validation
- Trust client-side validation alone
- Use deprecated HTML Helpers
- Mix routing approaches (stick to Attribute Routing)

---

## 📚 Additional Resources

- [Microsoft ASP.NET Core Razor Pages Documentation](https://docs.microsoft.com/aspnet/core/razor-pages/)
- [ASP.NET Core MVC Guide](https://docs.microsoft.com/aspnet/core/mvc/)
- [Data Annotations Validation](https://docs.microsoft.com/ef/core/modeling/data-annotations)
- [Routing in ASP.NET Core](https://docs.microsoft.com/aspnet/core/fundamentals/routing)

---

## 🏆 Summary

| Feature | Razor Pages | MVC |
|---------|------------|-----|
| Structure | Page-based | Controller-based |
| Binding | Built-in [BindProperty] | Manual binding |
| Complexity | Simpler | More complex |
| Scale | Lightweight apps | Enterprise apps |
| Learning Curve | Easier | Steeper |

Choose **Razor Pages** for simpler applications and faster development.
Choose **MVC** for larger, more complex enterprise applications.

---

**Happy Learning! 🚀**
