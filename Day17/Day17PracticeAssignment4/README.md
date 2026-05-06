# Day17 - Practice Assignment 4: Routing Master - Complete Routing Guide

## Overview
Complete Routing Master application covering all routing concepts: conventional routing, attribute routing, routing constraints, multiple routes, catch-all routes, and advanced routing patterns.

## Project Structure

```
RoutingMasterApp/
├── Models/
│   └── BlogPost.cs           (Blog post model)
├── Controllers/
│   └── BlogController.cs     (All routing examples)
├── Views/
│   └── Blog/
│       ├── Index.cshtml      (Post list)
│       ├── Create.cshtml     (Create form)
│       └── Details.cshtml    (Post details)
└── README.md                 (This file)
```

## Routing Concepts Covered

### 1. Conventional Routing
**Pattern:** `{controller}/{action}/{id?}`

```csharp
// GET: /blog or /api/blog
[HttpGet("")]
[HttpGet("list")]
public IActionResult Index() { }
```

**URLs:**
- `/blog` → Index()
- `/blog/list` → Index()

### 2. Basic Attribute Routing
**Decorator on Controller:**
```csharp
[Route("api/[controller]")]    // api/blog
[Route("[controller]")]         // blog
public class BlogController : Controller { }
```

**Decorator on Action:**
```csharp
[HttpGet("")]           // /blog or /api/blog
[HttpGet("list")]       // /blog/list or /api/blog/list
public IActionResult Index() { }
```

### 3. Route Parameters

#### Integer Parameter
```csharp
// GET: /blog/post/123
[HttpGet("post/{id:int}")]
public IActionResult Details(int id) { }
```

**Valid URLs:**
- `/blog/post/123` ✓
- `/blog/post/abc` ✗ (not integer)

#### String Parameter
```csharp
// GET: /blog/slug/getting-started-routing
[HttpGet("slug/{slug:string}")]
public IActionResult BySlug(string slug) { }
```

### 4. Routing Constraints

**Common Constraints Table:**

| Constraint | Format | Example | Matches |
|-----------|--------|---------|---------|
| `int` | `{id:int}` | `/post/123` | Integers only |
| `string` | `{name:string}` | `/post/abc` | Any string |
| `bool` | `{active:bool}` | `/filter/true` | true/false |
| `datetime` | `{date:datetime}` | `/2024-12-25` | Date format |
| `guid` | `{id:guid}` | `/uuid` | GUID format |
| `float` | `{price:float}` | `/10.5` | Decimal |
| `decimal` | `{amount:decimal}` | `/100.50` | Money |
| `regex` | `{name:regex(pattern)}` | Custom pattern | Regex match |
| `min` | `{age:int:min(18)}` | `/25` | Minimum value |
| `max` | `{id:int:max(100)}` | `/50` | Maximum value |
| `minlength` | `{slug:minlength(5)}` | `/hello` | Min length |
| `maxlength` | `{code:maxlength(3)}` | `/ABC` | Max length |
| `length` | `{code:length(3)}` | `/ABC` | Exact length |
| `range` | `{page:int:range(1,100)}` | `/50` | Range |
| `alpha` | `{name:alpha}` | `/abc` | Letters only |
| `required` | `{id:required}` | `/123` | Required |

#### Examples in Code:

```csharp
// GUID Constraint
// GET: /blog/by-guid/550e8400-e29b-41d4-a716-446655440000
[HttpGet("by-guid/{postGuid:guid}")]
public IActionResult GetByGuid(Guid postGuid) { }

// DateTime Constraint
// GET: /blog/by-date/2024-12-25
[HttpGet("by-date/{date:datetime}")]
public IActionResult GetByDate(DateTime date) { }

// Min/Max Range
// GET: /blog/page/1 through /blog/page/100
[HttpGet("page/{pageNumber:int:min(1):max(100)}")]
public IActionResult GetPage(int pageNumber) { }

// Regex Constraint - PDF files only
// GET: /blog/file/document.pdf (✓)
// GET: /blog/file/document.txt (✗)
[HttpGet("file/{filename:regex(^[a-z0-9-]+\\.pdf$)}")]
public IActionResult DownloadFile(string filename) { }

// Regex - Lowercase alphanumeric with hyphens
[HttpGet("tag/{tag:regex(^[a-z0-9-]+$)}")]
public IActionResult GetByTag(string tag) { }
```

