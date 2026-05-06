# ASP.NET Razor Pages & MVC - Comprehensive Concepts Guide

## Table of Contents
1. [Web Application Templates](#web-application-templates)
2. [Razor Pages Deep Dive](#razor-pages-deep-dive)
3. [MVC Pattern](#mvc-pattern)
4. [Model Binding](#model-binding)
5. [Validation](#validation)
6. [Routing](#routing)
7. [TagHelpers & HTML Helpers](#taghelpers--html-helpers)
8. [Real-World Examples](#real-world-examples)

---

## Web Application Templates

### 1. MVC Web App (Fullstack)
**Purpose:** Enterprise-level web applications with complete separation of concerns.

**Architecture:**
```
├── Models/          (Data structures, business logic)
├── Controllers/     (Request handlers, orchestrators)
└── Views/           (UI templates, rendered HTML)
```

**Characteristics:**
- Full control over HTML rendering
- Suitable for complex applications
- Steeper learning curve
- More boilerplate code

**Use When:**
- Building large enterprise applications
- Need complex routing and controller logic
- Team familiar with traditional MVC pattern
- API and web UI need different handling

**Example:** E-commerce platform, ERP systems, Admin dashboards

---

### 2. Web API (Backend)
**Purpose:** Building RESTful services that return JSON/XML data.

**Architecture:**
```
Request → Routing → Controller → Model/Service → Response (JSON/XML)
```

**Characteristics:**
- No UI rendering (no Views)
- HTTP status codes and JSON responses
- Perfect for microservices
- Can be consumed by multiple clients

**Use When:**
- Building mobile apps backend
- Creating microservices
- Need API-first development
- Data consumption from multiple clients

**Example:** Mobile app backend, Microservices, Third-party integrations

---

### 3. Web Application (Razor Pages)
**Purpose:** Lightweight, page-focused web applications with simplified development.

**Architecture:**
```
├── Pages/
│   ├── Page.cshtml      (View/UI)
│   └── Page.cshtml.cs   (PageModel/Logic)
```

**Characteristics:**
- Page-based approach (similar to traditional web development)
- Page and logic combined in one model
- Simplified data binding with `[BindProperty]`
- Excellent for CRUD applications
- Faster development

**Use When:**
- Building lightweight CRUD applications
- Rapid development required
- Team new to ASP.NET
- Simpler applications (e.g., Blog, Todo, Forms)

**Example:** CMS, Blog platforms, Admin panels, Form applications

---

## Razor Pages Deep Dive

### What are Razor Pages?

**Definition:** A page-based programming model in ASP.NET Core where each page encapsulates both the UI (HTML) and the page model (C# logic) in a single unit.

### File Structure

```
Pages/
├── Index.cshtml       ← View (HTML + Razor syntax)
├── Index.cshtml.cs    ← PageModel (C# logic)
├── Register.cshtml
└── Register.cshtml.cs
```

### Benefits

| Feature | Benefit |
|---------|---------|
| **Page-Based** | Easy to understand for beginners |
| **Less Code** | No separate controller files needed |
| **Model Binding** | Automatic binding with `[BindProperty]` |
| **Maintainable** | Logic and UI in same location |
| **Rapid Dev** | Quick CRUD application creation |

### Page Model Lifecycle

The complete request-response cycle in Razor Pages:

```
1. REQUEST ARRIVES
   ↓
2. ROUTING
   Determines which page to handle
   ↓
3. PAGE INSTANTIATION
   Razor Pages runtime creates PageModel instance
   ↓
4. DEPENDENCY INJECTION
   Services injected into PageModel
   ↓
5. HANDLER METHOD EXECUTION
   OnGet(), OnPost(), OnPut(), OnDelete(), etc.
   ↓
6. MODEL BINDING
   Form/query data mapped to [BindProperty] properties
   ↓
7. VALIDATION EXECUTION
   Data Annotation validators run
   ModelState populated
   ↓
8. RESPONSE GENERATION
   Return Page() or IActionResult
   ↓
9. RESPONSE SENT TO CLIENT
```

### Handler Methods Explained

```csharp
public class RegisterModel : PageModel
{
    // GET request handler (STAGE 1)
    // Called when user navigates to page via GET
    public void OnGet()
    {
        // Load data for display (e.g., list of students)
        Students = LoadStudentsFromDatabase();
    }
    
    // Async variant
    public async Task OnGetAsync()
    {
        Students = await LoadStudentsAsync();
    }
    
    // With route parameter
    [Route("/register/{id}")]
    public void OnGet(int id)
    {
        StudentId = id;
    }

    // POST request handler (STAGE 2)
    // Called when user submits form via POST
    public IActionResult OnPost()
    {
        // Validation happens automatically
        if (!ModelState.IsValid)
        {
            // Return to same page with errors
            return Page();
        }
        
        // Process data
        SaveStudent(Student);
        
        // Redirect to success page
        return RedirectToPage("Success");
    }

    // PUT request handler
    public IActionResult OnPut()
    {
        // Update operation
    }

    // DELETE request handler
    public IActionResult OnDelete()
    {
        // Delete operation
    }

    // Custom handler method
    public IActionResult OnPostApprove()
    {
        // Called when form submits with button value="Approve"
    }
}
```

---

## Property Binding

### GET Binding (One-Way from URL to Backend)

**What it does:** Extracts data from URL query string and binds it to page model property.

**Usage:**
```csharp
// URL: /register?searchName=John&pageSize=10
[BindProperty(SupportsGet = true)]
public string? SearchName { get; set; }

[BindProperty(SupportsGet = true)]
public int PageSize { get; set; } = 10;

public void OnGet()
{
    // SearchName and PageSize are automatically populated
    Students = FilterStudents(SearchName);
}
```

**HTML Example:**
```html
<form method="get">
    <input name="SearchName" value="@Model.SearchName" />
    <button>Search</button>
</form>
```

### POST Binding (One-Way from Form to Backend)

**What it does:** Extracts data from form submission and binds to page model property.

**Usage:**
```csharp
[BindProperty]  // SupportsGet = false by default
public Student? Student { get; set; }

public IActionResult OnPost()
{
    // Student is automatically populated from form
    // Student.Name, Student.Email, etc.
}
```

**HTML Example:**
```html
<form method="post">
    <input asp-for="Student.Name" />
    <input asp-for="Student.Email" />
    <button type="submit">Submit</button>
</form>
```

### Two-Way Binding (UI ↔ Backend)

**What it does:** Automatically syncs data between form and backend on both directions.

**In HTML:**
```html
<!-- Binding for display AND edit -->
<input asp-for="Student.Name" />

<!-- This generates HTML like: -->
<!-- <input name="Student.Name" value="@Model.Student.Name" /> -->
```

**Process:**
```
1. Page Load: Backend → HTML (Display data from Model)
2. User Edit: HTML → User input
3. Form Submit: HTML Form Data → Backend (Populate Model)
4. Validation: Check ModelState
5. If Valid: Save to database
   If Invalid: Redisplay with errors
```

---

## Model Binding

### What is Model Binding?

**Definition:** The process of automatically mapping HTTP request data (query strings, form data, route values) to model properties.

### Binding Sources (Priority Order)

```
1. Route values          {id}
2. Query string          ?id=1
3. Form body             <input name="id" />
4. Headers               Authorization: Bearer xxx
```

### Binding Attributes

```csharp
public IActionResult Create(
    [FromRoute] int id,           // From URL route
    [FromQuery] string? search,   // From query string
    [FromForm] Student student,   // From form body
    [FromHeader] string? token,   // From headers
    [FromBody] Product product    // From JSON body (API)
)
```

### BindProperty vs Manual Binding

```csharp
// AUTOMATIC BINDING (Razor Pages)
[BindProperty]
public Student? Student { get; set; }

// MANUAL BINDING (MVC Controller)
public IActionResult Create([Bind("Name,Email,Age")] Student student)
{
    // Only Name, Email, Age are bound (whitelist)
}
```

### Preventing Over-Posting (Security)

**Problem:** User could submit extra properties to change admin privileges.

**Solution 1: Whitelist with [Bind]**
```csharp
[HttpPost]
public IActionResult Edit(
    int id,
    [Bind("Name,Email,Age")] Student student  // Only these fields
)
```

**Solution 2: Whitelist on Model**
```csharp
[BindProperty(Include = "Name,Email,Age")]
public Student? Student { get; set; }
```

**Solution 3: Exclude properties**
```csharp
[BindProperty(Exclude = "IsAdmin,Salary")]
public Student? Student { get; set; }
```

---

## Validation

### 1. Data Annotation Validation

**Most Common Validators:**

```csharp
public class Student
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 3, 
        ErrorMessage = "Name must be 3-100 characters")]
    public string? Name { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    [Phone(ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; }

    [Range(18, 65, ErrorMessage = "Age must be 18-65")]
    public int Age { get; set; }

    [Range(0.0, 4.0, ErrorMessage = "GPA must be 0-4.0")]
    public decimal GPA { get; set; }

    [Compare("Email", ErrorMessage = "Emails don't match")]
    public string? EmailConfirm { get; set; }

    [RegularExpression(@"^\d{10}$", 
        ErrorMessage = "Phone must be 10 digits")]
    public string? Phone { get; set; }

    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime DOB { get; set; }

    [Url(ErrorMessage = "Invalid URL")]
    public string? Website { get; set; }
}
```

### 2. Server-Side Custom Validation

**In Page Model:**
```csharp
public IActionResult OnPost()
{
    // Built-in validation runs first
    if (!ModelState.IsValid)
        return Page();
    
    // Custom business logic validation
    if (Student.Age < 18)
    {
        ModelState.AddModelError("Age", "Must be 18 or older");
    }
    
    if (StudentExists(Student.Email))
    {
        ModelState.AddModelError("Email", "Email already registered");
    }
    
    if (!ModelState.IsValid)
        return Page();
    
    // Save if all validations pass
    SaveStudent(Student);
}
```

### 3. Model-Level Validation (IValidatableObject)

**Purpose:** Complex business rule validation involving multiple properties.

**Implementation:**
```csharp
public class Order : IValidatableObject
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public List<OrderItem> Items { get; set; }
    public decimal TotalAmount { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        // Rule 1: Delivery date after order date
        if (DeliveryDate <= OrderDate)
        {
            yield return new ValidationResult(
                "Delivery date must be after order date",
                new[] { nameof(DeliveryDate) }
            );
        }
        
        // Rule 2: Total amount limit
        var itemTotal = Items.Sum(i => i.Quantity * i.Price);
        if (itemTotal > 1000000)
        {
            yield return new ValidationResult(
                "Total order exceeds maximum limit"
            );
        }
        
        // Rule 3: Must have at least one item
        if (!Items.Any())
        {
            yield return new ValidationResult(
                "Order must contain at least one item"
            );
        }
    }
}
```

### 4. Client-Side Validation

**Automatic HTML5 Attributes:**
```html
<!-- From [Required] -->
<input name="Name" required />

<!-- From [EmailAddress] -->
<input name="Email" type="email" required />

<!-- From [Range] -->
<input name="Age" type="number" min="18" max="65" />

<!-- From [StringLength] -->
<input name="Name" maxlength="100" minlength="3" />
```

**Display Validation Errors:**
```html
<!-- Single field error -->
<span asp-validation-for="Name" class="text-danger"></span>

<!-- All errors summary -->
<div asp-validation-summary="All" class="alert alert-danger"></div>

<!-- Only model-level errors -->
<div asp-validation-summary="ModelOnly"></div>
```

### 5. Remote Validation

**Purpose:** Validate on server without form submission (e.g., check if email exists).

**Attribute:**
```csharp
public class User
{
    [Remote(
        action: "VerifyEmail",
        controller: "Account",
        ErrorMessage = "Email already in use"
    )]
    public string? Email { get; set; }
}
```

**Controller Method:**
```csharp
[AcceptVerbs("Get", "Post")]
public IActionResult VerifyEmail(string email)
{
    // Return true if valid, false if already exists
    var exists = _db.Users.Any(u => u.Email == email);
    return Json(!exists);
}
```

**JavaScript Generated:**
```html
<!-- jQuery Unobtrusive Validation automatically sends AJAX request -->
```

---

## Routing

### Conventional Routing (Traditional MVC)

**Pattern:**
```csharp
// In Program.cs
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// URL: /Product/Details/5
// Maps to: ProductController.Details(5)
```

### Attribute Routing (Modern)

**Basic Routing:**
```csharp
[Route("product")]
[Route("api/v1/product")]  // Multiple routes
public class ProductController : Controller
{
    [HttpGet("")]        // /product
    public IActionResult Index() { }
    
    [HttpGet("list")]    // /product/list
    public IActionResult List() { }
    
    [HttpPost("create")] // POST /product/create
    public IActionResult Create() { }
}
```

### Route Parameters

```csharp
[Route("product/{id}")]
public IActionResult Details(int id) { }

// URL: /product/123
// id = 123

[Route("posts/{year:int}/{month:int}")]
public IActionResult ByMonth(int year, int month) { }

// URL: /posts/2024/12
// year = 2024, month = 12
```

### Route Constraints

**Purpose:** Validate route parameters to ensure they match expected format.

```csharp
// Integer constraint
[Route("product/{id:int}")]
public IActionResult Details(int id) { }
// /product/123     ✓ Matches
// /product/abc     ✗ Doesn't match

// String constraint (default)
[Route("blog/{slug:string}")]
public IActionResult Post(string slug) { }

// Guid constraint
[Route("user/{userId:guid}")]
public IActionResult UserProfile(Guid userId) { }

// Datetime constraint
[Route("events/{date:datetime}")]
public IActionResult EventsByDate(DateTime date) { }

// Regex constraint
[Route("file/{name:regex(^[a-z0-9-]+\\.pdf$)}")]
public IActionResult DownloadPDF(string name) { }

// Min/Max values
[Route("page/{pageNumber:int:min(1):max(100)}")]
public IActionResult PageContent(int pageNumber) { }

// Catch-all route
[Route("files/{*path}")]
public IActionResult DownloadFile(string path) { }
```

**Common Constraints Table:**

| Constraint | Example | Matches |
|-----------|---------|---------|
| `int` | `{id:int}` | Integers only |
| `string` | `{name:string}` | Any string |
| `bool` | `{active:bool}` | true/false |
| `datetime` | `{date:datetime}` | Dates |
| `guid` | `{id:guid}` | GUIDs |
| `float` | `{price:float}` | Decimals |
| `decimal` | `{amount:decimal}` | Money |
| `regex` | `{name:regex(pattern)}` | Regex pattern |
| `min` | `{id:int:min(1)}` | Min value |
| `max` | `{id:int:max(100)}` | Max value |

### Routing Priority

Routes are matched in order of specificity:

```csharp
[HttpGet("product/featured")]     // 1. Most specific
public IActionResult Featured() { }

[HttpGet("product/{id:int}")]     // 2. Less specific
public IActionResult Details(int id) { }

[HttpGet("product/{*name}")]      // 3. Catch-all (least specific)
public IActionResult Search(string name) { }
```

---

## TagHelpers & HTML Helpers

### TagHelpers (Modern Approach)

**Advantages:**
- HTML-like syntax
- Better IntelliSense
- More readable
- Recommended for new projects

### Common TagHelpers

```html
<!-- Form Tag Helper -->
<form asp-page="Register" asp-page-handler="Validate" method="post">
    <!-- Automatically adds anti-forgery token -->
</form>

<!-- Input Tag Helper (Two-way binding) -->
<input asp-for="Student.Name" class="form-control" />
<!-- Generates: <input type="text" name="Student.Name" value="..." class="form-control" /> -->

<!-- Select Tag Helper -->
<select asp-for="Category" asp-items="@Html.GetEnumSelectList<CourseCategory>()">
    <option>-- Select --</option>
</select>

<!-- TextArea Tag Helper -->
<textarea asp-for="Description" rows="4"></textarea>

<!-- Anchor Tag Helper (Link Generation) -->
<a asp-page="Details" asp-route-id="@student.StudentId">View</a>
<!-- Generates: <a href="/Details/123">View</a> -->

<!-- Validation Message -->
<span asp-validation-for="Student.Email" class="text-danger"></span>

<!-- Validation Summary -->
<div asp-validation-summary="ModelOnly" class="alert alert-danger"></div>

<!-- Partial View Tag Helper -->
<partial name="_StudentForm" model="Model.Student" />

<!-- Cache Tag Helper -->
<cache expires-after="@TimeSpan.FromMinutes(10)">
    @* Content cached for 10 minutes *@
</cache>
```

### HTML Helpers (Legacy)

```html
<!-- Text Input -->
@Html.TextBoxFor(m => m.Name)

<!-- Email Input -->
@Html.TextBoxFor(m => m.Email, new { type = "email" })

<!-- Dropdown -->
@Html.DropDownListFor(m => m.Category, new SelectList(Model.Categories, "Id", "Name"))

<!-- Checkbox -->
@Html.CheckBoxFor(m => m.IsActive)

<!-- TextArea -->
@Html.TextAreaFor(m => m.Description, new { rows = 4 })

<!-- Link -->
@Html.ActionLink("View", "Details", new { id = student.StudentId })

<!-- Validation Messages -->
@Html.ValidationMessageFor(m => m.Name)

<!-- Partial View -->
@Html.Partial("_StudentForm", Model.Student)
```

---

## Real-World Examples

### Example 1: Student Registration with GET & POST Binding

**Scenario:** Search existing students (GET) and register new student (POST)

```csharp
// PageModel
public class RegisterModel : PageModel
{
    // GET Binding for search
    [BindProperty(SupportsGet = true)]
    public string? SearchName { get; set; }
    
    // POST Binding for registration
    [BindProperty]
    public Student? NewStudent { get; set; }
    
    public List<Student> SearchResults { get; set; } = new();
    
    // GET handler - search students
    public void OnGet()
    {
        if (!string.IsNullOrEmpty(SearchName))
        {
            SearchResults = _db.Students
                .Where(s => s.Name.Contains(SearchName))
                .ToList();
        }
    }
    
    // POST handler - register new student
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();
        
        _db.Students.Add(NewStudent);
        _db.SaveChanges();
        
        return RedirectToPage("Success");
    }
}
```

### Example 2: Product CRUD with Routing

**Scenario:** Complete CRUD operations with attribute routing

```csharp
[Route("product")]
[Route("api/product")]
public class ProductController : Controller
{
    // LIST: GET /product
    [HttpGet("")]
    [HttpGet("list")]
    public IActionResult Index([FromQuery] string? category = null)
    {
        var products = _db.Products;
        if (!string.IsNullOrEmpty(category))
            products = products.Where(p => p.Category == category);
        return View(products);
    }
    
    // DETAILS: GET /product/123
    [HttpGet("{id:int}")]
    public IActionResult Details(int id)
    {
        var product = _db.Products.Find(id);
        if (product == null) return NotFound();
        return View(product);
    }
    
    // CREATE FORM: GET /product/create
    [HttpGet("create")]
    public IActionResult Create() => View();
    
    // CREATE: POST /product/create
    [HttpPost("create")]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid) return View(product);
        _db.Products.Add(product);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = product.ProductId });
    }
    
    // EDIT FORM: GET /product/123/edit
    [HttpGet("{id:int}/edit")]
    public IActionResult Edit(int id)
    {
        var product = _db.Products.Find(id);
        return product == null ? NotFound() : View(product);
    }
    
    // UPDATE: POST /product/123/edit
    [HttpPost("{id:int}/edit")]
    public IActionResult Edit(int id, Product product)
    {
        if (id != product.ProductId) return BadRequest();
        if (!ModelState.IsValid) return View(product);
        
        _db.Products.Update(product);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = id });
    }
    
    // DELETE: DELETE /product/123
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var product = _db.Products.Find(id);
        if (product == null) return NotFound();
        _db.Products.Remove(product);
        _db.SaveChanges();
        return Ok();
    }
}
```

---

## Best Practices Summary

✅ **DO:**
- Use `[BindProperty]` in Razor Pages
- Implement `IValidatableObject` for complex validation
- Use Attribute Routing for clarity
- Validate both server and client-side
- Use TagHelpers for forms and links
- Include `[ValidateAntiForgeryToken]` on POST actions
- Return appropriate HTTP status codes

❌ **DON'T:**
- Forget `[BindProperty]` - properties will be null
- Trust only client-side validation
- Mix routing approaches
- Expose sensitive validation messages
- Use HTML Helpers in new projects
- Bind all properties - use whitelist with `[Bind]`
- Forget to check `ModelState.IsValid`

---

**Master these concepts and you'll be proficient with ASP.NET web development!** 🚀
