using Microsoft.AspNetCore.Mvc;
using RoutingMasterApp.Models;

namespace RoutingMasterApp.Controllers
{
    // Multiple routes: api/blog or blog
    [Route("api/[controller]")]
    [Route("[controller]")]
    public class BlogController : Controller
    {
        // In-memory blog posts for demo
        private static List<BlogPost> posts = new List<BlogPost>
        {
            new BlogPost { PostId = 1, Title = "Getting Started with Routing", Slug = "getting-started-routing", Category = "ASP.NET Core", Content = "Learn about routing...", PublishedDate = DateTime.Now.AddDays(-5), ViewCount = 150 },
            new BlogPost { PostId = 2, Title = "Advanced Routing Techniques", Slug = "advanced-routing-techniques", Category = "ASP.NET Core", Content = "Advanced routing patterns...", PublishedDate = DateTime.Now.AddDays(-3), ViewCount = 220 },
            new BlogPost { PostId = 3, Title = "C# Best Practices", Slug = "csharp-best-practices", Category = "C#", Content = "Follow best practices...", PublishedDate = DateTime.Now.AddDays(-10), ViewCount = 420 }
        };

        // ============================================
        // 1. CONVENTIONAL ROUTING EXAMPLES
        // ============================================

        // GET: /blog or /api/blog
        // Route: {controller}/{action}
        [HttpGet("")]
        [HttpGet("list")]
        public IActionResult Index([FromQuery] string? category = null, [FromQuery] int page = 1)
        {
            var result = posts;
            if (!string.IsNullOrEmpty(category))
                result = result.Where(p => p.Category.Contains(category, StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.Categories = posts.Select(p => p.Category).Distinct();
            return View(result);
        }

        // ============================================
        // 2. ATTRIBUTE ROUTING WITH PARAMETERS
        // ============================================

        // GET: /blog/post/123
        // Route: {controller}/post/{id}
        [HttpGet("post/{id:int}")]
        public IActionResult Details(int id)
        {
            var post = posts.FirstOrDefault(p => p.PostId == id);
            return post == null ? NotFound() : View(post);
        }

        // GET: /blog/slug/getting-started-routing
        // Route: {controller}/slug/{slug}
        [HttpGet("slug/{slug:string}")]
        public IActionResult BySlug(string slug)
        {
            var post = posts.FirstOrDefault(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
            return post == null ? NotFound() : View("Details", post);
        }

        // ============================================
        // 3. ROUTING CONSTRAINTS
        // ============================================

        // GET: /blog/by-id/123 (integer constraint)
        [HttpGet("by-id/{id:int}")]
        public IActionResult GetById(int id)
        {
            var post = posts.FirstOrDefault(p => p.PostId == id);
            return post == null ? NotFound() : Json(new { success = true, data = post });
        }

        // GET: /blog/by-guid/{guid:guid}
        // Route constraint: guid format
        [HttpGet("by-guid/{postGuid:guid}")]
        public IActionResult GetByGuid(Guid postGuid)
        {
            return Json(new { postGuid = postGuid, message = "Valid GUID received" });
        }

        // GET: /blog/by-date/2024-12-25
        // Route constraint: datetime format
        [HttpGet("by-date/{date:datetime}")]
        public IActionResult GetByDate(DateTime date)
        {
            var postsOnDate = posts.Where(p => p.PublishedDate.Date == date.Date).ToList();
            return Json(new { date = date, posts = postsOnDate });
        }

        // ============================================
        // 4. RANGE CONSTRAINTS (min/max)
        // ============================================

        // GET: /blog/page/1 or /blog/page/100
        // Constraint: page must be between 1 and 100
        [HttpGet("page/{pageNumber:int:min(1):max(100)}")]
        public IActionResult GetPage(int pageNumber)
        {
            var itemsPerPage = 10;
            var skipItems = (pageNumber - 1) * itemsPerPage;
            var pageItems = posts.Skip(skipItems).Take(itemsPerPage).ToList();
            
            return Json(new { 
                page = pageNumber, 
                totalItems = posts.Count, 
                pageSize = itemsPerPage,
                items = pageItems 
            });
        }

        // ============================================
        // 5. MULTIPLE ROUTE PARAMETERS
        // ============================================

        // GET: /blog/category/ASP.NET Core/year/2024
        // Route: {controller}/category/{category}/year/{year}
        [HttpGet("category/{category}/year/{year:int}")]
        public IActionResult GetByCategory(string category, int year)
        {
            var result = posts.Where(p =>
                p.Category.Equals(category, StringComparison.OrdinalIgnoreCase) &&
                p.PublishedDate.Year == year
            ).ToList();

            return Json(new { category = category, year = year, posts = result });
        }

        // ============================================
        // 6. REGEX CONSTRAINTS
        // ============================================

        // GET: /blog/file/document.pdf
        // Constraint: filename must end with .pdf
        [HttpGet("file/{filename:regex(^[a-z0-9-]+\\.pdf$)}")]
        public IActionResult DownloadFile(string filename)
        {
            return Ok(new { message = $"File: {filename} validated", format = "PDF" });
        }

        // GET: /blog/tag/csharp
        // Constraint: tag must be lowercase alphanumeric with hyphens
        [HttpGet("tag/{tag:regex(^[a-z0-9-]+$)}")]
        public IActionResult GetByTag(string tag)
        {
            return Json(new { tag = tag, message = "Valid tag format" });
        }

        // ============================================
        // 7. CATCH-ALL ROUTES
        // ============================================

        // GET: /blog/view/any/path/here
        // Route: {controller}/view/{*path}
        [HttpGet("view/{*path}")]
        public IActionResult CatchAll(string path)
        {
            return Json(new { message = "Catch-all route", path = path });
        }

        // ============================================
        // 8. MULTIPLE ROUTES TO SAME ACTION
        // ============================================

        // Multiple routes: all point to same action
        [HttpGet("")]
        [HttpGet("list")]
        [HttpGet("all")]
        [HttpGet("posts")]
        public IActionResult ListAll()
        {
            return View("Index", posts);
        }

        // ============================================
        // 9. HTTP METHOD-BASED ROUTING
        // ============================================

        // GET: /blog/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /blog/create
        [HttpPost("create")]
        public IActionResult Create(BlogPost post)
        {
            if (!ModelState.IsValid)
                return View(post);

            post.PostId = posts.Max(p => p.PostId) + 1;
            posts.Add(post);
            return RedirectToAction(nameof(Details), new { id = post.PostId });
        }

        // PUT: /blog/123
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] BlogPost post)
        {
            var existing = posts.FirstOrDefault(p => p.PostId == id);
            if (existing == null) return NotFound();

            existing.Title = post.Title;
            existing.Content = post.Content;
            return Ok(new { message = "Post updated" });
        }

        // DELETE: /blog/123
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var post = posts.FirstOrDefault(p => p.PostId == id);
            if (post == null) return NotFound();

            posts.Remove(post);
            return Ok(new { message = "Post deleted" });
        }