### 5. Multiple Route Parameters

```csharp
// GET: /blog/category/ASP.NET Core/year/2024
[HttpGet("category/{category}/year/{year:int}")]
public IActionResult GetByCategory(string category, int year) { }
```

**URL Structure:**
- `/blog/category/ASP.NET Core/year/2024`
- Parameter `category` = "ASP.NET Core"
- Parameter `year` = 2024

### 6. Catch-All Routes

```csharp
// GET: /blog/view/any/path/here/with/segments
[HttpGet("view/{*path}")]
public IActionResult CatchAll(string path) { }
```

**Captures everything:**
- `/blog/view/documents/2024/report` → path = "documents/2024/report"
- `/blog/view/files/archive` → path = "files/archive"

### 7. Multiple Routes to Same Action

```csharp
[HttpGet("")]
[HttpGet("list")]
[HttpGet("all")]
[HttpGet("posts")]
public IActionResult ListAll() { }
```

**All URLs call same action:**
- `/blog` ✓
- `/blog/list` ✓
- `/blog/all` ✓
- `/blog/posts` ✓

### 8. HTTP Method-Based Routing

```csharp
// GET: /blog/create
[HttpGet("create")]
public IActionResult Create() => View();

// POST: /blog/create
[HttpPost("create")]
public IActionResult Create(BlogPost post) { }

// PUT: /blog/123
[HttpPut("{id:int}")]
public IActionResult Update(int id, [FromBody] BlogPost post) { }

// DELETE: /blog/123
[HttpDelete("{id:int}")]
public IActionResult Delete(int id) { }
```

**HTTP Methods:**
- `GET` - Retrieve data
- `POST` - Create data
- `PUT` - Replace data
- `PATCH` - Partial update
- `DELETE` - Remove data

### 9. Optional Parameters

```csharp
// GET: /blog/search or /blog/search/csharp
[HttpGet("search/{query?}")]
public IActionResult Search(string? query = null) { }
```

**Valid URLs:**
- `/blog/search` → query = null
- `/blog/search/csharp` → query = "csharp"

### 10. Query String Parameters

```csharp
// GET: /blog/filter?minRating=3&category=ASP.NET Core
[HttpGet("filter")]
public IActionResult Filter(
    [FromQuery] int minRating = 0,
    [FromQuery] string? category = null
) { }
```

**Query String Binding:**
- NOT part of route path
- Extracted from query string
- Can be optional or required

**URLs:**
- `/blog/filter` (no parameters)
- `/blog/filter?minRating=3` (one parameter)
- `/blog/filter?minRating=3&category=ASP` (multiple)

### 11. Named Routes

```csharp
[HttpGet("posts", Name = "GetAllPosts")]
public IActionResult GetPosts() { }

[HttpGet("posts/{id:int}", Name = "GetPostById")]
public IActionResult GetPost(int id) { }
```

**Using Named Routes in Views:**
```html
<!-- Generate URL using named route -->
<a href="@Url.RouteLink("GetPostById", new { id = post.PostId })">
    View Post
</a>
```

### 12. API-Style Routes

```csharp
[Route("api/[controller]")]
public class BlogController : Controller
{
    // GET: /api/blog/posts
    [HttpGet("posts")]
    public IActionResult GetPosts() { }
    
    // GET: /api/blog/posts/123
    [HttpGet("posts/{id:int}")]
    public IActionResult GetPost(int id) { }
    
    // GET: /api/blog/stats
    [HttpGet("stats")]
    public IActionResult GetStats() { }
}
```

## Routing Priority & Matching Order

Routes are matched in order of:
1. **Route specificity** - More specific first
2. **Parameter presence** - With parameters before without
3. **Constraints** - Constrained before unconstrained

```csharp
// Order matters!
[Route("[controller]")]
public class ExampleController : Controller
{
    // 1. Most specific - exact match
    [HttpGet("special")]
    public IActionResult Special() { }
    
    // 2. With constraint - more specific
    [HttpGet("{id:int}")]
    public IActionResult ById(int id) { }
    
    // 3. Generic - least specific
    [HttpGet("{name:string}")]
    public IActionResult ByName(string name) { }
    
    // 4. Catch-all - last resort
    [HttpGet("{*path}")]
    public IActionResult CatchAll(string path) { }
}
```

## Testing Routes in Postman

### GET Requests
```
GET /blog/post/123
GET /blog/slug/getting-started-routing
GET /blog/by-date/2024-12-25
GET /blog/page/1
GET /blog/category/ASP.NET Core/year/2024
GET /blog/search/csharp
GET /blog/filter?minRating=3&category=ASP.NET Core
```

### API Requests
```
GET    /api/blog/posts
GET    /api/blog/posts/123
POST   /api/blog/create
PUT    /api/blog/123
DELETE /api/blog/123
```

## Binding Source Priority

When multiple sources could provide a value:

```
1. Route values          {id}
2. Query string          ?id=1
3. Form body             <input name="id" />
4. Headers               Authorization: Bearer
5. Body (for APIs)       @"{ id: 1 }"
```

**Use `[From*]` to be explicit:**
```csharp
public IActionResult Get(
    [FromRoute] int id,         // From URL
    [FromQuery] string? search,  // From query string
    [FromForm] string? name,     // From form
    [FromHeader] string? token   // From headers
) { }
```

## Running the Application

```bash
cd RoutingMasterApp
dotnet run
```

**Test URLs:**
```
https://localhost:5001/blog
https://localhost:5001/blog/post/1
https://localhost:5001/blog/slug/getting-started-routing
https://localhost:5001/blog/by-date/2024-12-25
https://localhost:5001/blog/page/1
https://localhost:5001/api/blog/posts
https://localhost:5001/api/blog/stats
```

## Route Testing Checklist

### Basic Routes
- [ ] `/blog` works
- [ ] `/blog/list` works
- [ ] `/api/blog` works

### Parameter Routes
- [ ] `/blog/post/123` works (valid ID)
- [ ] `/blog/post/abc` fails (invalid ID)
- [ ] `/blog/slug/test-slug` works

### Constraint Routes
- [ ] `/blog/by-guid/550e8400-e29b-41d4-a716-446655440000` works
- [ ] `/blog/by-guid/invalid` fails
- [ ] `/blog/page/50` works (between 1-100)
- [ ] `/blog/page/150` fails (exceeds max)

### Regex Routes
- [ ] `/blog/file/document.pdf` works
- [ ] `/blog/file/document.txt` fails
- [ ] `/blog/tag/csharp` works
- [ ] `/blog/tag/C#` fails (has non-alphanumeric)

### Query String
- [ ] `/blog/filter` works
- [ ] `/blog/filter?minRating=3` works
- [ ] `/blog/filter?category=ASP.NET Core` works

### Catch-All
- [ ] `/blog/view/any/path` works
- [ ] `/blog/view/deep/nested/path/here` works

## Key Takeaways

✅ **DO:**
- Use attribute routing for clarity
- Apply constraints to validate parameters
- Use named routes for generated links
- Make parameters required or optional explicitly
- Test routes thoroughly

❌ **DON'T:**
- Mix conventional and attribute routing
- Create ambiguous routes
- Forget constraints for safety
- Expose internal IDs in URLs
- Create overly complex route patterns

## Summary Table

| Concept | Example | Use Case |
|---------|---------|----------|
| Basic | `[HttpGet("")]` | Simple routes |
| Param | `{id:int}` | Resource by ID |
| Constraint | `:guid`, `:regex` | Validation |
| Multiple | `{year}/{month}/{day}` | Hierarchical |
| CatchAll | `{*path}` | File downloads |
| QueryString | `?filter=value` | Optional filtering |
| Optional | `{query?}` | Optional segment |
| Named | `Name = "GetById"` | Generate URLs |

---

**Difficulty Level:** Advanced
**Estimated Time:** 3-4 hours
**Topics Covered:** All routing types, constraints, parameters, API routes

**Happy Routing! 🚀**