        // ============================================
        // 10. OPTIONAL PARAMETERS
        // ============================================

        // GET: /blog/search or /blog/search/term
        // Route: {controller}/search/{query?}
        [HttpGet("search/{query?}")]
        public IActionResult Search(string? query = null)
        {
            if (string.IsNullOrEmpty(query))
                return View("Index", posts);

            var results = posts.Where(p =>
                p.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                p.Content.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            return View("Index", results);
        }

        // ============================================
        // 11. ROUTING WITH QUERY STRINGS
        // ============================================

        // GET: /blog/filter?minRating=3&category=ASP.NET Core
        // Query string parameters (NOT in route)
        [HttpGet("filter")]
        public IActionResult Filter([FromQuery] int minRating = 0, [FromQuery] string? category = null)
        {
            var result = posts;
            if (minRating > 0)
                result = result.Where(p => p.Rating >= minRating).ToList();
            if (!string.IsNullOrEmpty(category))
                result = result.Where(p => p.Category == category).ToList();

            return View("Index", result);
        }

        // ============================================
        // 12. API STYLE ROUTES
        // ============================================

        // GET: /api/blog/posts or /api/blog
        [HttpGet("posts", Name = "GetAllPosts")]
        public IActionResult GetPosts()
        {
            return Json(new { total = posts.Count, posts = posts });
        }

        // GET: /api/blog/posts/123 or /api/blog/post/123
        [HttpGet("posts/{id:int}", Name = "GetPostById")]
        [HttpGet("post/{id:int}")]
        public IActionResult GetPost(int id)
        {
            var post = posts.FirstOrDefault(p => p.PostId == id);
            return post == null ? NotFound() : Json(post);
        }

        // GET: /api/blog/stats
        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            return Json(new
            {
                totalPosts = posts.Count,
                totalViews = posts.Sum(p => p.ViewCount),
                averageRating = posts.Average(p => p.Rating),
                categories = posts.GroupBy(p => p.Category).Select(g => new { name = g.Key, count = g.Count() })
            });
        }
    }
}
